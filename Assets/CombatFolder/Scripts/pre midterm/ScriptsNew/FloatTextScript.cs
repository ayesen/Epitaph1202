using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatTextScript : MonoBehaviour
{
    public float DestroyTime = 3f;
    public float slideIntensity;
    private float intensity;
    void Start()
    {
        Destroy(gameObject, DestroyTime);
        intensity = Random.Range(-slideIntensity, slideIntensity);
    }

    private void Update()
    {
        transform.localPosition += new Vector3(0, 0.01f + intensity, 0);
    }

}
