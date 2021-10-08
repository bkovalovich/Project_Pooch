using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles behavior for basic enemies
public class EnemyScript : MonoBehaviour
{
    [SerializeField] public int health;//Number of hits needed to destroy enemy
    public GameObject gameManager;//Both invoked in order to communicate when object is destroyed
    public GameManagerScript gmScript;

    //Start()
    //Initializes gameManager reference
    void Start() {
        gameManager = GameObject.Find("GameManager");
        gmScript = gameManager.GetComponent<GameManagerScript>();
    }

    //OnTriggerEnter2D()
    //Removes health if hit with a bullet
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Bullet") {
            health--;
        }
    }

    //FixedUpdate()
    //Destroys enemy if health is equal/below zero
    public void FixedUpdate()
    {
        if(health <= 0) {
            gmScript.EnemyDestroyed();
            Destroy(gameObject);
        }
    }
}
