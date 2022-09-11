using System;
using System.ComponentModel.Design;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerrb;
    private CapsuleCollider2D capsuleCollider;
    Vector2 moveInput;

    public float GetScaleX => Mathf.Sign(playerrb.velocity.x);

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float jumpSpeed = 5f;


    public event Action OnRun;
    public event Action OnClimb;
    
    
    // Start is called before the first frame update
    void Start()
    {
        playerrb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
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
        if (value.isPressed && capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
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
        if (!capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) return;
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
        return Mathf.Abs(playerrb.velocity.y) > Mathf.Epsilon;
    }
    
    
}
