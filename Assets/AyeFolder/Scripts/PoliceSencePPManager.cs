using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PoliceSencePPManager : MonoBehaviour
{
    public Volume PolicePpVolume;
    public GameObject PoliceSenseCam;
    PaniniProjection FishEye;
    DepthOfField DOF;
    LensDistortion LD;
    Vignette Vig;
    public GameObject DirectionalLight;
    public GameObject LightEnemy;


    private void Awake()
    {
        
        PolicePpVolume.profile.TryGet<LensDistortion>(out LD);
        PolicePpVolume.profile.TryGet<DepthOfField>(out DOF);
        PolicePpVolume.profile.TryGet<PaniniProjection>(out FishEye);
        PolicePpVolume.profile.TryGet<Vignette>(out Vig);
    }
    
    public IEnumerator ResetPanini()
    {
        if (FishEye != null)
        {

            float time = 0;
            float originalDist = FishEye.distance.value;
            float originalVigInt = Vig.intensity.value;

            while (FishEye.distance.value != 0)
            {
                Debug.Log("resetP");
                time += Time.deltaTime*2;
                FishEye.distance.value = Mathf.Lerp(originalDist, 0, time);
                Vig.intensity.value = Mathf.Lerp(originalVigInt, 0, time);
                yield return null;
            }
        }
        PoliceSenseCam.SetActive(false);
        DirectionalLight.SetActive(false);
        LightEnemy.SetActive(false);
        FishEye.distance.value = 0.7f;
        Vig.intensity.value = 0.5f;

    }

    public IEnumerator PoliceDistorsionFilter()
    {
        PoliceSenseCam.SetActive(true);
        DirectionalLight.SetActive(true);
        LightEnemy.SetActive(true);
        if (DOF != null && LD != null)
        {
            float distAmount = 0.8f;
            float distance = 0.1f;

            while (distAmount >= 0)
            {
                LD.intensity.value = distAmount;
                DOF.focusDistance.value = distance;
                distAmount -= 0.01f;
                distance += 0.03f;
                yield return new WaitForSecondsRealtime(0.005f * Time.deltaTime);
            }

            LD.intensity.value = 0;
            DOF.focusDistance.value = 10;

        }

    }
}
