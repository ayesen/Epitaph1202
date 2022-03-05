using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PoliceSencePPManager : MonoBehaviour
{
    public Volume PolicePpVolume;
    DepthOfField DOF;
    LensDistortion LD;

    private void Awake()
    {
        
        PolicePpVolume.profile.TryGet<LensDistortion>(out LD);
        PolicePpVolume.profile.TryGet<DepthOfField>(out DOF);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(DistorsionFilter());
        }
    }
    public IEnumerator DistorsionFilter()
    {
        if (DOF != null && LD != null)
        {
            float distAmount = 0.7f;
            float distance = 0.1f;

            while (distAmount >= 0)
            {
                LD.intensity.value = distAmount;
                DOF.focusDistance.value = distance;
                distAmount -= 0.01f;
                distance += 0.04f;
                yield return new WaitForSecondsRealtime(0.01f);
            }

            LD.intensity.value = 0;
            DOF.focusDistance.value = 10;

        }

    }
}
