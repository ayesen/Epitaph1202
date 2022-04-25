using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public GameObject ExplosionObj;
    public float speed = 0.5f;
    private Material ExplosionMat;
    public float CircleSizeTimer;
    public float InOffsetTimer;
    public float OutOffsetTimer;


    private void Start()
    {
        ExplosionMat = ExplosionObj.GetComponent<MeshRenderer>().material;
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
        ExplosionObj.SetActive(true);
        while (CircleSizeTimer > -0.8)
        {
            Debug.Log("j");
            CircleSizeTimer -= Time.fixedDeltaTime * 0.5f;
            InOffsetTimer += Time.fixedDeltaTime * 0.5f;
            OutOffsetTimer -= Time.fixedDeltaTime * 0.5f;
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
            Debug.Log("j");
            CircleSizeTimer += Time.fixedDeltaTime * 0.5f;
            InOffsetTimer -= Time.fixedDeltaTime * 0.5f;
            OutOffsetTimer += Time.fixedDeltaTime * 0.5f;
            ExplosionMat.SetFloat("circleScale", CircleSizeTimer);
            ExplosionMat.SetFloat("InOffset", InOffsetTimer);
            ExplosionMat.SetFloat("OutOffset", OutOffsetTimer);

            yield return null;
        }
        ExplosionObj.SetActive(false);
    }


}
