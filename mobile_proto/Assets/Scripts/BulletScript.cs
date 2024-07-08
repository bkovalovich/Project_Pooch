using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A basic bullet used by the player
public class BulletScript : MonoBehaviour
{
    [SerializeField] CurrentShipSO currentShip;
    private float bulletSpeed, lifetime;
    private Sprite s;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private CapsuleCollider2D capcollider;
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        capcollider = GetComponent<CapsuleCollider2D>();
    }
    private void Start() {
        if (currentShip == null) { Debug.LogError("BULLET COUDLN'T ACCESS CURRENTSHIPSO"); }
        bulletSpeed = currentShip.SelectedShip.BulletInfo.BulletSpeed;
        lifetime = currentShip.SelectedShip.BulletInfo.BulletLifetime;
        sr.sprite = currentShip.SelectedShip.BulletInfo.BulletSprite;
        capcollider.size = currentShip.SelectedShip.BulletInfo.HitboxSize;
    }
    void FixedUpdate() {
        rb.velocity = transform.up * bulletSpeed;

        lifetime -= Time.deltaTime;
        if (lifetime <= 0f) {
            Destroy(gameObject);    
        }
    }
}

