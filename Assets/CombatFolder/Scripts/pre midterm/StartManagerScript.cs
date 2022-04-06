using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManagerScript : MonoBehaviour
{
    public Image fadeImage;
    public float fadeTime;
    public void PressedStart()
    {
        StartCoroutine(PressStart(fadeImage, 1, fadeTime));
    }

    public void PressedExit()
    {
        Application.Quit();
    }

    IEnumerator PressStart(Image target, float endValue, float duration)
    {
        float elapsedTime = 0;
        float startValue = target.GetComponent<Image>().color.a;
        Color curColor = target.GetComponent<Image>().color;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            curColor.a = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            target.GetComponent<Image>().color = curColor;
            yield return null;
        }
        SceneManager.LoadScene(1);
    }
}
