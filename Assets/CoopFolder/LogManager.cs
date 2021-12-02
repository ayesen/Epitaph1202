using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogManager : MonoBehaviour
{
    public static LogManager LOGManager;
    //ASSIGN
    public KeyCode back;
    public KeyCode next;
    public KeyCode call;
    public Image lOne;
    public Image lTwo;
    public Image lThree;
    public Image rOne;
    public Image rTwo;
    public Image rThree;
    public Sprite undiscoverd;
    public TMP_Text leftIndex;
    public TMP_Text rightIndex;
    public List<Page> page = new List<Page>();
    
    //VARIABLE
    private int leftPage;
    private int rightPage;
    private int maxPage;
    private bool mFaded = true;
    public float duration = 0.4f;
    private void Awake()
    {
        if(LOGManager != null)
        {
            Destroy(gameObject);
            return;
        }
        LOGManager = this;
        
        leftPage = 0;
        rightPage = 1;
        maxPage = page.Count;
    }

    private void Start()
    {
        ChangePage();
    }

    private void Update()
    {
        if (Input.GetKeyDown(call))
        {
            Fade();
        }

        if (!mFaded)
        {
            if (Input.GetKeyDown(back))
            {
                Back();
            }
            else if(Input.GetKeyDown(next))
            {
                Next();
            }
        }
        
    }

    public void Fade()
    {
        var canGroup = GetComponent<CanvasGroup>();

        StartCoroutine(DoFade(canGroup, canGroup.alpha, mFaded ? 1:0));
        
        mFaded = !mFaded;
    }

    public IEnumerator DoFade(CanvasGroup canvGroup, float start, float end)
    {
        float counter = 0f;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            canvGroup.alpha = Mathf.Lerp(start, end, counter / duration);

            yield return null;
        }
    }

    private void Back()
    {
        if (leftPage > 0)
        {
            leftPage-=2;
            rightPage-=2;
            ChangePage();
        }
    }

    private void Next()
    {
        if (rightPage + 1 < maxPage)
        {
            leftPage+=2;
            rightPage+=2;
            ChangePage();
        }
    }

    private void ChangePage()
    {
        //Missing Play Sound;
        Page tempLeft = page[leftPage];
        Page tempRight = page[rightPage];

        lOne.sprite = tempLeft.oneDiscovered ? tempLeft.one : undiscoverd;
        lTwo.sprite = tempLeft.twoDiscovered ? tempLeft.two : undiscoverd;
        lThree.sprite = tempLeft.threeDiscovered ? tempLeft.three : undiscoverd;

        rOne.sprite = tempRight.oneDiscovered ? tempRight.one : undiscoverd;
        rTwo.sprite = tempRight.twoDiscovered ? tempRight.two : undiscoverd;
        rThree.sprite = tempRight.threeDiscovered ? tempRight.three : undiscoverd;

        leftIndex.text = (leftPage + 1).ToString();
        rightIndex.text = (rightPage + 1).ToString();
    }

    public void TurnPageTo(int index)
    {
        if (index % 2 != 0)
        {
            leftPage = index;
            rightPage = index + 1;
        }
        else
        {
            leftPage = index + 1;
            rightPage = index;
        }
    }
    
}


[System.Serializable]public struct Page
{
    public Sprite one;
    public bool oneDiscovered;
    public Sprite two;
    public bool twoDiscovered;
    public Sprite three;
    public bool threeDiscovered;


}