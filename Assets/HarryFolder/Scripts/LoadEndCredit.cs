using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadEndCredit : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FadeInManager.Me.SceneLoadFadeCanvas(FadeInManager.Me.gameObject.GetComponent<CanvasGroup>(), 1, 3));

        }
        //SceneManager.LoadScene(2);
    }

    
}
