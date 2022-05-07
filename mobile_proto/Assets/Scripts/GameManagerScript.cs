using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Manages level progression within the main game scene
public class GameManagerScript : MonoBehaviour
{
    public static int level = 1;//Current Level
    private float modifiedGameSpeed = 1; //Sets the game speed
    private bool levelBeaten = false;//Is set to true if the level has been beaten

    //ENEMY SPAWNING
    public static int spawnCalcMax = 100;
    private int spawnCalcValueToBeAddedUpToMax = 0;
    private int amountOfEnemies; //Number of enemies per round
    private int range = 20; //Size of possible spawns 
    private int smallestSpawnCalcValue = 56;
    [SerializeField] private GameObject background; //In order to maintain spawn on the map

    // PREFABS
    [SerializeField] private GameObject enemyInfantryPrefab;
    [SerializeField] private GameObject enemyExplorerPrefab;
    [SerializeField] private GameObject enemyGuardianPrefab;
    [SerializeField] private GameObject enemyBarragePrefab;
    [SerializeField] private GameObject enemyInfantryIIPrefab;
    private GameObject[] enemyPrefabs;

    [SerializeField] private GameObject portalPrefab;
    [SerializeField] private GameObject levelFinishTextPrefab;

    //MISC
    [SerializeField] private Text levelText;
    [SerializeField] private Text playerHealthText;
    [SerializeField] private Text EnemiesLeftText;

    //Start()
    //Creates enemies at random positions
    public void Start() {
        levelText.text = levelText.text + level;
        enemyPrefabs = new GameObject[] { enemyInfantryPrefab, enemyExplorerPrefab, enemyGuardianPrefab, enemyBarragePrefab, enemyInfantryIIPrefab };
        spawnCalcMax = GenerateSpawnCalcMax();
        CreateLevelsEnemies();

    }

    //GenerateSpawnCalcMax()
    //Updates the field spawnCalcMax to increase the difficulty between levels
    public int GenerateSpawnCalcMax() {
        return (int)((float)spawnCalcMax * 1.3f);
    }

    //RandomMapPosition()
    //Returns a random position on the map
    public Vector3 RandomMapPosition() {
        return new Vector3(Random.Range(background.transform.position.x - range, background.transform.position.x + range), Random.Range(background.transform.position.y - range, background.transform.position.y + range), 0f);
    }

    //CreateLevelsEnemies()
    //Generate the correct amount of enemies per level
    public void CreateLevelsEnemies() {
        while (spawnCalcValueToBeAddedUpToMax <= spawnCalcMax - smallestSpawnCalcValue) {
            GameObject temp = enemyPrefabs[Random.Range(0, enemyPrefabs.Length - 1)];
            int tryNewCalcValue = spawnCalcValueToBeAddedUpToMax + temp.GetComponent<EnemyScript>().spawnCalcValue;
            if (tryNewCalcValue <= spawnCalcMax) {
                Instantiate(temp, RandomMapPosition(), new Quaternion(0, 0, 0, 0));
                spawnCalcValueToBeAddedUpToMax = tryNewCalcValue;
                amountOfEnemies++;
            } 
        }

    }

    //FixedUpdate()
    //Starts next round if all enemies were destroyed
    public void FixedUpdate() {
        Time.timeScale = modifiedGameSpeed;
        Debug.Log(spawnCalcMax);
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
