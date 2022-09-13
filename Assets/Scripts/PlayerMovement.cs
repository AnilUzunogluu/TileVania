using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerrb;
    private CapsuleCollider2D bodyCollider;
    private CircleCollider2D feetCollider;
    Vector2 moveInput;
    private bool isAlive = true;

    public bool Isalive
    {
        get => isAlive;
        private set => isAlive = value;
    }

    public float GetScaleX => Mathf.Sign(playerrb.velocity.x);

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private Vector2 deathForce = new Vector2(10f, 10f);

    private float basegravity;
    public bool isClimbing;


    public event Action OnRun;
    public event Action OnClimb;
    public event Action OnClimbEnd;
    public event Action OnDeath;
    public event Action OnShoot;
    
    // Start is called before the first frame update
    void Start()
    {
        playerrb = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<CircleCollider2D>();
        basegravity = playerrb.gravityScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isAlive) return;
        
        Run();
        Climb();
        Die();

    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        //Maybe make it so that the player can jump on the ladders too?
        // bug bodycollider shoul be feet collider. Fix after testing.

        if (value.isPressed && bodyCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && isAlive)
        {
            playerrb.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void OnFire()
    {
        if (!isAlive) return;
        OnShoot?.Invoke();
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, playerrb.velocity.y);
        playerrb.velocity = playerVelocity;
        OnRun?.Invoke();
    }

    void Climb()
    {
        // bug bodycollider shoul be feet collider. and add No Friction material to collider. Fix after testing.

        if (!bodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            playerrb.gravityScale = basegravity;
            OnClimbEnd?.Invoke(); //temporary fix to the climbing animation bug
            return;
        }
        playerrb.gravityScale = 0;
        Vector2 playerVelocity = new Vector2(playerrb.velocity.x, moveInput.y * moveSpeed);
        playerrb.velocity = playerVelocity;
        OnClimb?.Invoke();

    }
    
    public bool CheckHorizontalMovement()
    {
        return Mathf.Abs(playerrb.velocity.x) > Mathf.Epsilon;
    }

    public bool CheckVerticalMovement()
    {
        return isClimbing = Mathf.Abs(playerrb.velocity.y) > Mathf.Epsilon;
    }

    private void Die()
    {
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies")) || 
            bodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazards")))
        {
            isAlive = false;
            OnDeath?.Invoke();
            playerrb.velocity = deathForce;
        }
    
    }
}
