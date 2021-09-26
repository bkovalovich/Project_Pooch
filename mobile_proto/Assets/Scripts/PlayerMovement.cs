using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public Rigidbody2D rb;
    public Touch touch;
    public GameObject bulletPrefab;
    public Vector3 fingerPos;

    [SerializeField] public float movementSpeed;
    [SerializeField] public float rotateSpeed;

    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
    }
    void touchTests() {
        if (Input.touchCount > 0) {
            touch = Input.GetTouch(0);
            switch (touch.phase) {
                case TouchPhase.Began:
                    Debug.Log("finger was just put down");//YUP
                    break;
                case TouchPhase.Ended:
                    Debug.Log("finger was just removed");//YUP
                    break;
                case TouchPhase.Moved:
                    Debug.Log("finger was already down and has moved");//YUP
                    break;
                case TouchPhase.Stationary:
                    Debug.Log("finger was already down and hasn't moved");//YUP
                    break;
                case TouchPhase.Canceled:
                    Debug.Log("touch was canceled by the system");//NO
                    break;
                default:
                    break;
            }
        } else {
            Debug.Log("Not touching the screen");//YUP
        }
    }

    void touchScreenMovement() {
        if (Input.touchCount > 0) {
               touch = Input.GetTouch(0);
        }
    }
    void keyboardMovement() {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 tempVect = new Vector3(h, v, 0);
        tempVect = tempVect.normalized * movementSpeed/* * Time.deltaTime*/;
        rb.MovePosition(transform.position + tempVect);
        if (Input.GetKey("q")) {
            transform.Rotate(Vector3.forward * rotateSpeed);
        }
        if (Input.GetKey("e")) {
            transform.Rotate(Vector3.back * rotateSpeed);
        }
    }
    Vector3 getPos() {
        return transform.position;
    }
    void keyboardTurning() {
        if (Input.GetKey("a")) {
            transform.Rotate(Vector3.forward * rotateSpeed);
        }
        if (Input.GetKey("d")) {
            transform.Rotate(Vector3.back * rotateSpeed);
        }
        transform.position += transform.up * Time.deltaTime * movementSpeed;
    }

    void FixedUpdate()
    {
         keyboardTurning();
    }
}
