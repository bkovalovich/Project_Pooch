using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPassengerScript : EnemyScript
{
    [SerializeField] public Transform LeftTopSpawnPoint;//Where the weapon is used
    [SerializeField] public Transform RightTopSpawnPoint;//Where the weapon is used
    [SerializeField] public Transform LeftBottomSpawnPoint;//Where the weapon is used
    [SerializeField] public Transform RightBottomSpawnPoint;//Where the weapon is used

    [SerializeField] public GameObject explorerPrefab;//The basic bullet that will be continuously used
    [SerializeField] public float rateOfSpawning;//How often the bullet can be fired
    private GameObject gameManager;
    private GameManagerScript gameManagerScript;
    private float currentRechargeTime = 0f;//Determines current time between weapon uses

    new protected void Start() {
        base.Start();
        gameManager = GameObject.Find("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
    }

    //Deploy()
    //Creates bullet
    public void Deploy(Vector3 offset) {
        Instantiate(explorerPrefab, LeftTopSpawnPoint.position + offset, LeftTopSpawnPoint.rotation);
        Instantiate(explorerPrefab, RightTopSpawnPoint.position + offset, RightTopSpawnPoint.rotation);
        Instantiate(explorerPrefab, LeftBottomSpawnPoint.position + offset, LeftBottomSpawnPoint.rotation);
        Instantiate(explorerPrefab, RightBottomSpawnPoint.position + offset, RightBottomSpawnPoint.rotation);
        gameManagerScript.AmountOfEnemies = gameManagerScript.AmountOfEnemies + 4;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PassiveMovement();
        currentRechargeTime += Time.deltaTime;
        if (currentRechargeTime >= rateOfSpawning) {
            currentRechargeTime = currentRechargeTime % rateOfSpawning;
            Deploy(new Vector3(0, 0, 0));
        }
    }
}
