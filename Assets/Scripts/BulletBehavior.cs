using UnityEngine;

public class BulletBehavior : MonoBehaviour

{
    [SerializeField] private float bulletSpeed = 1f;
    
    private Rigidbody2D bulletrb;
    private float xSpeed;
    private PlayerMovement player;

    private void Start()
    {
        bulletrb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        Flip();
        xSpeed = player.transform.localScale.x * bulletSpeed;        

    }

    private void Update()
    {
        bulletrb.velocity = new Vector2(xSpeed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
    }

    void Flip()
    {
        if (player.transform.localScale.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}
