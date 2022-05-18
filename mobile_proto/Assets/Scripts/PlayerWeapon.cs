using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : Weapon
{

    // Update is called once per frame
    new private void FixedUpdate()
    {
        currentRechargeTime += Time.deltaTime;
        if (currentRechargeTime >= rateOfFire) {
            currentRechargeTime = currentRechargeTime % rateOfFire;
            if (!ShieldScript.shieldIsUp) {
                Shoot(new Vector3(0, 0, 0));
            }
        }
    }
}
