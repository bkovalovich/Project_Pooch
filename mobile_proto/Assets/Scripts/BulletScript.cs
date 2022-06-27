using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A basic bullet used by the player
public class BulletScript : MonoBehaviour
{
    private float currentSpeed;

    [SerializeField] public float bulletSpeed;//Speed of bullet 20
    [SerializeField] public float bulletSpeedWhenDashing;
    [SerializeField] public float lifetime;//How long the bullet lasts
    public Rigidbody2D rigid;//Accessing rigidbody in order to change position

    public GameObject player;
    private GameObject playerScript;
    //gameObject.GetComponent<ScriptName>().variable



    //Start()
    //Changes rigidbody to continue moving in one direction
    void Start()
    {
        currentSpeed = bulletSpeed;
        rigid.velocity = transform.up * currentSpeed;
        player = GameObject.Find("Player");

    }

    //FixedUpdate()
    //Destroys object after an amount of time
    void FixedUpdate() {
        if (player.GetComponent<PlayerMovement>().isDashPressed)
        {
            currentSpeed = bulletSpeedWhenDashing;
        }
        else
        {
            currentSpeed = bulletSpeed;
        }
        rigid.velocity = transform.up * currentSpeed;

        lifetime -= Time.deltaTime;
        if (lifetime <= 0f) {
            Destroy(gameObject);    
        }
    }
}

