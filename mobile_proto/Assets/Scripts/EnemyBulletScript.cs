using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "ChargeBullet" || collision.gameObject.tag == "Shield") {
            Destroy(gameObject);
        }

    }
}
