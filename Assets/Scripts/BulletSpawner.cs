using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bullet;

    [SerializeField] private Transform spawnLocation;

    [SerializeField] private float bulletSpawnDelay = 0.5f;

    private PlayerMovement playerMovementScript;

    // Start is called before the first frame update
    void Start()
    {
        playerMovementScript = GetComponent<PlayerMovement>();
        playerMovementScript.OnShoot += DelayedShoot;
    }

    private void Shoot()
    {
        Instantiate(bullet, spawnLocation.position, transform.rotation);
    }

    private void DelayedShoot()
    {
        Invoke(nameof(Shoot), bulletSpawnDelay);
    }
}
