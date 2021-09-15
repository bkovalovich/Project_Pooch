using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletprefab; 

    void Shoot() {
         // Vector3 firingPointOffset = new Vector3(-4, -4, 0); //whatever you want the offset to be.
         Instantiate(bulletprefab, firePoint.position /*+ firingPointOffset*/, firePoint.rotation);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }

   
}
