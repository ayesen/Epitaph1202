using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInManager : MonoBehaviour
{
    private static FadeInManager me;
    public static FadeInManager Me
    {
        get
        {
            return me;
        }
    }
    private void Awake()
    {
        if(me != null && me != this)
        {
            Destroy(this.gameObject);
        }

        me = this;
    }
    void Start()
    {
        StartCoroutine(UIManager.Me.FadeCanvas(GetComponent<CanvasGroup>(), 0, 3));
    }
}
