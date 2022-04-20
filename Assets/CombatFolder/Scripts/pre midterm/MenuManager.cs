using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
                SoundMan.SoundManager.AudioPauseOrUnpause();
                //AmbienceManager.ambienceManager.gameObject.GetComponent<AudioReverbFilter>().enabled = true;
            }
            else
            {
                StartCoroutine(FadeCanvas(cg, 0f, fadeTime));
                GameIsPaused = false;
                SoundMan.SoundManager.AudioPauseOrUnpause();
                /*AmbienceManager.ambienceManager.gameObject.GetComponent<AudioReverbFilter>().enabled = false;
                AmbienceManager.ambienceManager.gameObject.GetComponent<AudioLowPassFilter>().cutoffFrequency = 22000f;
                AmbienceManager.ambienceManager.gameObject.GetComponent<AudioLowPassFilter>().lowpassResonanceQ = 1f;*/
            }
        }
        if (isMenu)
        {
            float timer = 0;
            timer += .1f;
            /*AmbienceManager.ambienceManager.gameObject.GetComponent<AudioLowPassFilter>().cutoffFrequency =
                Mathf.Lerp(AmbienceManager.ambienceManager.gameObject.GetComponent<AudioLowPassFilter>().cutoffFrequency, 3900f, timer);
            AmbienceManager.ambienceManager.gameObject.GetComponent<AudioLowPassFilter>().lowpassResonanceQ =
                Mathf.Lerp(AmbienceManager.ambienceManager.gameObject.GetComponent<AudioLowPassFilter>().lowpassResonanceQ, 2.85f, timer);*/

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
                if (Input.GetButtonDown("BButton") && !isFading)
                {
                    isMenu = false;
                    SoundMan.SoundManager.SafehouseMaterialSelect();
                }
            }
            else if(panelState == panels.ctrlMap)
            {
                if ((Input.GetButtonDown("BButton") || Input.GetKeyDown(KeyCode.B)) && !isFading)
                {
                    StartCoroutine(FadeCanvas(mappingHolder.GetComponent<CanvasGroup>(), 0f, fadeTime));
                    StartCoroutine(FadeCanvas(gameObject.transform.Find("Home").GetComponent<CanvasGroup>(), 1f, fadeTime));
                    panelState = panels.home;
                    SoundMan.SoundManager.SafehouseMaterialSelect();
                }
            }
            else if(panelState == panels.settings)
            {
                if ((Input.GetButtonDown("BButton") || Input.GetKeyDown(KeyCode.B)) && !isFading)
                {
                    StartCoroutine(FadeCanvas(gameObject.transform.Find("Home").GetComponent<CanvasGroup>(), 1f, fadeTime));
                    panelState = panels.home;
                    SoundMan.SoundManager.SafehouseMaterialSelect();
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
        float elapsedTime = 0;
        float startValue = cg.alpha;
        while (elapsedTime < duration)
        {
            elapsedTime += .1f;
            cg.alpha = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            yield return null;
        }
        if (!isMenu)
        {
            panelState = panels.home;
            Time.timeScale = 1f;
            GameIsPaused = false;
            choosenIndex = 0;
            transform.Find("Home").GetComponent<CanvasGroup>().alpha = 1;
            mappingHolder.GetComponent<CanvasGroup>().alpha = 0;
        }
        isFading = false;
    }
    public void resumeGame()
    {
        isMenu = false;
    }
    
    public void exitGame()
    {
        //GameObject.Find("SceneLoader").GetComponent<SceneLoader>().LoadLevel(0);
        SceneManager.LoadScene(0);
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
