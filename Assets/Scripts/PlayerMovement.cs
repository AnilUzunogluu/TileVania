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

    public float GetScaleX => Mathf.Sign(playerrb.velocity.x);

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float jumpSpeed = 5f;

    private float basegravity;
    public bool isClimbing;


    public event Action OnRun;
    public event Action OnClimb;
    
    
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
        Run();
        Climb();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        //Maybe make it so that the player can jump on the ladders too?
        // bug bodycollider shoul be feet collider. Fix after testing.
        if (value.isPressed && bodyCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            playerrb.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, playerrb.velocity.y);
        playerrb.velocity = playerVelocity;
        OnRun?.Invoke();
    }

    void Climb()
    {
        // bug bodycollider shoul be feet collider. Fix after testing.
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            playerrb.gravityScale = 0;
            Vector2 playerVelocity = new Vector2(playerrb.velocity.x, moveInput.y * moveSpeed);
            playerrb.velocity = playerVelocity;
            OnClimb?.Invoke();
        }
        else
        {
            playerrb.gravityScale = basegravity;
        }

    }
    
    public bool CheckHorizontalMovement()
    {
        return Mathf.Abs(playerrb.velocity.x) > Mathf.Epsilon;
    }

    public bool CheckVerticalMovement()
    {
        return isClimbing = Mathf.Abs(playerrb.velocity.y) > Mathf.Epsilon;
    }
    
    
}
