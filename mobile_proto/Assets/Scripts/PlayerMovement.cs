using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

enum PlayerState {
    Hittable, 
    Invincible
}
public class PlayerMovement : MonoBehaviour {
    public Rigidbody2D rb;
    public Touch touch;
    public bool isLeftPressed;
    public bool isRightPressed;
    private float currentInvincibilityTime = 0;
    public SpriteRenderer spriteRenderer;
    public Color defaultColor;

    public static float health = 5;

    [SerializeField] public float amountOfInvincibleTimeOnHit;
    [SerializeField] public float movementSpeed;
    [SerializeField] public float rotateSpeed;

    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //Color newColor = new Vector4(0.3f, 0.4f, 0.6f);
         defaultColor = spriteRenderer.color;
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
        tempVect = tempVect.normalized * 0.1f;
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
        if(collision.gameObject.tag == "Wall") {
            health = 0;
        }
        if (currentInvincibilityTime <= 0 && (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")) {
            loseHealth(1f);
            currentInvincibilityTime = amountOfInvincibleTimeOnHit;
        }
    }

    public void loseHealth(float damage) {
        health = health - damage;
    }

    void FixedUpdate()
    {  
        if (health <= 0) {
            SceneManager.LoadScene("GameOver");
        } else {
            if(currentInvincibilityTime >= 0f) {
                currentInvincibilityTime -= Time.deltaTime;
                spriteRenderer.color = Color.red;
            } else {
                spriteRenderer.color = defaultColor;
        }
        //touchscreenMovement();
       // keyboardMovement();
            keyboardTurning();
        }
    }
}
