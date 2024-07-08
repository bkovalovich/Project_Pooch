using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] EnemyStatSO enemyStats;
    [SerializeField] GameObject bulletPrefab;
    [HideInInspector]
    #region State Machine
    public EnemyStateMachine stateMachine;
    public EnemyEscapingFromWallsState escapingFromWallsState;
    public EnemyState idleState, chasingState, destroyState;
    #endregion

    private float currentHealth, currentInvincibilityTime, currentSpeed;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private List<Weapon> weapons;
    [HideInInspector] public Vector3 wallhitPoint;

    private void Awake() {
        stateMachine = new EnemyStateMachine();
        escapingFromWallsState = new EnemyEscapingFromWallsState(this, stateMachine);
        idleState = SetState(enemyStats.IdleState, idleState);
        chasingState = SetState(enemyStats.ChasingState, chasingState);
        destroyState = SetState(enemyStats.DestroyState, destroyState);
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        stateMachine.Initialize(idleState);
        currentHealth = enemyStats.Health;
        currentSpeed = enemyStats.Speed;
        sr.sprite = enemyStats.ShipSprite;

        //for (int i = 0; i < enemyStats.Firepoints.Length; i++) {
        //    Weapon temp = gameObject.AddComponent<Weapon>();
        //    temp.IntializeWeapon(enemyStats.RateOfFire, enemyStats.Firepoints[i], bulletPrefab, false);
        //    weapons.Add(temp);
        //}
    }

    private EnemyState SetState(string s, EnemyState enemyState) {
        var type = EnemyStates.GetState(s);
        if (type != null || s != "None") { 
            enemyState = (EnemyState)Activator.CreateInstance(type, this, stateMachine);
            return enemyState;
        }
        return null; 
    }

    public void LookAtOtherObj(Vector3 otherPos) {
        Vector3 target = otherPos - transform.position;
        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * currentSpeed);
    }
    public void MoveForward() {
        //Debug.Log("move forward called");
        rb.velocity = transform.up * currentSpeed;
    }
    public void TurnLeft() {
        transform.Rotate(Vector3.forward * currentSpeed);
    }
    public void MoveTowardsPoint(Vector3 target) {
        rb.velocity = Vector2.MoveTowards(transform.position, target, currentSpeed);
    }
    public void FireWeapons() {
        foreach (Weapon weapon in weapons) { weapon.FireWeapon(); }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Bullet") {
            currentHealth--;
            if (currentHealth <= 0) { stateMachine.ChangeState(destroyState); }
        }
    }


    private void Update() {
        stateMachine.CurrentState.FrameUpdate();
    }
    private void FixedUpdate() {
        stateMachine.CurrentState.PhysicsUpdate();
    }
}
