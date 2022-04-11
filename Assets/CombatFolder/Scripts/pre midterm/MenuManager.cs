using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public static MenuManager me;
    public static bool GameIsPaused = false;

    public bool isMenu; //menu going out
    private bool doOnce;
    private bool pressedArrow;
    private bool arrowDoOnce;
    private bool isFading;

    public float fadeTime;

    private CanvasGroup cg;

    public GameObject mappingHolder;

    private enum panels
    {
        home,
        settings,
        ctrlMap
    }
    private panels panelState;

    public List<Button> buttonList;
    public GameObject selectPrefab;
    private int choosenIndex;

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
        panelState = panels.home;
    }

    void Update()
    {
        if((Input.GetKeyUp(KeyCode.Escape) || Input.GetButtonUp("Menu")) && !isFading)
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
                StartCoroutine(FadeCanvas(cg, 1f, fadeTime));
                Time.timeScale = 0f;
                GameIsPaused = true;
            }
            else
            {
                StartCoroutine(FadeCanvas(cg, 0f, fadeTime));
                GameIsPaused = false;
            }
        }
        if (isMenu)
        {
            if(panelState == panels.home)
            {
                SelectPrefabUpdate();
                if (Input.GetAxis("VerticalArrow") != 0)
                    pressedArrow = true;
                else
                {
                    pressedArrow = false;
                }

                if (arrowDoOnce != pressedArrow)
                {
                    arrowDoOnce = pressedArrow;
                    if (pressedArrow)
                    {
                        if (Input.GetAxis("VerticalArrow") < 0)
                        {
                            choosenIndex += 1;
                        }
                        else if (Input.GetAxis("VerticalArrow") > 0)
                        {
                            choosenIndex -= 1;
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.DownArrow) && !isFading)
                {
                    choosenIndex += 1;
                }

                if (Input.GetKeyDown(KeyCode.UpArrow) && !isFading)
                {
                    choosenIndex -= 1;
                }

                if (choosenIndex < 0)
                    choosenIndex = 0;
                else if (choosenIndex > buttonList.Count - 1)
                    choosenIndex = buttonList.Count - 1;
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("AButton") && !isFading)
                {
                    buttonList[choosenIndex].onClick.Invoke();
                }
                if (Input.GetButtonDown("BButton") && !isFading)
                {
                    isMenu = false;
                }
            }
            else if(panelState == panels.ctrlMap)
            {
                if ((Input.GetButtonDown("BButton") || Input.GetKeyDown(KeyCode.B)) && !isFading)
                {
                    StartCoroutine(FadeCanvas(mappingHolder.GetComponent<CanvasGroup>(), 0f, fadeTime));
                    StartCoroutine(FadeCanvas(gameObject.transform.Find("Home").GetComponent<CanvasGroup>(), 1f, fadeTime));
                    panelState = panels.home;
                }
            }
            else if(panelState == panels.settings)
            {
                if ((Input.GetButtonDown("BButton") || Input.GetKeyDown(KeyCode.B)) && !isFading)
                {
                    StartCoroutine(FadeCanvas(gameObject.transform.Find("Home").GetComponent<CanvasGroup>(), 1f, fadeTime));
                    panelState = panels.home;
                }
            }
        }
    }

    private void SelectPrefabUpdate()
    {
        RectTransform rt = selectPrefab.GetComponent<RectTransform>();
        RectTransform buttonRT = buttonList[choosenIndex].GetComponent<RectTransform>();
        RectTransform textRT = buttonList[choosenIndex].GetComponentsInChildren<RectTransform>()[1];
        rt.anchoredPosition = new Vector3(rt.anchoredPosition.x, buttonRT.anchoredPosition.y, 0);
        selectPrefab.transform.Find("SB-L").GetComponent<RectTransform>().anchoredPosition = new Vector3(-textRT.sizeDelta.x / 2, 0, 0);
        selectPrefab.transform.Find("SB-R").GetComponent<RectTransform>().anchoredPosition = new Vector3(textRT.sizeDelta.x / 2 + 22, 0, 0);
    }

    IEnumerator FadeCanvas(CanvasGroup cg, float endValue, float duration)
    {
        isFading = true;
        if (!isMenu)
        {
            Time.timeScale = 1f;
            GameIsPaused = false; 
            choosenIndex = 0;
        }
        float elapsedTime = 0;
        float startValue = cg.alpha;
        while (elapsedTime < duration)
        {
            elapsedTime += .1f;
            cg.alpha = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            yield return null;
        }
        isFading = false;
    }
    public void resumeGame()
    {
        isMenu = false;
    }
    
    public void exitGame()
    {
        Application.Quit();
    }

    public void ShowCtrlMap()
    {
        StartCoroutine(FadeCanvas(mappingHolder.GetComponent<CanvasGroup>(), 1f, fadeTime));
        StartCoroutine(FadeCanvas(gameObject.transform.Find("Home").GetComponent<CanvasGroup>(), 0f, fadeTime));
        panelState = panels.ctrlMap;
    }

    public void ShowSetting()
    {
        StartCoroutine(FadeCanvas(gameObject.transform.Find("Home").GetComponent<CanvasGroup>(), 0f, fadeTime));
        panelState = panels.settings;
    }
}
