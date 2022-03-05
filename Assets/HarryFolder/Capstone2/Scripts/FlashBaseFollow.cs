using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBaseFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    Vector3 lerpDes;
    public float moveSpd;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print(lerpDes);
        lerpDes = target.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, lerpDes, moveSpd * Time.deltaTime);
    }
}
