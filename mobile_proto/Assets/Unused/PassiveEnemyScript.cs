using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveEnemyScript : EnemyScript {

    void FixedUpdate() {
        DetermineIfDestroyed();
        if (!isPaused()) {
            PassiveMovement();
            spriteRenderer.color = defaultColor;
        } else {
            spriteRenderer.color = hitColor;
            currentPauseTime -= Time.deltaTime;
        }
    }
}
