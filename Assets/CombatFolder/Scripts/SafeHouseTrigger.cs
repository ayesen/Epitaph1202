using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeHouseTrigger : MonoBehaviour
{
    private bool doOnce;
    private bool isClose;
    void Start()
    {
        doOnce = isClose;
    }
    
    void Update()
    {
        if (Vector3.Distance(transform.position, PlayerScriptNew.me.transform.position) < 5)
        {
            isClose = true;
            if (Input.GetKeyDown(KeyCode.E) || Input.GetAxis("HorizontalArrow") > 0 && !SafehouseManager.Me.isSafehouse)
            {
                SafehouseManager.Me.isSafehouse = true;
            }
        }
        else
        {
            isClose = false;
        }
        if (doOnce != isClose && isClose)
        {
            doOnce = isClose;
        }
        else if (doOnce != isClose && !isClose)
        {
            doOnce = isClose;
        }
    }
}
