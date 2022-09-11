using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatonManager : MonoBehaviour
{
    
    private Animator playerAnimator;
    private PlayerMovement playerMovementScript;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerMovementScript = GetComponent<PlayerMovement>();
        playerMovementScript.OnRun += FlipSprite;
        playerMovementScript.OnRun += SetRunAnimation;
        playerMovementScript.OnClimb += SetClimbAnimation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
    }

    void SetRunAnimation()
    {
        playerAnimator.SetBool("isRunning", playerMovementScript.CheckHorizontalMovement());
    }
    
    void FlipSprite()
    {
        if (playerMovementScript.CheckHorizontalMovement())
        {
            transform.localScale = new Vector2(playerMovementScript.GetScaleX, 1f);
        }
    }

    void SetClimbAnimation()
    {
        playerAnimator.SetBool("isClimbing", playerMovementScript.CheckVerticalMovement());
    }
}
