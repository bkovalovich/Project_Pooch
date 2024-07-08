using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicChaseState : EnemyState {
    public EnemyBasicChaseState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine) {
       // Debug.Log("Basic Chase State init");

    }
    public override void EnterState() {
        Debug.Log($"entered {this.GetType()}");
    }

    public override void ExitState() {
    }

    public override void FrameUpdate() {
    }

    public override void PhysicsUpdate() {

    }
}
