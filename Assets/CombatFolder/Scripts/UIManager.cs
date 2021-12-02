using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private float timer;
    public float hideTime;
    public float fadeTime;
    private bool isHided;
    private CanvasGroup cg;
    
    void Start()
    {
        cg = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= hideTime & !isHided)
        {
            isHided = true;
            StartCoroutine(FadeCanvas(cg, 0f, fadeTime));
        }
        if (Input.anyKeyDown && 
            !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.E)))
        {
            timer = 0;
            isHided = false;
            StartCoroutine(FadeCanvas(cg, 1f, fadeTime));
        }
    }

    IEnumerator FadeCanvas(CanvasGroup cg, float endValue, float duration)
    {
        float elapsedTime = 0;
        float startValue = cg.alpha;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            cg.alpha = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            yield return null;
        }
    }
}
