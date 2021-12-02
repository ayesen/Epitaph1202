using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingManager : MonoBehaviour
{
    public Volume PpVolume;
    ColorAdjustments CA;
    Vignette Vig;
    ChromaticAberration ChrAb;

    private static PostProcessingManager me = null;
    public static PostProcessingManager Me
    {
        get
        {
            return me;
        }
    }

    private void Awake()
    {
        if(me != null && me != this)
        {
            Destroy(this.gameObject);
        }

        me = this;

        PpVolume.profile.TryGet<ColorAdjustments>(out CA);
        PpVolume.profile.TryGet<Vignette>(out Vig);
        PpVolume.profile.TryGet<ChromaticAberration>(out ChrAb);
    }
    private void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.Space))
        {

            StartCoroutine(DeadFilter());
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ResetFilter()); 
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            ChangeFilter();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            GetBack();
        }*/
    }

    public void Reset()
    {
        CA.saturation.value = 0;
        CA.colorFilter.value = Color.white;
        Vig.intensity.value = 0;
        ChrAb.intensity.value = 0;
    }

    public void ChangeFilter()
    {
        CA.saturation.value -= 5;
        Vig.intensity.value += 0.05f;
        ChrAb.intensity.value += 0.5f;
    }

    public void GetBack()
    {
        if (CA.saturation.value < 0)
        {
            CA.saturation.value += 5;
        }
        Vig.intensity.value -= 0.05f;
        ChrAb.intensity.value -= 0.5f;
    }

    public void StartDeadFilter()
    {
        StartCoroutine(DeadFilter());
    }

    public void StartReset()
    {
        StartCoroutine(ResetFilter());
    }

    public IEnumerator ResetFilter()
    {
        
        float time = 0;
        float timecolor = 0;
        float timevig = 0;
        float originalSat = CA.saturation.value;
        float originalVig = Vig.intensity.value;
        float originalChrom = ChrAb.intensity.value;

        while (CA.colorFilter.value != Color.white)
        {
            Debug.Log("resetf");
            time += Time.fixedDeltaTime / 5;
            timecolor += Time.fixedDeltaTime / 10;
            timevig += Time.fixedDeltaTime / 10;
            CA.saturation.value = Mathf.Lerp(originalSat, 0, time);
            CA.colorFilter.value = Color.Lerp(Color.black, Color.white, timecolor);
            Vig.intensity.value = Mathf.Lerp(originalVig, 0, timevig);
            ChrAb.intensity.value = Mathf.Lerp(originalChrom, 0, timevig);
            yield return null;
        }


    }
    public IEnumerator DeadFilter()
    {
        float time = 0;
        float timecolor = 0;
        float timevig = 0;
        float originalSat = CA.saturation.value;
        float originalVig = Vig.intensity.value;
        float originalChrom = ChrAb.intensity.value;

        while (CA.colorFilter.value != Color.black)
        {
            Debug.Log("black");
            time += Time.fixedDeltaTime/5;
            timecolor += Time.fixedDeltaTime/10;
            timevig += Time.fixedDeltaTime/10;
            CA.saturation.value = Mathf.Lerp(originalSat, -100, time);
            CA.colorFilter.value = Color.Lerp(Color.white, Color.black, timecolor);
            Vig.intensity.value = Mathf.Lerp(originalVig, 1, timevig);
            ChrAb.intensity.value = Mathf.Lerp(originalChrom, 1, timevig);
            yield return null;
        }

        if (CA.colorFilter.value == Color.black)
        {
            SafehouseManager.Me.isSafehouse = true;
        }
    }

}
