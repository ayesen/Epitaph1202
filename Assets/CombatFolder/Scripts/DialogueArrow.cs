using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueArrow : MonoBehaviour
{
    public float floatSpd;
    public float half_range;
    Vector3 initPos;
    private RectTransform rt;
    void Start()
    {
        rt = GetComponent<RectTransform>();
        initPos = rt.localPosition;
    }

    void Update()
    {
        rt.localPosition += Vector3.right * floatSpd * Time.deltaTime;

        if (rt.localPosition.x <= initPos.x - half_range || rt.localPosition.x >= initPos.x + half_range)
        {
            rt.localPosition = new Vector3(Mathf.Clamp(rt.localPosition.x, initPos.x - half_range, initPos.x + half_range), rt.localPosition.y, rt.localPosition.z);
            floatSpd *= -1;
        }
    }
}
