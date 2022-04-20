using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class StartSequence : MonoBehaviour
{
    /*The Sequence:
    1. Play the tile video
    2. As the video finished, 1. pan the video to the left 2. also the uis 3. BG Fade In
     */
    public float sequenceDuration;
    VideoPlayer vP;
    public RectTransform uI;
    public RectTransform video;
    public Image bG;
    public static bool sequenceIsDone;

    bool sequencePlayed;
    private void Start()
    {
        Time.timeScale = 1;
        vP = GetComponent<VideoPlayer>();
        sequenceIsDone = false;
    }

    private void Update()
    {
        if (!vP.isPlaying && !sequencePlayed)
        {
            StartCoroutine(Sequence());
            sequencePlayed = false;
        }

    }


    IEnumerator Sequence()
    {
        float elapsedTime = 0;
        //Backgrounds
        float bGStartValue = bG.GetComponent<Image>().color.a;
        Color curColor = bG.GetComponent<Image>().color;
        //UIs
        float uIPosXStartValue = uI.localPosition.x;
        Vector3 curUIPos = uI.localPosition;
        //Video
        float videoPosXStartValue = video.localPosition.x;
        Vector3 curVideoPos = video.localPosition;

        while (elapsedTime < sequenceDuration)
        {
            float t = elapsedTime / sequenceDuration;
            t = t * t * (3f - 2f * t);
            //Backgrounds
            curColor.a = Mathf.Lerp(bGStartValue, 1, t);
            bG.GetComponent<Image>().color = curColor;
            //UIs
            curUIPos.x = Mathf.Lerp(uIPosXStartValue, 200, t);
            uI.localPosition = curUIPos;
            //Video
            curVideoPos.x = Mathf.Lerp(videoPosXStartValue, -680, t);
            video.localPosition = curVideoPos;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        //Backgrounds
        curColor.a = 1;
        bG.GetComponent<Image>().color = curColor;
        //UIs
        curUIPos.x = 200;
        uI.localPosition = curUIPos;
        //Video
        curVideoPos.x = -680;
        video.localPosition = curVideoPos;

        sequenceIsDone = true;
    }
    
}
