using UnityEngine;
using System.Collections.Generic;

public class ShieldScript : MonoBehaviour {
    public Quaternion rotation;
    public GameObject player; //Main player object
    [SerializeField] private AudioSource bulletHitSFX;

    public float maxShieldHealth = 10;
    private float currentShieldHealth;
    public float shieldHealthRatio;
    public static bool shieldIsBroken = false;

    public float maxRechargeTime;
    public float shieldRechargePerFrame = 0.01f;

    private Renderer shieldSprite;

    private Color shieldColor;
    public float rfloat;
    public float gfloat;
    public float bfloat;
    public float afloat;
    //List<float> shieldColorValues;


    void Start() {
        player = GameObject.Find("Player");
        rotation = transform.rotation;
        currentShieldHealth = maxShieldHealth;
        shieldSprite = GetComponent<Renderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!shieldIsBroken) {
            if (collision.gameObject.CompareTag("EnemyBullet")) {
                currentShieldHealth--;
                bulletHitSFX.Play();
                Destroy(collision.gameObject);
            }
        }
    }

    private void PassiveShieldRecovery() {
        currentShieldHealth += shieldRechargePerFrame;
    }

    private void BrokenShieldRecovery() {
        if (currentShieldHealth < maxShieldHealth) {
            currentShieldHealth += shieldRechargePerFrame;
        } else {
            shieldIsBroken = false;
        }
    }
        
       
    void FixedUpdate() {
        transform.position = player.GetComponent<PlayerMovement>().PlayerPosition; //Stay with player
        transform.rotation = rotation;

        Debug.Log(currentShieldHealth);
        shieldHealthRatio = (currentShieldHealth / maxShieldHealth);
        shieldSprite.material.color = new Color(1, shieldHealthRatio, shieldHealthRatio, 1);
        //            sprite.color = new Color (1, 0, 0, 1); 



        rfloat = shieldSprite.material.color.r;
        gfloat = shieldSprite.material.color.g;
        bfloat = shieldSprite.material.color.b;
        afloat = shieldSprite.material.color.a;

        //Debug.Log($"rfloat: {rfloat}");
        //Debug.Log($"gfloat: {gfloat}");
        //Debug.Log($"bfloat: {bfloat}");
        //Debug.Log($"afloat: {afloat}");


        if (currentShieldHealth < 0.1f) {
            shieldIsBroken = true;
        }

        if (currentShieldHealth < maxShieldHealth && !shieldIsBroken) {
            PassiveShieldRecovery();
        } else {
            BrokenShieldRecovery();
        }

    }
}
