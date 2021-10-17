using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {
    public Rigidbody2D rb;
    public Touch touch;
    public bool isLeftPressed;
    public bool isRightPressed;
    [SerializeField] public int health;
    [SerializeField] public float movementSpeed;
    [SerializeField] public float rotateSpeed;

    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
    }

    //DEBUG MOVEMENT
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
        transform.position += transform.up * Time.deltaTime * movementSpeed;
    }

    //INTERFACES WITH TOUCHSCREEN BUTTONS
    public void leftPressed() {
        isLeftPressed = true;
    }
    public void leftNotPressed() {
        isLeftPressed = false;
    } 
    public void rightPressed() {
        isRightPressed = true;
    }
    public void rightNotPressed() {
        isRightPressed = false;
    }

    //VERTICAL MOVEMENT
    public void turnLeft() {
        transform.Rotate(Vector3.forward * rotateSpeed);
    }
    public void turnRight() {
        transform.Rotate(Vector3.back * rotateSpeed);
    }

    //FINAL MOVEMENT
    public void touchscreenMovement() {
        if (health > 0) {
            transform.position += transform.up * Time.deltaTime * movementSpeed;
        }
        if (isLeftPressed) {
            turnLeft();
        }
        if (isRightPressed) {
            turnRight();
        }
    }

    //HEALTH MODIFIERS
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            health--;
        }
    }
    public void loseHealth() {
        health--;
    }

    void FixedUpdate()
    {
        if (health <= 0) {
            SceneManager.LoadScene("GameOver");
        } else {
            touchscreenMovement();
           // keyboardMovement();
        }
    }
}
