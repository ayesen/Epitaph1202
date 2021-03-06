using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public GameObject ExplosionObj;
    public float speed = 2f;
    public Material ExplosionMat;
    public float CircleSizeTimer;
    public float InOffsetTimer;
    public float OutOffsetTimer;


    private void Start()
    {
        ExplosionMat = ExplosionObj.GetComponent<MeshRenderer>().material;
        StartCoroutine(StartExplosion());
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            StartCoroutine(StartExplosion());
        }
    }

    public IEnumerator StartExplosion()
    {
        print("started coroutine: start explosion");
        float time = 0f;
        //ExplosionObj.SetActive(true);
        while (CircleSizeTimer > -0.8)
        {
            time += Time.fixedDeltaTime;
            Debug.Log("start explosion");
            //CircleSizeTimer -= Time.fixedDeltaTime * speed;
            //InOffsetTimer += Time.fixedDeltaTime * 0.5f;
            //OutOffsetTimer -= Time.fixedDeltaTime * 0.5f;
            CircleSizeTimer = Mathf.Lerp(CircleSizeTimer, -0.9f, Time.deltaTime * speed);
            InOffsetTimer = Mathf.Lerp(InOffsetTimer, 1.2f, Time.deltaTime);
            OutOffsetTimer = Mathf.Lerp(OutOffsetTimer, -1.2f, Time.deltaTime);
            ExplosionMat.SetFloat("circleScale", CircleSizeTimer);
            ExplosionMat.SetFloat("InOffset", InOffsetTimer);
            ExplosionMat.SetFloat("OutOffset", OutOffsetTimer);

            yield return null;
        }
        StartCoroutine(CloseExplosion());
    }

    public IEnumerator CloseExplosion()
    {
        while (CircleSizeTimer < 0)
        {
            Debug.Log("close explosion");
            //CircleSizeTimer += Time.fixedDeltaTime * 0.5f;
            //InOffsetTimer -= Time.fixedDeltaTime * 0.5f;
            //OutOffsetTimer += Time.fixedDeltaTime * 0.5f;
            CircleSizeTimer = Mathf.Lerp(CircleSizeTimer, 0.2f, Time.deltaTime * speed);
            InOffsetTimer = Mathf.Lerp(InOffsetTimer, 0.2f, Time.deltaTime);
            OutOffsetTimer = Mathf.Lerp(OutOffsetTimer, 0.2f, Time.deltaTime);
            ExplosionMat.SetFloat("circleScale", CircleSizeTimer);
            ExplosionMat.SetFloat("InOffset", InOffsetTimer);
            ExplosionMat.SetFloat("OutOffset", OutOffsetTimer);

            yield return null;
        }
        gameObject.SetActive(false);
    }
}
