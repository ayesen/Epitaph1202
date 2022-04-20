using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadEndCredit : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        GameObject.Find("SceneLoader").GetComponent<SceneLoader>().LoadLevel(2);
        //SceneManager.LoadScene(2);
    }
}
