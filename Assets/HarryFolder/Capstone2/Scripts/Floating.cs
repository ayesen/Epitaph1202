using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    public float radian;

    public float perRadian;

    public float radius;

    Vector3 oldPos;
    
    // Start is called before the first frame update
    void Start()
    {
        oldPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        oldPos = transform.position;
        radian += perRadian;
        float dY = Mathf.Cos(radian) * radius;
        transform.position = oldPos + new Vector3(0, dY, 0);

    }
}
