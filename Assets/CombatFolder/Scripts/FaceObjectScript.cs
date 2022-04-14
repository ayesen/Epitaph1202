using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceObjectScript : MonoBehaviour
{
    //public GameObject objectToFace;

    void Update()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(90, 0, 0, Space.Self);
    }
}
