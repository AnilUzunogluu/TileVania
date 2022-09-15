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
        playerMovementScript.OnDeath += setDeathAnimation;
        playerMovementScript.OnShoot += SetShootingAnimation;
    }

    void SetRunAnimation(bool value)
    {
        playerAnimator.SetBool("isRunning", value);
        playerMovementScript.isClimbing = false;
    }
    
    void FlipSprite(bool value)
    {
        if (value)
        {
            transform.localScale = new Vector2(playerMovementScript.GetScaleX, 1f);
        }
    }
    
    void SetClimbAnimation(bool value)
    {
        //Bug: if player gets out from the top of the ladder before climbing animaton ends they get stuck in the climbing animation.
        playerAnimator.SetBool("isClimbing", value);
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
