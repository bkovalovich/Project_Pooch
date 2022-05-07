using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

//Handles behavior for basic enemies
public class EnemyScript : MonoBehaviour {

    //Stat fields
    [SerializeField] protected float movementSpeed;
    [SerializeField] protected float maxRotateSpeed;
    [SerializeField] protected float startingHealth;
    [SerializeField] public int spawnCalcValue;
   
    [SerializeField] protected SpriteRenderer spriteRenderer;//For changing texture color

    //Determine if dead fields
    public static int destroyedEnemies = 0;
    protected float currentHealth;

    //Face player fields
    protected float currentRotateSpeed;
    protected static GameObject player;

    //Hitstun fields
    protected float currentPauseTime = 0;
    protected float amountOfPauseTimeOnHit = 0.06f;
    protected Color defaultColor;
    protected Color hitColor;

    //Staying in arena fields
    protected bool touchingWalls;
    protected bool justEscapedWalls = false;

    //Damage recieved by bullet fields
    private float normalBulletDamage = 1;
    private float chargeBulletDamage = 10;

    //Passive movement fields
    protected int choosingMethod = 0;
    protected int maxAmountofMethods = 4;
    protected float currentPassiveMovementTime = 0;
    protected float maxPassiveMovementTime = 3f;

    //Active movement fields
    private bool onScreen;

    //PROPERTIES
    public float HealthRatio {
        get { return currentHealth / startingHealth; }
    }
    public Vector3 EnemyPosition {
        get { return transform.position; }
    }
    public int SpawnCalcValue
    {
        get { return spawnCalcValue; }
    }

    protected void Start() {
        currentHealth = startingHealth;
        player = GameObject.Find("Player");
        defaultColor = spriteRenderer.color;
        hitColor = Color.grey;
        currentRotateSpeed = maxRotateSpeed;
    }

    //ChooseRandomMethod()
    //Assigns a random value to chooseMethod every 3 seconds unless the object just left the walls of the arena
    protected void ChooseRandomMethod() {
        if (justEscapedWalls) {
            justEscapedWalls = false;
            choosingMethod = 2;
            return;
        }
        if (currentPassiveMovementTime <= 0) {
            currentPassiveMovementTime = maxPassiveMovementTime;
            choosingMethod = UnityEngine.Random.Range(0, maxAmountofMethods);
        }
    }

    //PassiveMovement()
    //Selects a random action every 3 seconds and prevents the enemy from leaving the arena
    protected void PassiveMovement() {
        DetermineIfDestroyed();
        if (!isPaused()) {
            if (touchingWalls) {
                justEscapedWalls = true;
                TurnLeft(3f);
                MoveForward(5);
            } else {
                ChooseRandomMethod();
                currentPassiveMovementTime -= Time.deltaTime;
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
            spriteRenderer.color = defaultColor;
        } else {
            spriteRenderer.color = hitColor;
            currentPauseTime -= Time.deltaTime;
        }


    }

    protected void HyperMovement() {
        DetermineIfDestroyed();
        if (!isPaused()){
            spriteRenderer.color = defaultColor;
            FacePlayer();
        }
        else{
            spriteRenderer.color = hitColor;
            currentPauseTime -= Time.deltaTime;
        }
    }

    //FaceOtherObject()
    //Rotates current object to face the player
    protected void FacePlayer() {
        try {
            transform.position += transform.right * Time.deltaTime * movementSpeed;
            currentRotateSpeed = GetGameObjectAngle(player) - transform.rotation.eulerAngles.z;
            transform.Rotate(Vector3.forward * currentRotateSpeed);
        } catch (MissingReferenceException) { Debug.Log("Active enemy could not find the player"); }
    }

    //ActiveMovement()
    //Changes movement pattern based on if the player is onscreen
    protected void ActiveMovement() {
        DetermineIfDestroyed();
        if (!isPaused()) {
            spriteRenderer.color = defaultColor;
            switch (onScreen) {
                case true:
                    FacePlayer();
                    break;
                case false:
                    PassiveMovement();
                    break;
            }
        } else {
            spriteRenderer.color = hitColor;
            currentPauseTime -= Time.deltaTime;
        }
    }

    //getGameObjectAngle()
    //Returns the degree angle that the current object must rotate to in order to face the parameter object  
    public float GetGameObjectAngle(GameObject otherObject) {
        Vector3 objectToEnemyDistance = otherObject.transform.position; //Gets position
        objectToEnemyDistance.z = 0f; //Prevents interference with other data

        objectToEnemyDistance.x = objectToEnemyDistance.x - transform.position.x; //Gets the vert and horizontal distance between objects
        objectToEnemyDistance.y = objectToEnemyDistance.y - transform.position.y;

        float playerAngle = Mathf.Atan2(objectToEnemyDistance.y, objectToEnemyDistance.x) * Mathf.Rad2Deg;//Calculates the tangent and converts it to degrees

        if (playerAngle < 0) { //Makes sure the angle doesn't subceed 0
            return 360 + playerAngle;
        }
        return playerAngle;
    }

    //OnTriggerEnter2D()
    //Handles collisions
    private void OnTriggerEnter2D(Collider2D collision) {
        switch (collision.gameObject.tag) {
            case "ChargeBullet": //Hit by charge shot
                currentHealth = currentHealth - chargeBulletDamage;
                break;
            //case "Shield": //Hit by player bubble shield
            //    currentHealth = 0;
            //    break;
            case "Bullet": //Hit by player bullet
                currentPauseTime = amountOfPauseTimeOnHit; 
                currentHealth = currentHealth - normalBulletDamage;
                Destroy(collision.gameObject);
                break;
            case "Wall": //Interact with wall, initiate turn around behvaior
                touchingWalls = true;
                break;
            default: 
                break;
        }
    }

    //OnTriggerExit2D()
    //Finishes wall interaction
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Wall") {
            touchingWalls = false;
        }
    }

    //If onscreen Methods
    //Checks if objects are onscreen
    void OnBecameVisible() {
        onScreen = true;
    }
    void OnBecameInvisible() {
        onScreen = false;
    }

    //Movement methods
    //Can be called to quickly move object
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

    //isPaused()
    //Called when enemy is in hitstun
    public bool isPaused() {
        return currentPauseTime >= 0f;
    }

    //DetermineIfDestroyed()
    //Destroys object if health drops below 1
    public void DetermineIfDestroyed() {
        if (currentHealth <= 0) {
            destroyedEnemies++;
            Destroy(gameObject);
        }
    }

}
