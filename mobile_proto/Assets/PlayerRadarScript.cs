using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRadarScript : MonoBehaviour
{
    [SerializeField] Enemy enemy;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            enemy.stateMachine.ChangeState(enemy.chasingState);
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            enemy.stateMachine.ChangeState(enemy.idleState);
        }
    }
}
