using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Manages level progression within the main game scene
public class GameManagerScript : MonoBehaviour
{
    public float modifiedGameSpeed = 1;

    public GameObject enemyPrefab; //Basic enemy prefab

    public GameObject portalPrefab;
    public GameObject levelFinishTextPrefab;
    public GameObject backgroundMusic;

    public static int level = 1;//Current Level
    private int amountOfEnemies; //Number of enemies per round
    private bool levelBeaten = false;

    public int range; //Size of possible spawns 
    public GameObject background; //In order to maintain spawn on the map
    public Text levelText;
    public Text playerHealthText;
    public Text EnemiesLeftText;

    void Awake() {
    }


    //Start()
    //Creates enemies at random positions
    public void Start() {
        levelText.text = levelText.text + level;
        for (amountOfEnemies = 0; amountOfEnemies < level; amountOfEnemies++) {
            Instantiate(enemyPrefab, RandomMapPosition(), new Quaternion(0, 0, 0, 0));
        }
        //if (Random.Range(0, 6) == 1) {
        //    Instantiate(enemy9Prefab, RandomMapPosition(), new Quaternion(0, 0, 0, 0));
        //    amountOfEnemies++;
        //}
        if (level >= 5) {
            //Instantiate(enemy5Prefab, RandomMapPosition(), new Quaternion(0, 0, 0, 0));
            //Instantiate(enemy5Prefab, RandomMapPosition(), new Quaternion(0, 0, 0, 0));
            //Instantiate(enemy5Prefab, RandomMapPosition(), new Quaternion(0, 0, 0, 0));
            //Instantiate(enemy4Prefab, RandomMapPosition(), new Quaternion(0, 0, 0, 0));
            //Instantiate(enemy4Prefab, RandomMapPosition(), new Quaternion(0, 0, 0, 0));
            //Instantiate(enemy6Prefab, RandomMapPosition(), new Quaternion(0, 0, 0, 0));
            //amountOfEnemies = amountOfEnemies + 6;
        }
        //if (level >= 10) {
        //    Instantiate(enemy7Prefab, RandomMapPosition(), new Quaternion(0, 0, 0, 0));
        //    amountOfEnemies++;

        //}
    }

    //RandomMapPosition()
    //Returns a random position on the map
    public Vector3 RandomMapPosition() {
        return new Vector3(Random.Range(background.transform.position.x - range, background.transform.position.x + range), Random.Range(background.transform.position.y - range, background.transform.position.y + range), 0f);
    }

    public void createLevelsEnemies() {
        //LEVEL SCALING HERE
    }

    //FixedUpdate()
    //Starts next round if all enemies were destroyed
    public void FixedUpdate() {
        Time.timeScale = modifiedGameSpeed;
       
        playerHealthText.text = "Health: " + PlayerMovement.health.ToString();
        int enemiesLeft = amountOfEnemies - EnemyScript.destroyedEnemies;
        EnemiesLeftText.text = "CATS LEFT: " + enemiesLeft;

        if (EnemyScript.destroyedEnemies == amountOfEnemies && !levelBeaten) {
            levelBeaten = true;
            Instantiate(portalPrefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            Instantiate(levelFinishTextPrefab);
            level++;
        }
        
    }
}
