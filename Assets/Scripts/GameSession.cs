using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    public static GameSession Instance;
    [SerializeField] private int playerLives = 3;
    
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI scoreText;

    private PlayerMovement playerMovement;
    private int score;

    public int Score
    {
        get => score;
        set => score = value;
    }
    
    public event Action OnCoinPickup;
    public event Action OnEnemyHit;


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
    //Testing feature. Delete method later.
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadCurrentScene();
        }
    }

    public void ComputePlayerLives()
    {
        if (playerLives > 1)
        {
            StartCoroutine(TakeLife());

        }
        else
        {
            ResetGame();
        }
    }

    public void AddToScore(int addAmount)
    {
        OnCoinPickup?.Invoke();
        score += addAmount;
        DisplayTexts();
    }
    
    private IEnumerator TakeLife()
    {
        playerLives--;
        DisplayTexts();
        yield return  new WaitForSecondsRealtime(0.5f);
        LoadCurrentScene();
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

    public void EnemyHit()
    {
        OnEnemyHit?.Invoke();
    }
    
    public void ResetEvents()
    {
        OnCoinPickup = null;
        OnEnemyHit = null;
    }

    public void DisplayTexts()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }

    public void ResetGame()
    {
        LoadScene(0);
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        Destroy(gameObject);
    }
}
