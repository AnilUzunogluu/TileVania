using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private float delayTime = 0.5f;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            var currentLevel = SceneManager.GetActiveScene().buildIndex;
            var nextSceneIndex = currentLevel + 1;
            if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0;
            }

            FindObjectsOfType<CoinBehavior>().ApplyToAll(x =>
            {
                
                Destroy(x.gameObject);
            });
            Utilities.DelayedExecute(this, delayTime, () => GameSession.Instance.LoadScene(nextSceneIndex));
        }
    }
}
