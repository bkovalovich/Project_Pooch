using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Handles behavior for basic enemies
public class EnemyScript : MonoBehaviour {
    protected float currentHealth;
    protected float currentRotateSpeed;
    protected float prevPlayerAngle;
    protected bool isRotatingRight;

    public static int destroyedEnemies = 0;
    public static GameObject player;

    private float normalBulletDamage = 1;
    private float chargeBulletDamage = 10; 

    [SerializeField] public float startingHealth;
    [SerializeField] public float movementSpeed;
    [SerializeField] public float maxRotateSpeed;

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
        currentRotateSpeed = maxRotateSpeed;
    }

    //OnTriggerEnter2D()
    //Removes health if hit with a bullet
    private void OnTriggerEnter2D(Collider2D collision) {
        switch (collision.gameObject.tag) {
            case "ChargeBullet":
                currentHealth = currentHealth - chargeBulletDamage;
                break;
            case "Shield":
                currentHealth = 0;
                break;
            case "Bullet":
                currentHealth = currentHealth - normalBulletDamage;
                Destroy(collision.gameObject);
                break;
            default: break;
        }
    }

    //getGameObjectAngle()
    //Gets the angle between current object and parameter object
    public float GetGameObjectAngle(GameObject player) {
        Vector3 playerToEnemyDistance = player.transform.position;
        playerToEnemyDistance.z = 0f;

        playerToEnemyDistance.x = playerToEnemyDistance.x - transform.position.x;
        playerToEnemyDistance.y = playerToEnemyDistance.y - transform.position.y;

        float playerAngle = Mathf.Atan2(playerToEnemyDistance.y, playerToEnemyDistance.x) * Mathf.Rad2Deg;

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

    public bool ChangedAxes(float playerAngle) {
        if((prevPlayerAngle <= 10 && prevPlayerAngle >= 0) && (playerAngle <= 360 && playerAngle >= 350) || (prevPlayerAngle <= 360 && prevPlayerAngle >= 350) && (playerAngle <= 10 && playerAngle >= 0)) {
            return true;
        } else {
            return false;
        }
    }

    public void DetermineDirection(float playerAngle) { //(playerAngle < 15f && playerAngle > 0f) || (playerAngle < 359.9999f && playerAngle > 340f)
        if (ChangedAxes(playerAngle)) {
            isRotatingRight = !isRotatingRight;
        } else
       if (playerAngle > transform.rotation.eulerAngles.z) {
            isRotatingRight = false;
        } else if (playerAngle < transform.rotation.eulerAngles.z) {
            isRotatingRight = true;
        }
    }

    //FaceOtherObject(){
    //Continuously rotate to face another object 
    public void FaceOtherObject(float playerAngle) {
        try {
            currentRotateSpeed = CalcRotateSpeed(playerAngle, transform.rotation.eulerAngles.z, currentRotateSpeed, maxRotateSpeed);
            DetermineDirection(playerAngle);
            if (isRotatingRight) {
                transform.Rotate(Vector3.back * currentRotateSpeed);//RIGHT
            } else {
                transform.Rotate(Vector3.forward * currentRotateSpeed);//LEFT
            }
        } catch (MissingReferenceException) { Debug.Log("Enemy could not find the player"); }
        prevPlayerAngle = playerAngle;
    }

    public void DetermineIfDestroyed() {
        if (currentHealth <= 0) {
            destroyedEnemies++;
            Destroy(gameObject);
        }
    }

}
