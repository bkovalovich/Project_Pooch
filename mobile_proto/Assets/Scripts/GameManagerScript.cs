using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Manages level progression within the main game scene
public class GameManagerScript : MonoBehaviour
{
    private int level;
    public GameObject enemyPrefab; //Basic enemy prefab
    private int destroyedEnemies = 0; //Number of destoryed enemies

    [SerializeField] public int range; //Size of possible spawns 
    [SerializeField] public GameObject background; //In order to maintain spawn on the map
    [SerializeField] public int amountOfEnemies; //Number of enemies per round

    //Start()
    //Creates enemies at random positions
    public void Start()
    {
        destroyedEnemies = 0;
        for(int i = 0; i < amountOfEnemies; i++) {
            Instantiate(enemyPrefab, RandomMapPosition(), new Quaternion(0, 0, 0, 0));
        }
    }

    //RandomMapPosition()
    //Returns a random position on the map
    public Vector3 RandomMapPosition() {
        return new Vector3(Random.Range(background.transform.position.x - range, background.transform.position.x + range), Random.Range(background.transform.position.y - range, background.transform.position.y + range), 0f);
    }

    //EnemyDestroyed()
    //Called by all enemy prefabs, communicates that it was destroyed
    public void EnemyDestroyed() {
        destroyedEnemies++;
    }

    //FixedUpdate()
    //Starts next round if all enemies were destroyed
    public void FixedUpdate() {
        if (destroyedEnemies == amountOfEnemies) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
