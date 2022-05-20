using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeHouseTrigger : MonoBehaviour
{
    private bool doOnce;
    private bool isClose;
    private bool isFading;
    private bool entered;
    private bool enterOnce;

    public Transform rebornPos;
    public GameObject refillInstructor;

    void Start()
    {
        doOnce = isClose;
    }
    
    void Update()
    {
        if (Vector3.Distance(transform.position, PlayerScriptNew.me.transform.position) < 3)
        {
            if (!MenuManager.GameIsPaused)
            {
                entered = true;

                if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("RB") && !SafehouseManager.Me.isSafehouse)
                {
                    isClose = true;
                }
                else
                {
                    isClose = false;
                }
            }
        }
        else
        {
            entered = false;
        }

        if (enterOnce != entered)
        {
            enterOnce = entered;
            if (entered)
            {
                SafehouseManager.Me.ResetMatAmount(); //Reset mat amount & recover hp
                StopAllCoroutines();
                StartCoroutine(InstrucFadeCanvas(refillInstructor.GetComponent<CanvasGroup>(), 0, 3));
            }
        }

        if (doOnce != isClose && isClose)
        {
            doOnce = isClose;
            SafehouseManager.Me.isSafehouse = true;
            SavePointManager.me.last_checkPoint = this;
        }
        else if (doOnce != isClose && !isClose)
        {
            doOnce = isClose;
        }
    }

    IEnumerator FadeCanvas(CanvasGroup cg, float endValue, float duration)
    {
        isFading = true;
        float elapsedTime = 0;
        float startValue = cg.alpha;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            cg.alpha = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            yield return null;
        }
        isFading = false;
    }

    IEnumerator InstrucFadeCanvas(CanvasGroup cg, float endValue, float duration)
    {
        isFading = true;
        cg.alpha = 1;
        yield return new WaitForSeconds(3f);
        float elapsedTime = 0;
        float startValue = cg.alpha;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            cg.alpha = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            yield return null;
        }
        isFading = false;
    }
}
