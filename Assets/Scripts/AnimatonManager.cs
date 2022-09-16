using UnityEngine;

public class AnimatonManager : MonoBehaviour
{
    
    private Animator playerAnimator;
    private PlayerMovement playerMovementScript;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerMovementScript = GetComponent<PlayerMovement>();
        playerMovementScript.OnRun += FlipSprite;
        playerMovementScript.OnRun += SetRunAnimation;
        playerMovementScript.OnClimb += SetClimbAnimation;
        playerMovementScript.OnDeath += SetDeathAnimation;
        playerMovementScript.OnShoot += SetShootingAnimation;
    }

    private void SetRunAnimation(bool value)
    {
        playerAnimator.SetBool("isRunning", value);
        playerMovementScript.isClimbing = false;
    }

    private void FlipSprite(bool value)
    {
        if (value)
        {
            transform.localScale = new Vector2(playerMovementScript.GetScaleX, 1f);
        }
    }

    private void SetClimbAnimation(bool value)
    {
        playerAnimator.SetBool("isClimbing", value);
    }


    private void SetDeathAnimation()
    {
        playerAnimator.SetTrigger("Dying");
    }

    private void SetShootingAnimation()
    {
        playerAnimator.SetTrigger("Shooting");
    }
}
