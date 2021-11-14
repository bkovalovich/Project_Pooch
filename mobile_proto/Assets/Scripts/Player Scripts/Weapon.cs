using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Deploys and uses a weapon by the player
public class Weapon : MonoBehaviour
{
    public Transform firePoint;//Where the weapon is used
    public GameObject bulletprefab;//The basic bullet that will be continuously used
   // public AudioSource source; //Sound effect for shooting
    private float currentRechargeTime = 0f;//Determines current time between weapon uses
    [SerializeField] public float rateOfFire;//How often the bullet can be fired

    public float RateOfFire {
        get { return rateOfFire; }
        set { rateOfFire = value; }
    }

    //Start()
    //Gets audio source
    void Start() {
       // source = GetComponent<AudioSource>();
    }

    //Shoot()
    //Creates bullet
    public void Shoot(Vector3 offset) {
         Instantiate(bulletprefab, firePoint.position + offset, firePoint.rotation);
    }

    //FixedUpdate()
    //Shoots bullets at a rate determined by float rateOfFire
     public void FixedUpdate() {
        currentRechargeTime += Time.deltaTime;
        if (currentRechargeTime >= rateOfFire) {
            currentRechargeTime = currentRechargeTime % rateOfFire;
            Shoot(new Vector3(0, 0, 0));
            //source.Play();
        }
    }

   
}
