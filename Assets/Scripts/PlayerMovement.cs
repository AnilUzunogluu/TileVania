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
    [SerializeField] private Vector2 deathForce = new(10f, 10f);

    private float basegravity;
    public bool isClimbing;
    
    public event Action<bool> OnRun;
    public event Action<bool> OnClimb;
    public event Action OnDeath;
    public event Action OnShoot; 
    public event Action OnJumping;
    
    void Start()
    {
        playerrb = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<CircleCollider2D>();
        basegravity = playerrb.gravityScale;
    }
    
    private void FixedUpdate()
    {
        if (!isAlive) return;
        
        Run();
        Climb();
        Die();

    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed && feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && isAlive)
        {
            playerrb.velocity += new Vector2(0f, jumpSpeed);
            OnJumping?.Invoke();
        }
    }

    private void OnFire()
    {
        if (Isalive && GameSession.Instance.Score >= 200)
        {
            OnShoot?.Invoke();
            GameSession.Instance.Score -= 200;
            GameSession.Instance.DisplayTexts();
        }
        if (!isAlive) return;
        
    }

    void Run()
    {
        var playerVelocity = new Vector2(moveInput.x * moveSpeed, playerrb.velocity.y);
        playerrb.velocity = playerVelocity;
        OnRun?.Invoke(CheckHorizontalMovement());
    }

    void Climb()
    {
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            playerrb.gravityScale = basegravity;
            OnClimb?.Invoke(false);
            return;
        }
        playerrb.gravityScale = 0;
        Vector2 playerVelocity = new Vector2(playerrb.velocity.x, moveInput.y * moveSpeed);
        playerrb.velocity = playerVelocity;
        OnClimb?.Invoke(CheckVerticalMovement());
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
            FindObjectOfType<GameSession>().ComputePlayerLives();
        }
    }
}
