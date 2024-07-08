using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockbackState : PlayerState {
    private float time, force;
    private float currentTime, currentForce;
    private Vector2 knockbackVector; 
    public PlayerKnockbackState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine) {
        time = player.persistentStats.KnockbackTime;
        force = player.persistentStats.KnockbackForce;

    }
    public override void EnterState() {
        currentTime = 0;
        currentForce = force;
        player.ResetVelocity();
        knockbackVector = (player.knockbackPoint - player.transform.position);
    }

    public override void ExitState() {
    }

    public override void FrameUpdate() {
    }

    public override void PhysicsUpdate() {
        if(currentTime < time) {
            currentTime += Time.deltaTime;
            currentForce -= 2.7f;
            player.Knockback(knockbackVector, currentForce);
        } else {
            playerStateMachine.ChangeState(player.playerFlyingState);
        }
    }
}
