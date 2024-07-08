using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInfoSO")]
public class PlayerInfoSO : ScriptableObject {
    [SerializeField] float speed, turnSpeed, rateOfFire;
    [SerializeField] Sprite sprite;
    
    [SerializeField] BulletInfoSO bullet;
    [SerializeField] Vector2[] firepoints;
    #region Ship Properties
    public float Speed => speed;
    public float TurnSpeed => turnSpeed;
    public float RateOfFire => rateOfFire;
    public Sprite ShipSprite => sprite;
    #endregion

    #region Bullet Properties
    public BulletInfoSO BulletInfo => bullet;
    public Vector2[] Firepoints => firepoints;
    #endregion
}
