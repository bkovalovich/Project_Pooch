using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidFieldScript : MonoBehaviour
{

    public float rotateSpeed;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * rotateSpeed);

    }
}
