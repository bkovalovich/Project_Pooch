using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperEnemyScript : EnemyScript {


    void FixedUpdate() {
        DetermineIfDestroyed();
        transform.position += transform.right * Time.deltaTime * movementSpeed;
    }
}
