using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bullet;

    [SerializeField] private Transform spawnLocation;

    private PlayerMovement playerMovementScript;
    // Start is called before the first frame update
    void Start()
    {
        playerMovementScript = GetComponent<PlayerMovement>();
        playerMovementScript.OnShoot += Shoot;
    }

    private void Shoot()
    {
        Instantiate(bullet, spawnLocation.position, transform.rotation);
    }
}
