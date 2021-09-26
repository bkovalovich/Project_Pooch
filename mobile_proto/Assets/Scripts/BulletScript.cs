using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] public float speed;
    public float lifetime = 0.1f;
    public Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid.velocity = transform.up * speed;
    }

    // Update is called once per frame
    void FixedUpdate() {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f) {
            Destroy(gameObject);    
        }
    }
}
