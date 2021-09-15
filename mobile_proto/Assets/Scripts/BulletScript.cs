using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float lifetime;
    public Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid.velocity = transform.up * speed;
    }

    // Update is called once per frame
    void Update() {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f) {
            Destroy(gameObject);    
        }
    }
}
