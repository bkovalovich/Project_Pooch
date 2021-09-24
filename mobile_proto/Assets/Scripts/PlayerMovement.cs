using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    public Rigidbody2D rb;
    public Touch touch;
    public GameObject bulletPrefab;
    public Vector3 fingerPos;

    public float RotationSpeed = 5;
    private Quaternion lookRotation;
    private Vector3 direction;


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

    void touchScreenMovement() {
        if (Input.touchCount > 0) {
            touch = Input.GetTouch(0);

            Vector3 touchPos = new Vector3(touch.position.x, touch.position.y, 0);
            Vector3 direction = touchPos - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;

            //Vector3 lookPos = touchPos3 - transform.position;
            //lookPos.y = 0;
            //var rotation = Quaternion.LookRotation(lookPos);
            //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, RotationSpeed);
            // Debug.Log("Touching the screen");

            //direction = (touchPos3 - transform.position).normalized;

            //lookRotation = Quaternion.LookRotation(direction);
            //   transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, /*Time.deltaTime **/ RotationSpeed);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, step);


        }// else {
         //        Debug.Log("Not touching the screen");
         //    }
         //Debug.Log($"X: {touch.position.x}");
         //Debug.Log($"Y: {touch.position.y}");

    }
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
        //transform.Rotate(Vector3.forward);
       // touchTests();
       // keyboardMovement();
        touchScreenMovement();
    }
}
