using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    [SerializeField] private int PointsToAdd = 100;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            var gameSession = GameSession.Instance;
            gameSession.AddToScore(PointsToAdd);
            gameSession.IncrementCoin();
            Destroy(gameObject);
        }
        
    }
}
