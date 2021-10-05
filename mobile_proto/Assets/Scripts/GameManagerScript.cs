using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    public GameObject enemyPrefab;
    [SerializeField] public int range;
    [SerializeField] public GameObject background;
    [SerializeField] public int amountOfEnemies;

    public void Start()
    {        
        for(int i = 0; i < amountOfEnemies; i++) {
            Instantiate(enemyPrefab, randomPosition(), new Quaternion(0, 0, 0, 0));
        }
    }
    public Vector3 randomPosition() {
        return new Vector3(Random.Range(background.transform.position.x - range, background.transform.position.x + range), Random.Range(background.transform.position.y - range, background.transform.position.y + range), 0f);
        //Random.Range(background.transform.position.x, background.transform.position.x + 50), Random.Range(background.transform.position.y, background.transform.position.y + 50), 0f
    }
}
