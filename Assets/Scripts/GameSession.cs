using System;
using System.Collections;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    public static GameSession Instance;
    [SerializeField] private int playerLives = 3;
    [SerializeField] private int coins;
    
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI scoreText;

    private PlayerMovement playerMovement;
    private int score = 0;
    
    public event Action OnCoinPickup;

    private void Awake()
    {
        int numOfSessions = FindObjectsOfType<GameSession>().Length;
        if (numOfSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }

    public void ComputePlayerLives()
    {
        if (playerLives > 1)
        {
            StartCoroutine(TakeLife());

        }
        else
        {
            LoadScene(0);
            var scenePersist = FindObjectOfType<ScenePersist>();
            Destroy(scenePersist.gameObject);
            Destroy(gameObject);
        }
    }

    public void AddToScore(int addAmount)
    {
        score += addAmount;
        scoreText.text = score.ToString();
    }
    
    private IEnumerator TakeLife()
    {
        playerLives--;
        livesText.text = playerLives.ToString();
        yield return  new WaitForSecondsRealtime(0.5f);
        LoadCurrentScene();
    }

    public void IncrementCoin()
    {
        coins++;
        OnCoinPickup?.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadCurrentScene();
        }
    }

    public void ResetEvents()
    {
        OnCoinPickup = null;
    }

    public void LoadCurrentScene()
    {
        var scene = SceneManager.GetActiveScene().buildIndex;
        LoadScene(scene);
    }

    public void LoadScene(int value)
    {
        ResetEvents();
        SceneManager.LoadScene(value);
    }
}
