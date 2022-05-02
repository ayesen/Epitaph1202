using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SubtitleScript : MonoBehaviour
{
    public TextMeshProUGUI Subtitle;
    public TextMeshProUGUI SubtitleEnglish;
    public TextMeshProUGUI Credit;
    public int SubtitleReadTime;
    public string LineOne;
    public string LineOneEnglish;
    public string LineTwo;
    public string LineTwoEnglish;
    public string LineThree;
    public string LineThreeEnglish;
    private int Loop = 3;
    private Vector3 OriginalPos;
    public SpriteRenderer BackGround;
    public CanvasGroup Black;
    public GameObject videoHolder;
    public VideoPlayer videoPlayer;
    public GameObject upperStripe;
    public GameObject lowerStripe;
    public RenderTexture videoRD;
    bool videoCouldFinish;


    void Start()
    {
        StartCoroutine(FadeIn(5));
        StartCoroutine(HideBG(Black, 0f, 12f));
    }

    private void Update()
    {
        if (Input.GetButtonUp("AButton"))
        {
            GameObject.Find("SceneLoader").GetComponent<SceneLoader>().LoadLevel(0);
        }
        if(videoCouldFinish && !videoPlayer.isPlaying)
        {
            GameObject.Find("SceneLoader").GetComponent<SceneLoader>().LoadLevel(0);
        }
    }

    public IEnumerator FadeIn(float time)
    {
        yield return new WaitForSeconds(2);
        Debug.Log("fadein");
        switch(Loop)
        {
            case 3:
                Subtitle.text = LineOne;
                SubtitleEnglish.text = LineOneEnglish;
                break;
            case 2:
                Subtitle.text = LineTwo;
                SubtitleEnglish.text = LineTwoEnglish;
                break;
            case 1:
                Subtitle.text = LineThree;
                SubtitleEnglish.text = LineThreeEnglish;
                break;
            default:
                Subtitle.text = "";
                SubtitleEnglish.text = "";
                break;
        }

        float subTimer = 0;
        while(subTimer < time)
        {
            subTimer += Time.fixedDeltaTime;
            Subtitle.color = Color.Lerp(Color.clear, Color.white, subTimer);
            SubtitleEnglish.color = Color.Lerp(Color.clear, Color.white, subTimer);
            yield return null;
        }
        StartCoroutine(FadeOut(5));
    }

    public IEnumerator FadeOut(float time)
    {
        yield return new WaitForSeconds(5);
        Debug.Log("fadeout");
        float subTimer = 0;
        while (Subtitle.color != Color.clear)
        {
            subTimer += Time.fixedDeltaTime;
            Subtitle.color = Color.Lerp(Color.white, Color.clear, subTimer);
            SubtitleEnglish.color = Color.Lerp(Color.white, Color.clear, subTimer);
            yield return null;
        }
        Loop -= 1;
        if (Loop >= 1)
        {
            StartCoroutine(FadeIn(5));
        }
        else
            StartCoroutine(ShowBG());
    }

    public IEnumerator ShowBG()
    {
        yield return new WaitForSeconds(1);
        float Timer = 0;
        while(BackGround.color != Color.white)
        {
            Timer += 0.01f;
            BackGround.color = Color.Lerp(Color.clear, Color.white, Timer);
            yield return null;
        }
        StartCoroutine(PlayDaVideo());
        //StartCoroutine(RolingUp(Black, 1f, 20f));
    }

    public IEnumerator HideBG(CanvasGroup cg, float endValue, float duration)
    {
        float elapsedTime = 0;
        float startValue = cg.alpha;
        while (elapsedTime < duration)
        {
            elapsedTime += .1f;
            cg.alpha = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            yield return null;
        }
    }

    public IEnumerator PlayDaVideo()
    {
        float elapsedTime = 0;
        float startValue = Black.alpha;
        while (elapsedTime < 1.5f)
        {
            elapsedTime += Time.deltaTime;
            Black.alpha = Mathf.Lerp(startValue, 1f, elapsedTime / 1.5f);
            yield return null;
        }

        upperStripe.SetActive(false);
        lowerStripe.SetActive(false);

        videoPlayer.frame = 0;
        videoRD.Release();

        videoHolder.SetActive(true);

        videoPlayer.Play();

        elapsedTime = 0;
        startValue = Black.alpha;
        while (elapsedTime < 3f)
        {
            elapsedTime += Time.deltaTime;
            Black.alpha = Mathf.Lerp(startValue, 0f, elapsedTime / 3f);
            yield return null;
        }

        

        videoCouldFinish = true;

       
    }

    public IEnumerator RolingUp(CanvasGroup cg, float endValue, float duration)
    {
        OriginalPos = Credit.rectTransform.anchoredPosition3D;
        float yPos = OriginalPos.y;
        while(Credit.rectTransform.anchoredPosition.y < 1550)
        {
            yPos += 0.2f;
            Credit.rectTransform.anchoredPosition = new Vector3(OriginalPos.x, yPos, OriginalPos.z);
            yield return null;
        }
        float elapsedTime = 0;
        float startValue = cg.alpha;
        while (elapsedTime < duration)
        {
            elapsedTime += .1f;
            cg.alpha = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            yield return null;
        }
        GameObject.Find("SceneLoader").GetComponent<SceneLoader>().LoadLevel(0);
        //SceneManager.LoadScene(0);
    }



}
