using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEffectManager : MonoBehaviour
{
    public GameObject SoundWaveVFX;
    private float time = 0;
    public GameObject SlashVFX;
    public float SlashTimer;
    public float SlashWaitTimer;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            StartCoroutine(StartSlash());
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
        while (time < 0.7)
        {
            time += Time.fixedDeltaTime * 0.2f;
            SoundWaveVFX.GetComponent<MeshRenderer>().material.SetFloat("WaveAmt", time);
            yield return null;
        }
        ResetSoundWave();
    }

    public IEnumerator StartSlash()
    {
        yield return new WaitForSeconds(SlashWaitTimer);
        SlashVFX.SetActive(true);
        yield return new WaitForSeconds(SlashTimer);
        SlashVFX.SetActive(false);
        yield return null;

    }


}
