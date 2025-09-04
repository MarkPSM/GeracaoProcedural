using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    private static SceneLoader singletonInstance;

    private bool isLoadingInProgress;

    public static SceneLoader Instance
    {
        get
        {
            if (singletonInstance == null)
            {
                var go = new GameObject("[SceneLoader]");

                singletonInstance = go.AddComponent<SceneLoader>();

                DontDestroyOnLoad(go);
            }

            return singletonInstance;
        }
    }

    public static void Load(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName)) return;

        if (Instance.isLoadingInProgress) return;

        Instance.StartCoroutine(Instance.LoadRoutine(sceneName));
    }

     private IEnumerator LoadRoutine(string targetSceneName)
    {
        isLoadingInProgress = true;

        var loadOperation = SceneManager.LoadSceneAsync(targetSceneName, LoadSceneMode.Single);

        loadOperation.allowSceneActivation = true;

        while (!loadOperation.isDone)
        {
            yield return null;
        }

        yield return null;

        isLoadingInProgress = false;
    }
}
