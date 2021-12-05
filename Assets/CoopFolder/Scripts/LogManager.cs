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

    public Image pageBase;
    public GameObject coverPrefab;

    public GameObject Page1;
    public GameObject Page2;
    public GameObject Page3;
    public GameObject Page4;
    public GameObject Page5;

    public List<Sprite> PageBase = new List<Sprite>();
    public List<Sprite> P1Cover = new List<Sprite>();
    public List<Sprite> P2Cover = new List<Sprite>();
    public List<Sprite> P3Cover = new List<Sprite>();
    public List<Sprite> P4Cover = new List<Sprite>();
    public List<Sprite> P5Cover = new List<Sprite>();
    
    //VARIABLE
    private bool mFaded = true;
    public float duration = 0.4f;
    private int currentPage = 0;
    public List<GameObject> P1CoverInstance = new List<GameObject>();
    public List<GameObject> P2CoverInstance = new List<GameObject>();
    public List<GameObject> P3CoverInstance = new List<GameObject>();
    public List<GameObject> P4CoverInstance = new List<GameObject>();
    public List<GameObject> P5CoverInstance = new List<GameObject>();

    private void Awake()
    {
        if(LOGManager != null)
        {
            Destroy(gameObject);
            return;
        }
        LOGManager = this;
    }

    private void Start()
    {
        
        for(int i = 0; i<P1Cover.Count;i++)
        {
        	GameObject x = Instantiate(coverPrefab,this.transform.position,this.transform.rotation,Page1.transform);
        	x.GetComponent<Image>().sprite = P1Cover[i];
        	P1CoverInstance.Add(x);
        }
        for(int i = 0; i<P2Cover.Count;i++)
        {
        	GameObject x = Instantiate(coverPrefab,this.transform.position,this.transform.rotation,Page2.transform);
        	x.GetComponent<Image>().sprite = P2Cover[i];
        	P2CoverInstance.Add(x);
        }
        for(int i = 0; i<P3Cover.Count;i++)
        {
        	GameObject x = Instantiate(coverPrefab,this.transform.position,this.transform.rotation,Page3.transform);
        	x.GetComponent<Image>().sprite = P3Cover[i];
        	P3CoverInstance.Add(x);
        }
        for(int i = 0; i<P4Cover.Count;i++)
        {
        	GameObject x = Instantiate(coverPrefab,this.transform.position,this.transform.rotation,Page4.transform);
        	x.GetComponent<Image>().sprite = P4Cover[i];
        	P4CoverInstance.Add(x);
        }
        for(int i = 0; i<P5Cover.Count;i++)
        {
        	GameObject x = Instantiate(coverPrefab,this.transform.position,this.transform.rotation,Page5.transform);
        	x.GetComponent<Image>().sprite = P5Cover[i];
        	P5CoverInstance.Add(x);
        }
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
        //pageBase.enabled = !pageBase.enabled;
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
    	if(currentPage>0){
    		currentPage--;
    	}
    	ChangePage();
    }

    private void Next()
    {
    	if(currentPage<PageBase.Count-1){
    		currentPage++;
    	}
    	ChangePage();
    }

    private void ChangePage()
    {
        //Missing Play Sound;
    	pageBase.sprite = PageBase[currentPage];
    	CoverSetNotActive();
    	switch(currentPage)
    	{
    		case 4:
    			Page5.SetActive(true);
    			break;
    		case 3:
    			Page4.SetActive(true);
    			break;
    		case 2:
    			Page3.SetActive(true);
    			break;
    		case 1:
    			Page2.SetActive(true);
    			break;
    		case 0:
    			Page1.SetActive(true);
    			break;
    		default:

    			break;

    	}
    }

    private void CoverSetNotActive()
    {
    	Page1.SetActive(false);
    	Page2.SetActive(false);
    	Page3.SetActive(false);
    	Page4.SetActive(false);
    	Page5.SetActive(false);
    }

    public void TurnPageTo(int index)
    {
    	currentPage = index;
    	ChangePage();
    }
    
}
