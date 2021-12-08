using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInManager : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(UIManager.Me.FadeCanvas(GetComponent<CanvasGroup>(), 0, 3));
    }
}
