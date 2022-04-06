using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager me;
    public static bool GameIsPaused = false;

    public bool isMenu;
    private bool doOnce;
    private bool isFading;

    public float fadeTime;

    private CanvasGroup cg;

    public Image mappingHolder;

    private void Awake()
    {
        if (me != null && me != this)
        {
            Destroy(gameObject);
        }
        me = this;
    }

    void Start()
    {
        doOnce = isMenu;
        cg = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape) || Input.GetButtonUp("Menu") && !isFading)
        {
            if (!isMenu)
                isMenu = true;
            else
                isMenu = false;
        }

        if(doOnce != isMenu)
        {
            doOnce = isMenu;
            if (isMenu)
            {
                cg.alpha = 1;
                Time.timeScale = 0f;
                GameIsPaused = true;
            }
            else
            {
                StartCoroutine(FadeCanvas(cg, 0f, fadeTime));
            }
        }
    }

    IEnumerator FadeCanvas(CanvasGroup cg, float endValue, float duration)
    {
        isFading = true;
        if (!isMenu)
        {
            Time.timeScale = 1f;
            GameIsPaused = false;
        }
        float elapsedTime = 0;
        float startValue = cg.alpha;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            cg.alpha = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            yield return null;
        }
        if (isMenu)
        {
            Time.timeScale = 0f;
            GameIsPaused = true;
        }
        isFading = false;
    }
}
