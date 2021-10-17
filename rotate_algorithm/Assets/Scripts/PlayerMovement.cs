using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] public float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void keyboardMovement() {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 tempVect = new Vector3(h, v, 0);
        tempVect = tempVect.normalized * movementSpeed/* * Time.deltaTime*/;
        rb.MovePosition(transform.position + tempVect);
    }

        // Update is called once per frame
        void Update()
    {
        keyboardMovement();
    }
}
