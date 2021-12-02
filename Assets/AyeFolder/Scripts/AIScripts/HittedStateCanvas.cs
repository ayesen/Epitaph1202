using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittedStateCanvas : MonoBehaviour
{
    public GameObject AI;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = AI.transform.position;
    }
}
