using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    public Quaternion rotation;
    public GameObject player; //Main player object


    //OnTriggerEnter2D()
    //Removes health if hit with a bullet
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "EnemyBullet" || collision.gameObject.tag == "Bullet") {
            Destroy(collision.gameObject);
        }
        //switch (collision.gameObject.tag) {
        //    case "EnemyBullet":
        //        break;
        //    case:
        //        break;
        //    default: break;
        //}
    }

    void Start()
    {
        player = GameObject.Find("Player");
        rotation = transform.rotation;
        gameObject.SetActive(false);
    }

    public void ShieldPressed() {
        gameObject.SetActive(true);
    }

    public void ShieldNotPressed() {
        gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        transform.position = player.GetComponent<PlayerMovement>().PlayerPosition;
        transform.rotation = rotation;
    }
}
