using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A basic bullet used by the player
public class BulletScript : MonoBehaviour
{
    [SerializeField] public float speed;//Speed of bullet
    [SerializeField] public float lifetime;//How long the bullet lasts
    public Rigidbody2D rigid;//Accessing rigidbody in order to change position

    //Start()
    //Changes rigidbody to continue moving in one direction
    void Start()
    {
        rigid.velocity = transform.up * speed;
    }

    //FixedUpdate()
    //Destroys object after an amount of time
    void FixedUpdate() {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f) {
            Destroy(gameObject);    
        }
    }
}
