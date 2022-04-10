using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager me;
    public static bool GameIsPaused = false;

    public bool isMenu; //menu going out
    private bool doOnce;
    private bool isFading;

    public float fadeTime;

    private CanvasGroup cg;

    public Image mappingHolder;
    //UI Panel Dragdown
    public int uiIndex; //which UI panel ur in;
    public int buttonIndex; //which button player in when in uiIndex = 0;
    public GameObject mappingPanel; //uiIndex = 1;
    public GameObject pausePanel; //uiIndex = 0;
    public GameObject resumeButton; //buttonIndex = 0;
    public GameObject controlButton; //buttonIndex = 1;
    public GameObject exitButton; //buttonIndex = 2;
    public bool inInput; //use for restrict player dpad input, once per only;
    public bool isReset; // for reseting UI Panel Index;
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
            {
                isMenu = true; 
                isReset = true; //UI Index
            }
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

        //从下面这段都是和UI Index有关的，Line73 - 115 not yet working,交给你了！
        if (isMenu)
        {
            //UI Index: Pause Menu == 0, Controller Mapping == 1;
            //Button Index: Resume Button == 0, Control Button == 1, Exit Button ==2;
            //bool for detect
            if (isReset)
            {
                uiIndex = 0;
                buttonIndex = 0;
                inInput = false;
                isReset = false;
            }
            
            
            Debug.Log(Input.GetAxis("VerticalArrow"));
            Debug.Log("ininput: " + inInput);
            Debug.Log("Button index:" + buttonIndex);
            
            if (Input.GetAxis("VerticalArrow") < 0)
            {
                inInput = true;
            }
            else if (Input.GetAxis("VerticalArrow") > 0)
            {
                inInput = true;
            }
            else
            {
                inInput = false;
            }
            
            if (buttonIndex != 2 && Input.GetAxis("VerticalArrow") < 0f)
            {
                buttonIndex += 1;
                inInput = false;
            }
            if (buttonIndex != 0 && Input.GetAxis("VerticalArrow") > 0f)
            {
                buttonIndex -= 1;
                inInput = false;
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
    
    public void pauseGame()
    {
        Time.timeScale = 0f;
    }

    public void showMapping()
    {
        pausePanel.SetActive(false);
        mappingPanel.SetActive(true);
    }
    public void resumeGame()
    {
        isMenu = false;
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
