using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StartManagerScript : MonoBehaviour
{
    public static StartManagerScript me;
    private bool pressedArrow;
    private bool arrowDoOnce;
    [Header("Fading Settings")]
    private bool isFading;
    private bool startGame;
    private bool creditScene;
    public float fadeTime;

    [Header("PanelHolders")]
    public GameObject mappingHolder;
    public GameObject settingsHolder;
    public GameObject fadeImage;
    public SceneLoader sceneLoader;

    private enum panels
    {
        home,
        settings,
        ctrlMap
    }
    private panels panelState;

    [Header("Selection")]
    public List<Button> buttonList;
    public GameObject selectPrefab;
    public float Y_Space;
    public float lerpSpd;
    private Vector2 offset;
    
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
        panelState = panels.home;
        offset = new Vector2(0, Y_Space);
        choosenIndex = 2;
    }

    void Update()
    {
        if (panelState == panels.home)
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
                        SoundMan.SoundManager.SafehouseMaterialSelect();
                    }
                    else if (Input.GetAxis("VerticalArrow") > 0)
                    {
                        choosenIndex -= 1;
                        SoundMan.SoundManager.SafehouseMaterialSelect();
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && !isFading)
            {
                choosenIndex += 1;
                SoundMan.SoundManager.SafehouseMaterialSelect();
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) && !isFading)
            {
                choosenIndex -= 1;
                SoundMan.SoundManager.SafehouseMaterialSelect();
            }

            if (choosenIndex < 0)
                choosenIndex = 0;
            else if (choosenIndex > buttonList.Count - 1)
                choosenIndex = buttonList.Count - 1;

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("AButton") && !isFading)
            {
                buttonList[choosenIndex].onClick.Invoke();
                SoundMan.SoundManager.SafehouseMaterialSelect();
            }
        }
        else if (panelState == panels.ctrlMap)
        {
            if ((Input.GetButtonDown("BButton") || Input.GetKeyDown(KeyCode.B)) && !isFading)
            {
                StartCoroutine(FadeCanvas(mappingHolder.GetComponent<CanvasGroup>(), 0f, fadeTime));
                StartCoroutine(FadeCanvas(gameObject.transform.Find("Home").GetComponent<CanvasGroup>(), 1f, fadeTime));
                panelState = panels.home;
                SoundMan.SoundManager.SafehouseMaterialSelect();
            }
        }
        else if (panelState == panels.settings)
        {
            if ((Input.GetButtonDown("BButton") || Input.GetKeyDown(KeyCode.B)) && !isFading)
            {
                StartCoroutine(FadeCanvas(settingsHolder.GetComponent<CanvasGroup>(), 0f, fadeTime));
                StartCoroutine(FadeCanvas(gameObject.transform.Find("Home").GetComponent<CanvasGroup>(), 1f, fadeTime));
                panelState = panels.home;
                SoundMan.SoundManager.SafehouseMaterialSelect();
            }
        }
    }

    private void SelectPrefabUpdate()
    {
        RectTransform rt = selectPrefab.GetComponent<RectTransform>();
        RectTransform buttonRT = buttonList[choosenIndex].GetComponent<RectTransform>();
        RectTransform textRT = buttonList[choosenIndex].GetComponentsInChildren<RectTransform>()[1];
        rt.anchoredPosition = new Vector3(rt.anchoredPosition.x, buttonRT.anchoredPosition.y, 0);
        //selectPrefab.transform.Find("SB-L").GetComponent<RectTransform>().anchoredPosition = new Vector3(-textRT.sizeDelta.x / 2, 0, 0);
        //selectPrefab.transform.Find("SB-R").GetComponent<RectTransform>().anchoredPosition = new Vector3(textRT.sizeDelta.x / 2 + 22, 0, 0);

        /*
        RectTransform buttonRT = buttonList[choosenIndex].GetComponent<RectTransform>();
        RectTransform SBRT = selectPrefab.GetComponent<RectTransform>();
        buttonRT.anchoredPosition = Vector2.Lerp(buttonRT.anchoredPosition, SBRT.anchoredPosition, lerpSpd * Time.deltaTime);

        for(int i = 0; i >= -choosenIndex; i--)
        {
            buttonList[choosenIndex - i].GetComponent<RectTransform>().anchoredPosition =
            Vector2.Lerp(buttonList[choosenIndex - i].GetComponent<RectTransform>().anchoredPosition, SBRT.anchoredPosition + (offset * ), lerpSpd * Time.deltaTime);
        }


        if (choosenIndex - 1 >= 0)
        {
            RectTransform prevRT = buttonList[choosenIndex - 1].GetComponent<RectTransform>();
            prevRT.anchoredPosition = Vector2.Lerp(prevRT.anchoredPosition, SBRT.anchoredPosition + offset, lerpSpd * Time.deltaTime);
            Debug.Log(lerpSpd * Time.deltaTime);
        }
        if(choosenIndex + 1 <= buttonList.Count - 1)
        {
            RectTransform nextRT = buttonList[choosenIndex + 1].GetComponent<RectTransform>();
            nextRT.anchoredPosition = Vector2.Lerp(nextRT.anchoredPosition, SBRT.anchoredPosition - offset, lerpSpd * Time.deltaTime);
        }
        */
    }

    IEnumerator FadeCanvas(CanvasGroup cg, float endValue, float duration)
    {
        isFading = true;
        float elapsedTime = 0;
        float startValue = cg.alpha;
        while (elapsedTime < duration)
        {
            elapsedTime += .1f;
            cg.alpha = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            yield return null;
        }
        isFading = false;

        if (startGame)
            sceneLoader.LoadLevel(1);
        //SceneManager.LoadScene(1);
        else if (creditScene)
            sceneLoader.LoadLevel(2);
            //SceneManager.LoadScene(2);
    }
    public void PressedStart()
    {
        startGame = true;
        StartCoroutine(FadeCanvas(fadeImage.GetComponent<CanvasGroup>(), 1f, fadeTime));
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
        StartCoroutine(FadeCanvas(settingsHolder.GetComponent<CanvasGroup>(), 1f, fadeTime));
        StartCoroutine(FadeCanvas(gameObject.transform.Find("Home").GetComponent<CanvasGroup>(), 0f, fadeTime));
        panelState = panels.settings;
    }

    public void ShowCredit()
    {
        creditScene = true;
        StartCoroutine(FadeCanvas(fadeImage.GetComponent<CanvasGroup>(), 1f, fadeTime));
    }
}
