using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MonoBehavior : MonoBehaviour
{
    public TMP_Text objectDes_ui;
    public GameObject textBg;
    //public GameObject pressE;
    public float readLength = 1f;
    public float timer = 0;
    public bool inRange;
    private MeshRenderer mR;
    public GameObject[] interactiveSwitch;
    [TextArea]
    public string displayText;
    void Start()
    {
        inRange = false;
        mR = GetComponent<MeshRenderer>();
        //mR.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange)
        {
            timer += Time.deltaTime;
        }

        if (timer > readLength)
        {
            objectDes_ui.gameObject.SetActive(false);
            textBg.SetActive(false);
            foreach (GameObject interactable in interactiveSwitch)
            {
                interactable.SetActive(true);
            }
            this.gameObject.SetActive(false);
            //pressE.SetActive(false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RealPlayer"))
        {
            inRange = true;
            StartCoroutine(Monobehavior());
        }
        
    }
    
    IEnumerator Monobehavior()
    {
        yield return new WaitForSeconds(0.1f);
        if (timer < readLength)
        {
            objectDes_ui.text = displayText;
            objectDes_ui.gameObject.SetActive(true);
            textBg.SetActive(true);
            //pressE.SetActive(true);
        }
    }
    
}
