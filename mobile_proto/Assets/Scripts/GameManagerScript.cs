using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Manages level progression within the main game scene
public class GameManagerScript : MonoBehaviour
{
    public static int level = 1;//Current Level
    private int amountOfEnemies; //Number of enemies per round
    private float modifiedGameSpeed = 1; //Sets the game speed
    private bool levelBeaten = false;//Is set to true if the level has been beaten

    private int range; //Size of possible spawns 
    [SerializeField] private GameObject background; //In order to maintain spawn on the map

    // PREFABS
    [SerializeField] public GameObject enemyPrefab;
    //[SerializeField] private GameObject enemy2Prefab;
    //[SerializeField] private GameObject enemy3Prefab;
    //[SerializeField] private GameObject enemy4Prefab;
    //[SerializeField] private GameObject enemy5Prefab;
    //[SerializeField] private GameObject enemy6Prefab;
    //[SerializeField] private GameObject enemy7Prefab;
    [SerializeField] private GameObject portalPrefab;
    [SerializeField] private GameObject levelFinishTextPrefab;

    //MISC
    [SerializeField] private Text levelText;
    [SerializeField] private Text playerHealthText;
    [SerializeField] private Text EnemiesLeftText;

    void Awake() {
    }


    //Start()
    //Creates enemies at random positions
    public void Start() {
        levelText.text = levelText.text + level;

        createLevelsEnemies();

        //for (amountOfEnemies = 0; amountOfEnemies < level; amountOfEnemies++) {
        //    Instantiate(enemyPrefab, RandomMapPosition(), new Quaternion(0, 0, 0, 0));

        //    if (level >= 5) {
        //        Instantiate(enemy5Prefab, RandomMapPosition(), new Quaternion(0, 0, 0, 0));
        //        Instantiate(enemy5Prefab, RandomMapPosition(), new Quaternion(0, 0, 0, 0));
        //        Instantiate(enemy5Prefab, RandomMapPosition(), new Quaternion(0, 0, 0, 0));
        //        Instantiate(enemy6Prefab, RandomMapPosition(), new Quaternion(0, 0, 0, 0));
        //        amountOfEnemies = amountOfEnemies + 6;
        //    }
        //    if (level >= 10) {
        //        Instantiate(enemy7Prefab, RandomMapPosition(), new Quaternion(0, 0, 0, 0));
        //        amountOfEnemies++;
        //    }
        //}

    }

    //RandomMapPosition()
    //Returns a random position on the map
    public Vector3 RandomMapPosition() {
        return new Vector3(Random.Range(background.transform.position.x - range, background.transform.position.x + range), Random.Range(background.transform.position.y - range, background.transform.position.y + range), 0f);
    }

    public void createLevelsEnemies() {

        Instantiate(enemyPrefab, RandomMapPosition(), new Quaternion(0, 0, 0, 0));
        amountOfEnemies++;


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
