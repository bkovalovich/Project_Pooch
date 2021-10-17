using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles behavior for basic enemies
public class EnemyScript : MonoBehaviour
{
    [SerializeField] public int health;//Number of hits needed to destroy enemy
    [SerializeField] public float movementSpeed;
    [SerializeField] public float maxRotateSpeed;

    public float currentRotateSpeed;
    public static int destroyedEnemies = 0;
    public static GameObject player;

    void Start() {
        player = GameObject.Find("Player");
        currentRotateSpeed = maxRotateSpeed;
    }

    //OnTriggerEnter2D()
    //Removes health if hit with a bullet
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Bullet") {
            health--;
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

    //PreventJitter()
    //Descreases rate of rotation to not continuously over/undershoot the value and cause jittering
    float PreventJitter(float playerAngle, float objectAngle, float currentRotateSpeed, float maxRotateSpeed) {
        float checkSmallerThanRotate = Mathf.Abs(playerAngle - objectAngle);
        if (checkSmallerThanRotate < currentRotateSpeed) {
            return checkSmallerThanRotate;
        }
             return maxRotateSpeed;
    }
    
    //FacePlayer(){
    //Continuously rotate to face another object 
    void FaceOtherObject(float playerAngle) {
        try {
            currentRotateSpeed = PreventJitter(playerAngle, transform.rotation.eulerAngles.z, currentRotateSpeed, maxRotateSpeed);

            if (playerAngle > transform.rotation.eulerAngles.z) {
                transform.Rotate(Vector3.forward * currentRotateSpeed);//LEFT
            } else if (playerAngle < transform.rotation.eulerAngles.z) {
                transform.Rotate(Vector3.back * currentRotateSpeed);//RIGHT
            }
        } catch (MissingReferenceException) {
            Debug.Log("Enemy could not find the player");
        }
    }

    //FixedUpdate()
    //Destroys enemy if health is equal/below zero
    public void FixedUpdate()
    {
        if(health <= 0) {
            destroyedEnemies++;
            Destroy(gameObject);
        } else {
            FaceOtherObject(GetGameObjectAngle(player));
            transform.position += transform.right * Time.deltaTime * movementSpeed;
        }
    }

}
