using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletSO")]
public class BulletInfoSO : ScriptableObject
{
    [SerializeField] bool homing; 
    [SerializeField] float bulletSpeed, bulletlifetime;
    [SerializeField] Sprite bulletSprite;
    [SerializeField] Vector2 hitboxSize;

    public float BulletSpeed => bulletSpeed;
    public float BulletLifetime => bulletlifetime;
    public Sprite BulletSprite => bulletSprite;
    public Vector2 HitboxSize => hitboxSize;
}
