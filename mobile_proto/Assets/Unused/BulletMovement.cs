using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float speed = 2f;          // The speed our bullet travels
    public Vector3 targetVector;    // the direction it travels
    public float lifetime = 10f;     // how long it lives before destroying itself
    public float damage = 10f;       // how much damage this projectile causes
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        // find our RigidBody
        rb = gameObject.GetComponentInChildren<Rigidbody2D>();
        targetVector = targetVector.normalized * speed;
        // add force 
        //  rb.AddForce(targetVector.normalized * speed);
    }

    // Update is called once per frame
    void Update()
    {
        //targetVector = new Vector3(0, speed, 0);
        /* * Time.deltaTime*/;
        rb.MovePosition(transform.position + targetVector);
        // decrease our life timer
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f) {
            // we have ran out of life
            Destroy(gameObject);    // kill me
        }
    }
}
