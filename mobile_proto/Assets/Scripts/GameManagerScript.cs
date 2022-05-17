using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Manages level progression within the main game scene
public class GameManagerScript : MonoBehaviour {
    public static int level = 1;//Current Level
    private float modifiedGameSpeed = 1; //Sets the game speed
    private bool levelBeaten = false;//Is set to true if the level has been beaten

    //ENEMY SPAWNING
    public static int spawnCalcMax = 100;
    public int amountOfEnemies; //Number of enemies per round
    private int spawnCalcValueToBeAddedUpToMax = 0;
    private float rangeOfPointsOnMap = 20; //Size of possible spawns 
    private int smallestSpawnCalcValue = 56;
    [SerializeField] private GameObject background; //In order to maintain spawn on the map

    // PREFABS
    [SerializeField] private GameObject enemyInfantryPrefab;
    [SerializeField] private GameObject enemyExplorerPrefab;
    [SerializeField] private GameObject enemyGuardianPrefab;
    [SerializeField] private GameObject enemyBarragePrefab;
    [SerializeField] private GameObject enemyInfantryIIPrefab;
    [SerializeField] private GameObject enemyInfantryIIIPrefab;
    [SerializeField] private GameObject enemyPassengerPrefab;
    private GameObject[] enemyPrefabs;

    [SerializeField] private GameObject portalPrefab;
    [SerializeField] private GameObject levelFinishTextPrefab;

    //MISC
    [SerializeField] private Text levelText;
    [SerializeField] private Text playerHealthText;
    [SerializeField] private Text EnemiesLeftText;

    public int AmountOfEnemies{
        get{ return amountOfEnemies; }
        set { amountOfEnemies = value; }
    } 

    //Start()
    //Creates enemies at random positions
    public void Start() {
        levelText.text = levelText.text + level;
        enemyPrefabs = new GameObject[] { enemyInfantryPrefab, enemyExplorerPrefab, enemyGuardianPrefab, enemyInfantryIIPrefab, enemyBarragePrefab, enemyPassengerPrefab, enemyInfantryIIIPrefab };
        spawnCalcMax = GenerateSpawnCalcMax();
        CreateLevelsEnemies();

    }

    //GenerateSpawnCalcMax()
    //Updates the field spawnCalcMax to increase the difficulty between levels
    public int GenerateSpawnCalcMax() {
        return (int)((float)spawnCalcMax * 1.3f);
    }

    //isSpawningTooClose()
    //returns true if the spawn point will be too close for the player to react to
    private bool isSpawningTooClose(float x, float y) {
        return (x > 14 || x < -14) || (y > 6.7f || y < -13);//TRUE, CAN SPAWN THERE
    } 

    //RandomMapPosition()
    //Returns a random position on the map
    public Vector3 RandomMapPosition() {
        float xSpawnValues = Random.Range(background.transform.position.x - rangeOfPointsOnMap, background.transform.position.x + rangeOfPointsOnMap);
        float ySpawnValues = Random.Range(background.transform.position.y - rangeOfPointsOnMap, background.transform.position.y + rangeOfPointsOnMap);
        while(!isSpawningTooClose(xSpawnValues, ySpawnValues))
        {
             xSpawnValues = Random.Range(background.transform.position.x - rangeOfPointsOnMap, background.transform.position.x + rangeOfPointsOnMap);
             ySpawnValues = Random.Range(background.transform.position.y - rangeOfPointsOnMap, background.transform.position.y + rangeOfPointsOnMap);
        }
        return new Vector3(xSpawnValues, ySpawnValues, 0f);
    }

    //CreateLevelEnemies()
    //FOR TESTING
    public void CreateLevelsEnemies(int i) {
        Instantiate(enemyInfantryIIIPrefab, RandomMapPosition(), new Quaternion(0, 0, 0, 0));
        amountOfEnemies++;

    }

    //CreateLevelsEnemies()
    //Generate the correct amount of enemies per level
    public void CreateLevelsEnemies() {
        while (spawnCalcValueToBeAddedUpToMax <= spawnCalcMax - smallestSpawnCalcValue) {
            GameObject temp = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];//Range has an int overload that is max exclusive -_-
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
        playerHealthText.text = "Health: " + PlayerMovement.health.ToString();
        int enemiesLeft = amountOfEnemies - EnemyScript.destroyedEnemies;
        EnemiesLeftText.text = "CATS LEFT: " + enemiesLeft;
        //Debug.Log(spawnCalcMax);
        if (EnemyScript.destroyedEnemies == amountOfEnemies && !levelBeaten) {
            levelBeaten = true;
            Instantiate(portalPrefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            Instantiate(levelFinishTextPrefab);
            level++;
        }
        
    }
}
