using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperEnemyScript : EnemyScript {

    void HyperMovement(float playerAngle) {
        try {
            currentRotateSpeed = playerAngle - transform.rotation.eulerAngles.z;
            transform.Rotate(Vector3.forward * currentRotateSpeed);
        } 
        catch (MissingReferenceException) { Debug.Log("Hyper enemy could not find the player"); }
    }

    void FixedUpdate() {
        DetermineIfDestroyed();
        HyperMovement(GetGameObjectAngle(player));
        transform.position += transform.right * Time.deltaTime * movementSpeed;
    }
}
