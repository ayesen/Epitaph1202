using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInManager : MonoBehaviour
{
    private static FadeInManager me;
    public static FadeInManager Me
    {
        get
        {
            return me;
        }
    }
    private void Awake()
    {
        if(me != null && me != this)
        {
            Destroy(this.gameObject);
        }

        me = this;
    }
    void Start()
    {
        StartCoroutine(UIManager.Me.FadeCanvas(GetComponent<CanvasGroup>(), 0, 3));
    }

    public IEnumerator SceneLoadFadeCanvas(CanvasGroup cg, float endValue, float duration)
    {
        float elapsedTime = 0;
        float startValue = cg.alpha;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            cg.alpha = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            yield return null;
        }

        GameObject.Find("SceneLoader").GetComponent<SceneLoader>().LoadLevel(2);
    }
}
