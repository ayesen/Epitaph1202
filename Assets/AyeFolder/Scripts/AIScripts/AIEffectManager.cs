using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEffectManager : MonoBehaviour
{
    public GameObject SoundWaveVFX;
    private float time = 0;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.J))
        {
            StartCoroutine(StartSoundWave());
        }
    }

    public void ResetSoundWave()
    {
        SoundWaveVFX.SetActive(false);
        time = 0;
        SoundWaveVFX.GetComponent<MeshRenderer>().material.SetFloat("WaveAmt", -1);
    }

    public IEnumerator StartSoundWave()
    {
        SoundWaveVFX.SetActive(true);
        while (time < 0.5)
        {
            time += Time.fixedDeltaTime * 0.2f;
            SoundWaveVFX.GetComponent<MeshRenderer>().material.SetFloat("WaveAmt", time);
            yield return null;
        }
        ResetSoundWave();
    }


}
