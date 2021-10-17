using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Manages level progression within the main game scene
public class GameManagerScript : MonoBehaviour
{
    public GameObject enemyPrefab; //Basic enemy prefab
    public GameObject portalPrefab;

    public static int level = 1;//Current Level

    [SerializeField] public int range; //Size of possible spawns 
    [SerializeField] public GameObject background; //In order to maintain spawn on the map
    [SerializeField] public Text levelText;
    [SerializeField] public int amountOfEnemies; //Number of enemies per round

    //Start()
    //Creates enemies at random positions
    public void Start()
    {
        levelText.text = levelText.text + level;
        amountOfEnemies = (amountOfEnemies * level)/2;
        for(int i = 0; i < amountOfEnemies; i++) {
            Instantiate(enemyPrefab, RandomMapPosition(), new Quaternion(0, 0, 0, 0));
        }
    }

    //RandomMapPosition()
    //Returns a random position on the map
    public Vector3 RandomMapPosition() {
        return new Vector3(Random.Range(background.transform.position.x - range, background.transform.position.x + range), Random.Range(background.transform.position.y - range, background.transform.position.y + range), 0f);
    }


    //FixedUpdate()
    //Starts next round if all enemies were destroyed
    public void FixedUpdate() {
        if (EnemyScript.destroyedEnemies == amountOfEnemies) {
            EnemyScript.destroyedEnemies = 0;
            Instantiate(portalPrefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            level++;
        }
    }
}
