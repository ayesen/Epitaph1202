using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsych(sceneIndex));
        //how to use this: GameObject.Find("SceneLoader").GetComponent<SceneLoader>().LoadLevel( );
        //make sure there is SceneLoader in the level
    }

    IEnumerator LoadAsych(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            Debug.Log(progress);

            slider.value = progress;

            yield return null;
        }
    }
}
