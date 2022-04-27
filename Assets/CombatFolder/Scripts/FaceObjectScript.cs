using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceObjectScript : MonoBehaviour
{
    public bool noSelfRotation;

    void Update()
    {
        transform.LookAt(Camera.main.transform);
        if (!noSelfRotation)
        {
            transform.Rotate(90, 0, 0, Space.Self);
        }
    }
}
