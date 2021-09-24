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


    // Start is called before the first frame update
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

    public static float AngleInRad(Vector3 vec1, Vector3 vec2) {
        return Mathf.Atan2(vec2.y - vec1.y, vec2.x - vec1.x);
    }

    public float AngleInDeg(Vector3 vec1, Vector3 vec2) {
        return AngleInRad(vec1, vec2) * 180 / Mathf.PI;
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
    }

    void keyboardTurning() {
        if (Input.GetKey("a")) {
            transform.Rotate(Vector3.forward * rotateSpeed);
        }
        if (Input.GetKey("d")) {
            transform.Rotate(Vector3.back * rotateSpeed);
        }
    }




    // Update is called once per frame
    void Update()
    {
        //Vector3 tempVect = new Vector3(0, 0.05f, 0);
        //transform.position += transform.forward * speed* Time.deltaTime;
        //rb.MovePosition(transform.position + tempVect);
        //    transform.Translate(transform.forward * speed);
        // touchTests();
        // keyboardMovement();
        // touchScreenMovement();
        keyboardTurning();
        transform.position += transform.forward * movementSpeed;

    }
}
