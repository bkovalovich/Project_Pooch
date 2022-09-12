using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : Weapon
{

    new private void FixedUpdate(){
        currentRechargeTime += Time.deltaTime;
        if (currentRechargeTime >= rateOfFire) {
            currentRechargeTime = currentRechargeTime % rateOfFire;
            if (!PlayerMovement.shieldIsUp && !PlayerMovement.startMovingTowardsPortal) {
                Shoot(new Vector3(0, 0, 0));
            }
        }
    }
}
