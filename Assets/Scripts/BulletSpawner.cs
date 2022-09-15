using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bullet;

    [SerializeField] private Transform spawnLocation;

    [SerializeField] private float bulletSpawnDelay = 0.1f;

    private PlayerMovement playerMovementScript;

    void Start()
    {
        playerMovementScript = GetComponent<PlayerMovement>();
        playerMovementScript.OnShoot += () => {Invoke(nameof(Shoot), bulletSpawnDelay);};
    }
    
    void Shoot()
    {
        Utilities.DelayedExecute(this, bulletSpawnDelay,
            () => Instantiate(bullet, spawnLocation.position, transform.rotation));
    }

    
    
    
}
