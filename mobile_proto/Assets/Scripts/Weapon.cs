using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletprefab;
    private float elapsed = 0f;
    public AudioSource source;
    [SerializeField] float rateOfFire;

    void Start() {
        source = GetComponent<AudioSource>();
    }

    void Shoot(Vector3 offset) {
         // Vector3 firingPointOffset = new Vector3(-4, -4, 0); //whatever you want the offset to be.
         Instantiate(bulletprefab, firePoint.position + offset, firePoint.rotation);
    }

    void FixedUpdate() {
        elapsed += Time.deltaTime;
        if (elapsed >= rateOfFire) {
            elapsed = elapsed % rateOfFire;
            Shoot(new Vector3(0, 0, 0));
            source.Play();
        }
    }

   
}
