using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectorCam : MonoBehaviour
{
    public GameObject myCam;
    public float speed;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myCam.transform.Translate(Vector3.up * Time.deltaTime*speed, Space.World);
    }
}
