using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Deploys and uses a weapon by the player
public class Weapon : MonoBehaviour
{
    private Vector3 firePoint;//Where the weapon is used
    private GameObject bulletPrefab;//The basic bullet that will be continuously used
    private float rateOfFire;//How often the bullet can be fired

    protected float currentRechargeTime = 0f;//Determines current time between weapon uses

    public void IntializeWeapon(float rateOfFire, Vector2 firePoint, GameObject bulletPrefab, bool playerBullet) {
        this.rateOfFire = rateOfFire;
        this.bulletPrefab = bulletPrefab;
        this.firePoint = firePoint;
        int layerName = 0;
        if (playerBullet) { layerName = LayerMask.NameToLayer("DetectPBullets"); } else { layerName = LayerMask.NameToLayer("DetectEBulletsl"); }
        this.bulletPrefab.gameObject.layer = layerName;
        /*        int LayerIgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
        gameObject.layer = LayerIgnoreRaycast;
         */
        //Debug.Log(firePoint);
    }

    private void ShootBullet() {
        Instantiate(bulletPrefab, transform.TransformPoint(firePoint), gameObject.transform.rotation);
    }
    public void FireWeapon() {
        if (currentRechargeTime >= rateOfFire) {
            currentRechargeTime = currentRechargeTime % rateOfFire;
            ShootBullet();
        }
    }

     public void FixedUpdate() {
        currentRechargeTime += Time.deltaTime;
    }

   
}
