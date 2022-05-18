using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    public Quaternion rotation;
    public GameObject player; //Main player object

    public bool isCoolingDown;
    public float maxFill = 1;
    public float currentFill = 1;
    public float shieldDamageWhenHitByBullet = 0.15f;
    private Rigidbody2D rigid;
    [SerializeField] private AudioSource bulletHitSFX;
    public static bool shieldIsUp;



    //OnTriggerEnter2D()
    //Removes health if hit with a bullet
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "EnemyBullet") {
            bulletHitSFX.Play();
            Destroy(collision.gameObject);
            currentFill -= shieldDamageWhenHitByBullet;
        }

    }

    void Start()
    {
        player = GameObject.Find("Player");
        rigid = gameObject.GetComponent<Rigidbody2D>();
        rotation = transform.rotation;
    }

    void FixedUpdate()
    {
        transform.position = player.GetComponent<PlayerMovement>().PlayerPosition;
        transform.rotation = rotation;
    }
}
