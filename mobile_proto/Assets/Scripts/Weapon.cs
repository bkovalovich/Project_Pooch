using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletprefab;
    private float elapsed = 0f;
    [SerializeField] float rateOfFire;

    void Shoot(Vector3 offset) {
         // Vector3 firingPointOffset = new Vector3(-4, -4, 0); //whatever you want the offset to be.
         Instantiate(bulletprefab, firePoint.position + offset, firePoint.rotation);
    }

    void FixedUpdate() {
        elapsed += Time.deltaTime;
        if (elapsed >= rateOfFire) {
            elapsed = elapsed % rateOfFire;
            Shoot(new Vector3(0, 0, 0));
            //Shoot(new Vector3(0,.2f,0));
        }
    }

   
}
