using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
public class Player : MonoBehaviour
{
    public PlayerStateMachine stateMachine;
    [SerializeField] CurrentShipSO currentShipInfo;
    [SerializeField] public PersistentPlayerStatsSO persistentStats;
    public CurrentShipSO CurrentShipInfo => currentShipInfo;
    #region States
    public PlayerFlyingState playerFlyingState;
    public PlayerKnockbackState playerKnockbackState;
    #endregion
    #region Input
    public PlayerInputActions inputActions;
    private InputAction move;
    #endregion
    #region Ship Info
    private float speed, turnspeed;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    #endregion
    #region Bullet
    [SerializeField] GameObject bulletPrefab;
    private List<Weapon> weapons;
    #endregion
    #region Current
    private float currentInput;
    #endregion
    #region Knockback
    public Vector3 knockbackPoint;
    #endregion
    private void Awake() {
        stateMachine = new PlayerStateMachine();
        playerKnockbackState = new PlayerKnockbackState(this, stateMachine);
        playerFlyingState = new PlayerFlyingState(this, stateMachine);

        inputActions = new PlayerInputActions(); 

        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start() {
        speed = currentShipInfo.SelectedShip.Speed;
        turnspeed = currentShipInfo.SelectedShip.TurnSpeed;
        sr.sprite = currentShipInfo.SelectedShip.ShipSprite;
        weapons = new List<Weapon>();

        for (int i = 0; i < currentShipInfo.SelectedShip.Firepoints.Length; i++) {
            Weapon temp = gameObject.AddComponent<Weapon>();
            temp.IntializeWeapon(currentShipInfo.SelectedShip.RateOfFire, CurrentShipInfo.SelectedShip.Firepoints[i], bulletPrefab, true);
            weapons.Add(temp);
        }
        stateMachine.Initialize(playerFlyingState);
    }
    private void OnEnable() {
        move = inputActions.Player.Move;
        move.Enable();
        move.performed += StartedInput;
        move.canceled += EndedInput;
    }
    private void OnDisable() {
        move.Disable();
        move.performed -= StartedInput;
        move.canceled -= EndedInput;
    }
    private void StartedInput(InputAction.CallbackContext context) {
        currentInput = context.ReadValue<Vector2>().x;
    }
    private void EndedInput(InputAction.CallbackContext context) {
        currentInput = 0;
    }
    public void MoveShip(float currentSpeed) {
        rb.velocity = transform.up * currentSpeed;
        if (currentInput != 0) { transform.Rotate(new Vector3(0,0,-currentInput) * turnspeed /** Time.deltaTime*/); }
    }
    public void FireWeapons() {
        foreach (Weapon weapon in weapons) { weapon.FireWeapon(); }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Wall") {
            knockbackPoint = collision.ClosestPoint(transform.position);
            stateMachine.ChangeState(playerKnockbackState);
        }
    }
    
    public void ResetVelocity() {
        rb.velocity = Vector2.zero;
    }

    public void Knockback(Vector2 knockbackVector, float force) {
        Vector2 temp = knockbackVector * -force;
        rb.velocity = temp;
    }
    private void Update() {
        stateMachine.CurrentState.FrameUpdate();
    }
    private void FixedUpdate() {
        stateMachine.CurrentState.PhysicsUpdate();
    }


}
