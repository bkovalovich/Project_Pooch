using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveEnemyScript : EnemyScript {

    private bool onScreen;

    void OnBecameVisible() {
        onScreen = true;
    }

    void OnBecameInvisible() {
        onScreen = false;
    }


    void FixedUpdate() {
        DetermineIfDestroyed();
        if (!isPaused()) {
            spriteRenderer.color = defaultColor;
            switch (onScreen) {
                case true:
                    FacePlayer();
                    break;
                case false:
                    PassiveMovement();
                    break;
            }
        } else {
            spriteRenderer.color = hitColor;
            currentPauseTime -= Time.deltaTime;
        }
    }

    
}
