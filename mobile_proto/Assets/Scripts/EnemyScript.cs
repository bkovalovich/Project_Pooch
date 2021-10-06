using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] public int health;
    private GameObject gameManager;

    void Start() {
        gameManager = GameObject.Find("GameManager");
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Bullet") {
            health--;
        }
    }
    public void FixedUpdate()
    {
        if(health <= 0) {
            Destroy(gameObject);
        }
    }
}
