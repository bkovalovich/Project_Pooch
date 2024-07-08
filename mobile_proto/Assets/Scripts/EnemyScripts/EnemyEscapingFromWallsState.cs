using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEscapingFromWallsState : EnemyState {
    private Vector2 knockbackPos; 
    public EnemyEscapingFromWallsState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine) {
       // Debug.Log("Escaping from walls state init");
    }

    public override void EnterState() {
        Debug.Log($"entered {this.GetType()}");
        knockbackPos = (enemy.wallhitPoint - enemy.transform.position) * 10;
    }

    public override void ExitState() {
    }

    public override void FrameUpdate() {
    }

    public override void PhysicsUpdate() {
        enemy.MoveForward();
        enemy.TurnLeft();
        //        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        //enemy.MoveForward();
        //enemy.LookAtOtherObj(Vector3.zero);
        //enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, knockbackVector);
    }
}
