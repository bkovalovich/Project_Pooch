using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Manages level progression within the main game scene
public class GameManagerScript : MonoBehaviour
{
    public GameObject enemyPrefab; //Basic enemy prefab
    public GameObject enemy2Prefab; //Basic enemy2 prefab
    public GameObject enemy3Prefab; //Basic enemy3 prefab
    public GameObject enemy4Prefab; //Basic enemy4 prefab
    public GameObject enemy5Prefab; //Basic enemy4 prefab
    public GameObject enemy6Prefab; //Basic enemy4 prefab
    public GameObject portalPrefab;
    public GameObject levelFinishTextPrefab;
    public GameObject backgroundMusic;

    public static int level = 1;//Current Level
    private int amountOfEnemies; //Number of enemies per round

    [SerializeField] public int range; //Size of possible spawns 
    [SerializeField] public GameObject background; //In order to maintain spawn on the map
    [SerializeField] public Text levelText;
    [SerializeField] public Text playerHealthText;
    [SerializeField] public Text EnemiesLeftText;

    void Awake() {
    }


    //Start()
    //Creates enemies at random positions
    public void Start() {
        levelText.text = levelText.text + level;
        for (amountOfEnemies = 0; amountOfEnemies < level; amountOfEnemies = amountOfEnemies + 3) {
            Instantiate(enemyPrefab, RandomMapPosition(), new Quaternion(0, 0, 0, 0));
            Instantiate(enemy2Prefab, RandomMapPosition(), new Quaternion(0, 0, 0, 0));
            Instantiate(enemy3Prefab, RandomMapPosition(), new Quaternion(0, 0, 0, 0));
        }
        if (Random.Range(0, 6) == 1) {
            Instantiate(enemy6Prefab, RandomMapPosition(), new Quaternion(0, 0, 0, 0));
            amountOfEnemies++;
        }
        if (level >= 5) {
            Instantiate(enemy5Prefab, RandomMapPosition(), new Quaternion(0, 0, 0, 0));
            Instantiate(enemy5Prefab, RandomMapPosition(), new Quaternion(0, 0, 0, 0));
            Instantiate(enemy5Prefab, RandomMapPosition(), new Quaternion(0, 0, 0, 0));
            Instantiate(enemy4Prefab, RandomMapPosition(), new Quaternion(0, 0, 0, 0));
            Instantiate(enemy4Prefab, RandomMapPosition(), new Quaternion(0, 0, 0, 0));
            amountOfEnemies = amountOfEnemies + 5;
        }
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
        //Debug.Log()
        playerHealthText.text = "Health: " + PlayerMovement.health.ToString();
        int enemiesLeft = amountOfEnemies - EnemyScript.destroyedEnemies;
        EnemiesLeftText.text = "CATS LEFT: " + enemiesLeft;
        if (EnemyScript.destroyedEnemies == amountOfEnemies) {
            EnemiesLeftText.text = "Proceed to wormhole";
            EnemyScript.destroyedEnemies = 0;
            Instantiate(portalPrefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            Instantiate(levelFinishTextPrefab);
            level++;
        }
    }
}
