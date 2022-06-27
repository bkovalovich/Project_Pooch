using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryWeapon : MonoBehaviour {

    public Transform firePoint;//Where the weapon is used
    public GameObject bulletprefab;//The basic bullet that will be continuously used
    private bool isCoolingDown = false;//Whether you can shoot at the moment
    private float currentRechargeTime = 0f;//Determines current time between weapon uses
    [SerializeField] public float maxRechargeTime;//Amount of time between shots
    [SerializeField] private AudioSource chargeShotSFX;

    private PlayerMovement playerScript;


    void Start() {
        playerScript = gameObject.GetComponent<PlayerMovement>();

    }

    //IsCoolingDown
    //Returns isCoolingDown
    public bool IsCoolingDown {
        get { return isCoolingDown; }
    }

    //RechargeRatio
    //Returns ratio corresponding to how long recharge will take, used for refill on button
    public float RechargeRatio {
        get { return currentRechargeTime / maxRechargeTime; }
    }

    //Shoot()
    //Creates bullet
    public void Shoot(Vector3 offset) {
        Instantiate(bulletprefab, firePoint.position + offset, firePoint.rotation);
    }

    //ChargedPressed()
    //Calls Shoot() and starts the cooldown period so it can't be called for the length of field maxRechargeTime
    public void ChargedPressed() {
        if (isCoolingDown == false && !playerScript.shieldIsUp) {
            chargeShotSFX.Play();
            Shoot(new Vector3(0, 0, 0));
            currentRechargeTime = maxRechargeTime;
            isCoolingDown = true;
        }
    }

    //KeyboardShooting()
    //For testing and PC port
    public void KeyboardShooting() {
        if (Input.GetKey("o")) {
            ChargedPressed();
        }
    }

    //FixedUpdate()
    //Counts down the recharge time according to field maxRechargeTime
    public void FixedUpdate() {
        KeyboardShooting();
        isCoolingDown = currentRechargeTime <= 0 ? false : true;
        currentRechargeTime -= isCoolingDown ? Time.deltaTime : 0;
    } 

}
