using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadEndCredit : MonoBehaviour
{
    //bool stopMoving;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //stopMoving = true;
            StartCoroutine(FadeInManager.Me.SceneLoadFadeCanvas(FadeInManager.Me.gameObject.GetComponent<CanvasGroup>(), 1, 3));
        }
        //SceneManager.LoadScene(2);
    }

    
}
