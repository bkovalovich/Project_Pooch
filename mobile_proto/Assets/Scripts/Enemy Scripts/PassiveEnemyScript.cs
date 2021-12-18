using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveEnemyScript : EnemyScript {

    //public override void Start() {
    //       base.Speak();
    //   }

    public int choosingMethod = 0;
    public int maxAmountofMethods = 4;
    public float currentTime = 0;
    public float maxTime = 3f;

    void ChooseMethod(int max) {
        if (currentTime <= 0) {
            currentTime = maxTime;
            choosingMethod = Random.Range(0, max);
        }
    }


    void PassiveMovement() {
        if (touchingWalls) {
            TurnLeft(1.8f);
            MoveForward();
        } else {
            ChooseMethod(maxAmountofMethods);
            currentTime -= Time.deltaTime;
            Debug.Log(choosingMethod);
            switch (choosingMethod) {
                case 0:
                    MoveForward();
                    TurnLeft();
                    break;
                case 1:
                    TurnRight(0.5f);
                    break;
                case 2:
                    MoveForward(0.9f);
                    break;
                case 3:
                    TurnLeft();
                    MoveForward(6);
                    break;
                default: break;

            }
        }
    }

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
