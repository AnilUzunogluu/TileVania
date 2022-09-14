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
        playerMovementScript.OnClimb += SetClimbAnimationTrue;
        playerMovementScript.OnClimbEnd += SetClimbAnimationFalse;
        playerMovementScript.OnDeath += setDeathAnimation;
        playerMovementScript.OnShoot += SetShootingAnimation;
    }

    void SetRunAnimation()
    {
        playerAnimator.SetBool("isRunning", playerMovementScript.CheckHorizontalMovement());
        playerMovementScript.isClimbing = false;
    }
    
    void FlipSprite()
    {
        if (playerMovementScript.CheckHorizontalMovement())
        {
            transform.localScale = new Vector2(playerMovementScript.GetScaleX, 1f);
        }
    }
    
    void SetClimbAnimationTrue()
    {
        //Bug: if player gets out from the top of the ladder before climbing animaton ends they get stuck in the climbing animation.
        playerAnimator.SetBool("isClimbing", true);
    }
    void SetClimbAnimationFalse()
    {
        //Bug: if player gets out from the top of the ladder before climbing animaton ends they get stuck in the climbing animation.
        playerAnimator.SetBool("isClimbing", false);
    }
    
    void setDeathAnimation()
    {
        playerAnimator.SetTrigger("Dying");
    }

    void SetShootingAnimation()
    {
        playerAnimator.SetTrigger("Shooting");
    }
}
