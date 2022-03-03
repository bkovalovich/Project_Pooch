using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

//Handles behavior for basic enemies
public class EnemyScript : MonoBehaviour {
    protected float currentHealth;
    protected float currentRotateSpeed;
    protected float prevPlayerAngle;
    protected bool isRotatingRight;
    protected float currentPauseTime = 0;
    protected float amountOfPauseTimeOnHit = 0.06f;

    protected Color defaultColor;
    protected Color hitColor;
    protected float playerAngle;
    protected bool currentlyColliding;
    protected bool touchingWalls;
    //protected Renderer mainRenderer;

    public static int destroyedEnemies = 0;
    public static GameObject player;
    public SpriteRenderer spriteRenderer;//For changing texture color

    private float normalBulletDamage = 1;
    private float chargeBulletDamage = 10;

    [SerializeField] public float startingHealth;
    [SerializeField] public float movementSpeed;
    [SerializeField] public float maxRotateSpeed;

    protected int choosingMethod = 0;
    protected int maxAmountofMethods = 4;
    protected float currentTime = 0;
    protected float maxTime = 3f;
    protected bool justEscapedWalls = false;

    //PROPERTIES
    public float HealthRatio {
        get { return currentHealth / startingHealth; }
    }
    public Vector3 EnemyPosition {
        get { return transform.position; }
    }

    void Start() {
        currentHealth = startingHealth;
        player = GameObject.Find("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
        hitColor = Color.grey;
        currentRotateSpeed = maxRotateSpeed;
       // mainRenderer = GetComponent<Renderer>();
    }

    protected void ChooseRandomMethod() {
        if (justEscapedWalls) {
            justEscapedWalls = false;
            choosingMethod = 2;
            return;
        }
        if (currentTime <= 0) {
            currentTime = maxTime;
            choosingMethod = UnityEngine.Random.Range(0, maxAmountofMethods);
        }
    }

    protected void PassiveMovement() {
        if (touchingWalls) {
            justEscapedWalls = true;
            TurnLeft(3f);
            MoveForward(5);
        } else {
            ChooseRandomMethod();
            currentTime -= Time.deltaTime;
            switch (choosingMethod) {
                case 0:
                    MoveForward();
                    TurnLeft();
                    break;
                case 1:
                    TurnRight();
                    break;
                case 2:
                    MoveForward();
                    break;
                case 3:
                    TurnLeft();
                    MoveForward();
                    break;
                default: break;

            }
        }
    }
    protected void ActiveMovement() {
        transform.position += transform.right * Time.deltaTime * movementSpeed;
        FaceOtherObject();
    }

    //OnTriggerEnter2D()
    private void OnTriggerEnter2D(Collider2D collision) {
        currentlyColliding = true;
        switch (collision.gameObject.tag) {
            case "ChargeBullet":
                currentHealth = currentHealth - chargeBulletDamage;
                break;
            case "Shield":
                currentHealth = 0;
                break;
            case "Bullet":
                currentPauseTime = amountOfPauseTimeOnHit; 
                currentHealth = currentHealth - normalBulletDamage;
                Destroy(collision.gameObject);
                break;
            case "Wall":
                touchingWalls = true;
                break;
            default: 
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Wall") {
            touchingWalls = false;
        }
    }

    public void TurnLeft() {
        transform.Rotate(Vector3.forward * currentRotateSpeed);
    }
    public void TurnLeft(float speed) {
        transform.Rotate(Vector3.forward * speed);
    }
    public void TurnRight() {
        transform.Rotate(Vector3.back * currentRotateSpeed);
    }
    public void TurnRight(float speed) {
        transform.Rotate(Vector3.back * speed);
    }

    public void MoveForward(float speed) {
        transform.position += transform.right * Time.deltaTime * speed;
    }
    public void MoveForward() {
        transform.position += transform.right * Time.deltaTime * movementSpeed;
    }

    public bool isPaused() {
        return currentPauseTime >= 0f;
    }

    //getGameObjectAngle()
    //Gets the angle between current enemy object and parameter object
    public float GetGameObjectAngle(GameObject otherObject) {
        Vector3 objectToEnemyDistance = otherObject.transform.position;
        objectToEnemyDistance.z = 0f;

        objectToEnemyDistance.x = objectToEnemyDistance.x - transform.position.x;
        objectToEnemyDistance.y = objectToEnemyDistance.y - transform.position.y;

        float playerAngle = Mathf.Atan2(objectToEnemyDistance.y, objectToEnemyDistance.x) * Mathf.Rad2Deg;

        if (playerAngle < 0) {
            return 360 + playerAngle;
        }
        return playerAngle;
    }

    //CalcRotateSpeed()
    //Return a proper angle that does not cause jittering when turning
    public float CalcRotateSpeed(float playerAngle, float objectAngle, float currentRotateSpeed, float maxRotateSpeed) {
        float checkSmallerThanRotate = Mathf.Abs(playerAngle - objectAngle);
        if (checkSmallerThanRotate < currentRotateSpeed) {
            return checkSmallerThanRotate;
        }
        return maxRotateSpeed;
    }

    public void DetermineDirection(float playerAngle) { //(playerAngle < 15f && playerAngle > 0f) || (playerAngle < 359.9999f && playerAngle > 340f)
        float gap = Mathf.Abs(prevPlayerAngle - playerAngle);
        if (/*(gap < 5f && gap > 0f) ||*/ (gap < 359.9999f && gap > 355f)) {
            isRotatingRight = !isRotatingRight;
        }
       if (playerAngle > transform.rotation.eulerAngles.z) {
            isRotatingRight = false;
        } else if (playerAngle < transform.rotation.eulerAngles.z) {
            isRotatingRight = true;
        }
    }

    //FaceOtherObject(){
    //Continuously rotate to face another object 
    public void FaceOtherObject() { 
        try {
            playerAngle = GetGameObjectAngle(player);
            currentRotateSpeed = CalcRotateSpeed(playerAngle, transform.rotation.eulerAngles.z, currentRotateSpeed, maxRotateSpeed);
            DetermineDirection(playerAngle);
            if (isRotatingRight) {
                transform.Rotate(Vector3.back * currentRotateSpeed);//RIGHT
            } else {
                transform.Rotate(Vector3.forward * currentRotateSpeed);//LEFT
            }
        } catch (MissingReferenceException) { Debug.Log("Enemy could not find the player"); }
        prevPlayerAngle = playerAngle;

        //Vector3 targetAngle = player.transform.position;
        //targetAngle.z = 0f;

        //Vector3 objectPos = transform.position;
        //targetAngle.x = targetAngle.x - objectPos.x;
        //targetAngle.y = targetAngle.y - objectPos.y;

        //float angle = Mathf.Atan2(targetAngle.y, targetAngle.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void DetermineIfDestroyed() {
        if (currentHealth <= 0) {
            destroyedEnemies++;
            Destroy(gameObject);
        }
    }

}
