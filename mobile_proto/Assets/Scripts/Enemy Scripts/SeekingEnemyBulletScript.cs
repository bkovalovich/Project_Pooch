using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekingEnemyBulletScript : EnemyScript
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "ChargeBullet" || collision.gameObject.tag == "Shield") {
            Destroy(gameObject);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
