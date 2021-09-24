using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public Rigidbody2D rb;
    public Touch touch;
    public GameObject bulletPrefab;
    public Vector3 fingerPos;

    /*[SerializeField] */public float speed = 5;
    private Quaternion lookRotation;
    private Vector3 direction;
    public float iter = 0;
    public float rotateSpeed = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        //ParentGameObject.transform.GetChild (1).gameObject;
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

    private void PlayerLookAtClick(Vector3 screenTouch) {
        // calculate displacement vector (relative Position) between player position and touch position
      //  Camera camera = transform.Find("Main Camera");
        Vector3 displacementVector = screenTouch - Camera.main.WorldToScreenPoint(transform.position);


        // Since the player should not be upside down, you have to check whether you tap on the left or right side of the player and then rotate the player accordingly around its own axis

        // The calculated look direction in euler angles
        Vector3 preLookRotation = Quaternion.LookRotation(displacementVector).eulerAngles;

        // if tap right
        if (displacementVector.x > 0)
            transform.rotation = Quaternion.Euler(preLookRotation.x, 90, 0);
        // if tap left
        else
            transform.rotation = Quaternion.Euler(preLookRotation.x, -90, 0);
    }

    void touchScreenMovement() {
        if (Input.touchCount > 0) {
            //    touch = Input.GetTouch(0);
            PlayerLookAtClick(Input.mousePosition);
        }
    }

        
       
        //rb.rotation = iter;
        //iter++;

        //float rotation;
        //if (transform.eulerAngles.x <= 180f) {
        //    rotation = transform.eulerAngles.x;
        //} else {
        //    rotation = transform.eulerAngles.x - 360f;
        //}

        //Debug.Log(rotation);
        //if(iter <= 30) {
        //    transform.Rotate(Vector3.back * rotateSpeed);
        //    iter = iter + rotateSpeed;
        //}

    void keyboardMovement() {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 tempVect = new Vector3(h, v, 0);
        tempVect = tempVect.normalized * speed/* * Time.deltaTime*/;
        rb.MovePosition(transform.position + tempVect);
    }

    // Update is called once per frame
    void Update()
    {
       // touchTests();
       // keyboardMovement();
        touchScreenMovement();
    }
}
