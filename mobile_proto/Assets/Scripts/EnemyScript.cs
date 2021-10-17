using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles behavior for basic enemies
public class EnemyScript : MonoBehaviour
{
    [SerializeField] public int health;//Number of hits needed to destroy enemy
    [SerializeField] public float rotateSpeed;
    [SerializeField] public float movementSpeed;


    public static int destroyedEnemies = 0;
    public static GameObject player;

    void Start() {
        player = GameObject.Find("Player");
    }

    //OnTriggerEnter2D()
    //Removes health if hit with a bullet
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Bullet") {
            health--;
        }
    }

    void facePlayer() {
        try {
            Vector3 targ = player.transform.position;
            targ.z = 0f;
            Vector3 objectPos = transform.position;

            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;

            float playerAngle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
            if (playerAngle < 0) {
                playerAngle = 360 + playerAngle;
            }
            if (playerAngle > transform.rotation.eulerAngles.z) {
                transform.Rotate(Vector3.forward * rotateSpeed);
            } else if (playerAngle < transform.rotation.eulerAngles.z) {
                transform.Rotate(Vector3.back * rotateSpeed);
            }
        } catch (MissingReferenceException) { }
    }

    //FixedUpdate()
    //Destroys enemy if health is equal/below zero
    public void FixedUpdate()
    {
        if(health <= 0) {
            destroyedEnemies++;
            Destroy(gameObject);
        } else {
            facePlayer();
            transform.position += transform.right * Time.deltaTime * movementSpeed;
        }
    }

}
