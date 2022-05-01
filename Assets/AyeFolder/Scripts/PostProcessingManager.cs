using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingManager : MonoBehaviour
{
    public Volume PpVolume;
    public GameObject PolicePPM;
    ColorAdjustments CA;
    Vignette Vig;
    ChromaticAberration ChrAb;
    PaniniProjection FishEye;
    DepthOfField DOF;
    LensDistortion LD;
    private bool isQueuing;
    private bool PS_Running;
    private Queue<IEnumerator> coroutinesQueue = new Queue<IEnumerator>();

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
        PpVolume.profile.TryGet<PaniniProjection>(out FishEye);
        PpVolume.profile.TryGet<LensDistortion>(out LD);
        PpVolume.profile.TryGet<DepthOfField>(out DOF);
    }

    private void Start()
    {
        StartCoroutine(CoroutineCoordinator());
    }

    private void Update()
    {
        if((Input.GetButtonDown("LB") || Input.GetKeyDown(KeyCode.P)) && coroutinesQueue.Count <= 0) // Move this shit mountain into playerScriptNew
        {
            PS_Running = true;
            PlayerScriptNew.me.isPoliceSense = true;
            PlayerScriptNew.me.StopPlayer();
            coroutinesQueue.Enqueue(DistorsionFilter());
        }
        if((Input.GetButtonUp("LB") || Input.GetKeyUp(KeyCode.P)) && PS_Running)
        {
            coroutinesQueue.Enqueue(ResetPolice());
            PS_Running = false;
            PlayerScriptNew.me.isPoliceSense = false;
        }

        if (coroutinesQueue.Count > 0)
            isQueuing = true;



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
        yield return new WaitForSeconds(1);

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

    public void GradualDeath(int maxHp, int currentHp)
    {
        float minValue = 0;
        float maxValue = 0.8f;

        float deathValue = 1 - 1.8f * Mathf.Sqrt((float)currentHp/maxHp);
        CA.saturation.value = Mathf.Clamp(deathValue * 100, minValue, maxValue*100);
        Vig.intensity.value = Mathf.Clamp(deathValue/2.2f, minValue, maxValue);
        ChrAb.intensity.value = Mathf.Clamp(deathValue, minValue, maxValue);
        //Debug.Log(deathValue);
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
            //Debug.Log("black");
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
            // reborn
            SavePointManager.me.ResetPlayer();
            SavePointManager.me.ResetBears();
            SafehouseManager.Me.isSafehouse = true;
        }
    }

    private IEnumerator CoroutineCoordinator()
    {
        while (true)
        {
            while (coroutinesQueue.Count > 0)
                yield return StartCoroutine(coroutinesQueue.Dequeue());
            yield return null;
        }
    }
    
    public IEnumerator PoliceSenceEffect()
    {
        if (CA != null)
        {
            float time = 0;
            float originalSat = CA.saturation.value;
            float originalDist = FishEye.distance.value;

            while (CA.saturation.value != -100)
            {
                Debug.Log("black");
                time += Time.fixedDeltaTime;
                CA.saturation.value = Mathf.Lerp(originalSat, -100, time);
                //FishEye.distance.value = Mathf.Lerp(originalDist, 0.5f, time);
                yield return null;
            }

            //FishEye.distance.value = 0.5f;
        }
    }

    public IEnumerator ResetPolice()
    {
        if (CA != null)
        {
            PolicePPM.GetComponent<PoliceSencePPManager>().StartCoroutine(PolicePPM.GetComponent<PoliceSencePPManager>().ResetPanini());
            float time = 0;
            float originalSat = CA.saturation.value;
            float originalDist = FishEye.distance.value;

            while (CA.saturation.value != 0)
            {
                Debug.Log("resetP");
                time += Time.deltaTime;
                CA.saturation.value = Mathf.Lerp(originalSat, 0, time);
                //FishEye.distance.value = Mathf.Lerp(originalDist, 0, time);
                yield return null;
            }

            CA.saturation.value = 0;
        }

        
    }

    public IEnumerator DistorsionFilter()
    {
        if (DOF != null && LD != null)
        {
            PolicePPM.GetComponent<PoliceSencePPManager>().StartCoroutine(PolicePPM.GetComponent<PoliceSencePPManager>().PoliceDistorsionFilter());
            float distAmount = 0.7f;
            float distance = 0.1f;
            StartCoroutine(PoliceSenceEffect());

            while (distAmount >= 0)
            {
                LD.intensity.value = distAmount;
                DOF.focusDistance.value = distance;
                distAmount -= 0.01f;
                distance += 0.04f;
                yield return new WaitForSecondsRealtime(0.01f * Time.deltaTime);
            }

            LD.intensity.value = 0;
            DOF.focusDistance.value = 10;
        }

    }

}
