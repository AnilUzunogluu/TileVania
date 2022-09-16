using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    private void Awake()
    {
        int numOfScenePersists = FindObjectsOfType<ScenePersist>().Length;
        if (numOfScenePersists > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetScenePersist()
    {
        Destroy(gameObject);
    }
}
