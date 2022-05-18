using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {
    public Touch touch;//For
                       //touch tests
    public bool isLeftPressed;//Main variables for left and right movement with touchscreen
    public bool isRightPressed;
    private float currentInvincibilityTime = 0;//How much invincibility you have left
    public SpriteRenderer spriteRenderer;//For changing texture color
    public Color defaultColor;
    public static float health = 5;//Main health bar

    [SerializeField] public float amountOfInvincibleTimeOnHit;//How much invinicbility you get on hit
    [SerializeField] public float movementSpeed;//How fast you move forward
    [SerializeField] public float rotateSpeed;//How fast you turn

    [SerializeField] private AudioSource enemyBulletHitSFX;

    public bool shieldIsUp;

    //Shield fields
    private GameObject playerShield;

    //PROPERTIES
    public Vector3 PlayerPosition {
        get { return transform.position; }
    }

    //START()
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
        playerShield = gameObject.transform.GetChild(1).gameObject;

    }

    //DEBUG MOVEMENT
    void TouchTests() {
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

    public void ShieldPressed() {
        shieldIsUp = true;
    }

    public void ShieldNotPressed() {
        shieldIsUp = false;
    }

    public void turnShieldOn() {
        playerShield.SetActive(true);
    }
    public void turnShieldOff() {
        playerShield.SetActive(false);
    }

    //FINAL MOVEMENT
    public void touchscreenMovement() {
        if (isLeftPressed) {
            turnLeft();
        }
        if (isRightPressed) {
            turnRight();
        }
        if (shieldIsUp) {
            turnShieldOn();
        }
        else {
            turnShieldOff();
        }
    }

    void keyboardTurning() {
        if (Input.GetKey("a"))
        {
            transform.Rotate(Vector3.forward * rotateSpeed);
        }
        if (Input.GetKey("d"))
        {
            transform.Rotate(Vector3.back * rotateSpeed);
        }
        if (Input.GetKey("p"))
        {
            shieldIsUp = true;
            turnShieldOn();
        }
        else
        {
            shieldIsUp = false;
            turnShieldOff();
        }
    }

    //HEALTH MODIFIERS
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Wall") {
            health = 0;
        }
        if (currentInvincibilityTime <= 0 && (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")) {
            enemyBulletHitSFX.Play();
            loseHealth(1f);
            currentInvincibilityTime = amountOfInvincibleTimeOnHit;
        }
    }

    public void loseHealth(float damage) {
        health = health - damage;
    }

    public bool isInvincible() {
        return currentInvincibilityTime >= 0f;
    }

    void FixedUpdate()
    {  
        if (health <= 0) {
            SceneManager.LoadScene("GameOver");
        }
            if(isInvincible()) {
                currentInvincibilityTime -= Time.deltaTime;
                spriteRenderer.color = Color.red;
            } else {
            spriteRenderer.color = defaultColor;
            
        }
        transform.position += transform.up * Time.deltaTime * movementSpeed;
       // keyboardTurning();
        touchscreenMovement();
    }
}
