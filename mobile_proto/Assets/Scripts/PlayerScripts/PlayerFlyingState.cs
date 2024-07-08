using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlyingState : PlayerState {
    private float speed; 
    public PlayerFlyingState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine) {
        speed = player.CurrentShipInfo.SelectedShip.Speed;
    }
    public override void EnterState() {
        
    }

    public override void ExitState() {
    }

    public override void FrameUpdate() {
    }

    public override void PhysicsUpdate() {
        player.MoveShip(speed);
        player.FireWeapons();
    }
}
