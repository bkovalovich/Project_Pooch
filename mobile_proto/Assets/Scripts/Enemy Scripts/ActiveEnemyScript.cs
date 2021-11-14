using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveEnemyScript : EnemyScript {

    void ActiveMovement() {
        FaceOtherObject(GetGameObjectAngle(player));
    }

    void FixedUpdate() {
        DetermineIfDestroyed();
        ActiveMovement();
        transform.position += transform.right * Time.deltaTime * movementSpeed;
    }
}
