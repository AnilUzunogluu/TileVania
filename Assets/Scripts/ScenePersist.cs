using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    // Start is called before the first frame update
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
}
