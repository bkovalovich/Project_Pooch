using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveEnemyScript : EnemyScript {

    void FixedUpdate() {
        DetermineIfDestroyed();
        if (!isPaused()) {
            ActiveMovement();
            spriteRenderer.color = defaultColor;
        } else {
            spriteRenderer.color = hitColor;
            currentPauseTime -= Time.deltaTime;
        }
    }

    
}
