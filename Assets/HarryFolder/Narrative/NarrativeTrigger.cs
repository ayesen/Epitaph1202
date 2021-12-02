using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeTrigger : MonoBehaviour
{
    public string []inspectLines;
    public string[] monoLines;
    public string[] itemLines;
    public bool isInspect;
    public bool isMono;
    public bool isItem;
    private GameObject mask;
    private GameObject textDisplay;
    private GameObject indicator;
    void Start()
    {
        //Get universal Text
        textDisplay = GameObject.Find("NarText");
        indicator = GameObject.Find("eIndicator");
        mask = GameObject.Find("Mask");
        textDisplay.SetActive(false);
        indicator.SetActive(false);
        mask.SetActive(false);
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            textDisplay.SetActive(true);
            indicator.SetActive(true);
            mask.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            textDisplay.SetActive(false);
            indicator.SetActive(false);
            mask.SetActive(false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("He's in");
            indicator.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("He's out");
            indicator.SetActive(false);
        }
    }
}
