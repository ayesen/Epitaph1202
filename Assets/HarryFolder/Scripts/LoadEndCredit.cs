using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadEndCredit : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(0);
    }
}
