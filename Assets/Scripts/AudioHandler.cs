using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip coinPickup;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip shotFired;
    [SerializeField] private AudioClip gotHit;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        GameSession.Instance.OnCoinPickup += PLayCoinPickupClip;
        GameSession.Instance.OnEnemyHit += PlayEnemyHitClip;

        FindObjectOfType<PlayerMovement>().OnJumping += PlayJumpClip;
        FindObjectOfType<PlayerMovement>().OnShoot += PlayShootClip;


    }

    void PLayCoinPickupClip()
    {
        audioSource.PlayOneShot(coinPickup);
    }

    void PlayJumpClip()
    {
        audioSource.PlayOneShot(jump);
    }

    void PlayShootClip()
    {
        audioSource.PlayOneShot(shotFired);
    }

    void PlayEnemyHitClip()
    {
        audioSource.PlayOneShot(gotHit);

    }
}
