using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueArrow : MonoBehaviour
{
    public float floatSpd;
    private RectTransform rt;
    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    void Update()
    {
        rt.localPosition -= Vector3.down * floatSpd * Time.deltaTime;

        if (rt.localPosition.y < 80 || rt.localPosition.y > 110)
        {
            rt.localPosition = new Vector3(rt.localPosition.x, Mathf.Clamp(rt.localPosition.y, 80, 110), rt.localPosition.z);
            floatSpd *= -1;
        }
    }
}
