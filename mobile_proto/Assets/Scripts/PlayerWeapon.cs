using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : Weapon
{
    public PlayerMovement playerScript;

     void Start() {
        playerScript = gameObject.GetComponent<PlayerMovement>();
    }

    new private void FixedUpdate(){
        currentRechargeTime += Time.deltaTime;
        if (currentRechargeTime >= rateOfFire) {
            currentRechargeTime = currentRechargeTime % rateOfFire;
            if (!playerScript.shieldIsUp) {
                Shoot(new Vector3(0, 0, 0));
            }
        }
    }
}
