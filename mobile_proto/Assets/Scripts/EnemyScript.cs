using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Handles behavior for basic enemies
public class EnemyScript : MonoBehaviour {
    private float currentHealth;
    [SerializeField] public float startingHealth;
    [SerializeField] public float movementSpeed;
    [SerializeField] public float maxRotateSpeed;
    private float prevPlayerAngle;
    private bool isRotatingRight;

    private float currentRotateSpeed;
    public static int destroyedEnemies = 0;
    public static GameObject player;


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
        if (collision.gameObject.tag == "ChargeBullet" || collision.gameObject.tag == "Shield") {
            currentHealth--;
        }else if (collision.gameObject.tag == "Bullet") {
            currentHealth--;
            Destroy(collision.gameObject);
        }
    }
    //getGameObjectAngle()
    //Gets the angle between current object and parameter object
    float GetGameObjectAngle(GameObject player) {
        Vector3 targ = player.transform.position;
        targ.z = 0f;
        Vector3 objectPos = transform.position;

        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float playerAngle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;

        if (playerAngle < 0) {
            return 360 + playerAngle;
        }
        return playerAngle;
    }

    //CorrectAngle()
    //Return a proper angle that does not cause jittering when turning
    float CalcRotateSpeed(float playerAngle, float objectAngle, float currentRotateSpeed, float maxRotateSpeed) {
        float checkSmallerThanRotate = Mathf.Abs(playerAngle - objectAngle);
        if (checkSmallerThanRotate < currentRotateSpeed) {
            return checkSmallerThanRotate;
        }
        return maxRotateSpeed;
    }

    bool ChangedAxes(float playerAngle) {
        if((prevPlayerAngle <= 10 && prevPlayerAngle >= 0) && (playerAngle <= 360 && playerAngle >= 350) || (prevPlayerAngle <= 360 && prevPlayerAngle >= 350) && (playerAngle <= 10 && playerAngle >= 0)) {
            return true;
        } else {
            return false;
        }
    }

    void DetermineDirection(float playerAngle) { //(playerAngle < 15f && playerAngle > 0f) || (playerAngle < 359.9999f && playerAngle > 340f)
        if (ChangedAxes(playerAngle)) {
            isRotatingRight = !isRotatingRight;
        } else
       if (playerAngle > transform.rotation.eulerAngles.z) {
            isRotatingRight = false;
        } else if (playerAngle < transform.rotation.eulerAngles.z) {
            isRotatingRight = true;
        }
    }

    //FacePlayer(){
    //Continuously rotate to face another object 
    void FaceOtherObject(float playerAngle) {
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

    //FixedUpdate()
    //Destroys enemy if health is equal/below zero
    public void FixedUpdate() {
        if (currentHealth <= 0) {
            destroyedEnemies++;
            Destroy(gameObject);
        } else {
            //healthBarImage.transform.position = transform.position;
            FaceOtherObject(GetGameObjectAngle(player));
            transform.position += transform.right * Time.deltaTime * movementSpeed;
        }
    }

}
