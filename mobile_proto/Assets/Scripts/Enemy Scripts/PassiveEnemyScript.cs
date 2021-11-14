using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveEnemyScript : EnemyScript {


    void PassiveMovement() {
        FaceOtherObject(GetGameObjectAngle(player));
    }

    void FixedUpdate() {
        DetermineIfDestroyed();
        PassiveMovement();
        transform.position += transform.right * Time.deltaTime * movementSpeed;

    }
}
