using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Stat SO")]
public class EnemyStatSO : ScriptableObject {
    [SerializeField] float speed, turnSpeed, rateOfFire, health, radarRange;
    [SerializeField] BulletInfoSO bulletInfoSO;
    [SerializeField] Sprite sprite;
    [SerializeField] Vector2[] firePoints; 
    [SerializeField] State idleState, chasingState, destroyState; 
    public float Speed => speed;
    public float TurnSpeed => turnSpeed;
    public float RateOfFire => rateOfFire;
    public float Health => health;
    public Sprite ShipSprite => sprite;
    public Vector2[] Firepoints => firePoints;
    public string IdleState {
        get { return idleState.ToString(); }
    }
    public string ChasingState {
        get { return chasingState.ToString(); }
    }
    public string DestroyState {
        get { return destroyState.ToString(); }
    }
}
