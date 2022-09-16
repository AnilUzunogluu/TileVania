using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    [SerializeField] private int PointsToAdd = 100;

    private bool wasCollected = false;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !wasCollected)
        {
            wasCollected = true;
            var gameSession = GameSession.Instance;
            gameSession.AddToScore(PointsToAdd);
            Destroy(gameObject);
        }
        
    }
}
