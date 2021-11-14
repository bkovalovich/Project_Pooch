using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreenScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManagerScript.level = 1;
        EnemyScript.destroyedEnemies = 0;
        PlayerMovement.health = 5;
    }
}
