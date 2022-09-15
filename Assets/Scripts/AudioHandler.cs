using System;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip coinPickup;
    [SerializeField] private AudioClip jump;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        FindObjectOfType<GameSession>().OnCoinPickup += PLayCoinPickupClip;

        FindObjectOfType<PlayerMovement>().OnJumping += PlayJumpClip;
        
    }

    void PLayCoinPickupClip()
    {
        audioSource.PlayOneShot(coinPickup);
    }

    void PlayJumpClip()
    {
        audioSource.PlayOneShot(jump);
    }
}
