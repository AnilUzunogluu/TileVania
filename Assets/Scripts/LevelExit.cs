using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private float delayTime = 1f;

    private void OnTriggerEnter2D(Collider2D col)
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        var currentLevel = SceneManager.GetActiveScene().buildIndex;
        var nextSceneIndex = currentLevel + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;

        }
        
        yield return new WaitForSecondsRealtime(delayTime);
        
        SceneManager.LoadScene(nextSceneIndex);
    }
}
