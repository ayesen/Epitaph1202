using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeHouseTrigger : MonoBehaviour
{
    public Material highlightMaterial;
    void Start()
    {
        
    }
    
    void Update()
    {
        if (Vector3.Distance(transform.position, PlayerScriptNew.me.transform.position) < 5)
        {
            GetComponent<MeshRenderer>().material = highlightMaterial;
            if (Input.GetKeyDown(KeyCode.E) || Input.GetAxis("HorizontalArrow") > 0)
            {
                SafehouseManager.Me.isSafehouse = true;
            }
        }
    }
}
