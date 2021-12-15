using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMan : MonoBehaviour
{
    public static SoundMan SoundManager;
    public int maxAudioSouces;
    private AudioSource[] sources;
    public AudioSource sourcePrefab;
    [Header("SFX")]
    public AudioClip[] manWalkClips;
    public AudioClip[] bearWalkClips;
    public AudioClip castFlying;
    public AudioClip materialSelect;
    public AudioClip enemyHitten;
    public AudioClip droppingBall;
    public AudioClip inspection;
    public AudioClip logChangePage;
    public AudioClip logClose;
    public AudioClip logOpen;
    public AudioClip playerCast;
    public AudioClip playerHitten;
    public AudioClip safehouseMaterialSelect;
    public AudioClip safehouseMaterialSwap;
    public AudioClip bearRoar;
    public AudioClip wallBreak;
    [Header("BattleVO")]
    public AudioClip[] battleVOPhaseOne;
    public AudioClip[] battleVOPhaseTwo;
    int lastManWalk;
    int lastBearWalk;
    int lastBPOne;
    int lastBPTwo;

    private void Awake()
    {
        if(SoundManager != null)
        {
            Destroy(gameObject);
            return;
        }
        SoundManager = this;

        sources = new AudioSource[maxAudioSouces];
        for (int i = 0; i < maxAudioSouces; i++)
        {
            sources[i] = Instantiate(sourcePrefab);
        }
    }

    public void BattlePhaseOneVO()
    {
        AudioSource source = GetSource();
        int clipNum = GetClipIndex(battleVOPhaseOne.Length, lastBPOne);
        lastBPOne = clipNum;
        source.clip = battleVOPhaseOne[clipNum];
        //source.transform.position = pos;
        source.volume = 1;
        source.Play();
    }

    public void BattlePhaseTwoVO()
    {
        AudioSource source = GetSource();
        int clipNum = GetClipIndex(battleVOPhaseTwo.Length, lastBPTwo);
        lastBPTwo = clipNum;
        source.clip = battleVOPhaseTwo[clipNum];
        //source.transform.position = pos;
        source.Play();
    }
    public void WallBreaks()
    {
        AudioSource source = GetSource();
        source.clip = wallBreak;
        source.Play();
    }

    public void BearRoar()
    {
        AudioSource source = GetSource();
        source.clip = bearRoar;
        source.Play();
    }

    public void SafehouseMaterialSelect()
    {
        AudioSource source = GetSource();
        source.clip = safehouseMaterialSelect;
        source.Play();
    }

    public void SafehouseMaterialSwap()
    {
        AudioSource source = GetSource();
        source.clip = safehouseMaterialSwap;
        source.Play();
    }
    public void PlayerCast()
    {
        AudioSource source = GetSource();
        source.clip = playerCast;
        source.Play();
    }

    public void PlayerHitten()
    {
        AudioSource source = GetSource();
        source.clip = playerHitten;
        source.Play();
    }

    public void LogClose()
    {
        AudioSource source = GetSource();
        source.clip = logClose;
        source.Play();
    }

    public void LogOpen()
    {
        AudioSource source = GetSource();
        source.clip = logOpen;
        source.Play();
    }

    public void LogChangePage()
    {
        AudioSource source = GetSource();
        source.clip = logChangePage;
        source.Play();
    }

    public void ItemInspection()
    {
        AudioSource source = GetSource();
        source.clip = inspection;
        source.Play();
    }

    public void DroppingBall()
    {
        AudioSource source = GetSource();
        source.clip = droppingBall;
        source.Play();
    }

    public void EnemyHitten()
    {
        AudioSource source = GetSource();
        source.clip = enemyHitten;
        source.Play();
    }

    public void CastFlying()
    {
        AudioSource source = GetSource();
        source.clip = castFlying;
        source.Play();
    }

    public void MaterialSelect()
    {
        AudioSource source = GetSource();
        source.clip = materialSelect;
        source.Play();
    }

    public void ManWalk()
    {
        AudioSource source = GetSource();
        int clipNum = GetClipIndex(manWalkClips.Length, lastManWalk);
        lastManWalk = clipNum;
        source.clip = manWalkClips[clipNum];
        //source.transform.position = pos;
        source.Play();
    }

    public void BearWalk()
    {
        AudioSource source = GetSource();
        int clipNum = GetClipIndex(bearWalkClips.Length, lastBearWalk);
        lastBearWalk = clipNum;
        source.clip = bearWalkClips[clipNum];
        //source.transform.position = pos;
        source.Play();
    }

    int GetClipIndex(int clipNum, int lastPlayed)
    {
        int num = Random.Range(0, clipNum);
        while (num == lastPlayed)
            num = Random.Range(0, clipNum);
        return num;
    }

    AudioSource GetSource()
    {
        for (int i = 0; i < maxAudioSouces ; i++)
        {
            if (!sources[i].isPlaying)
                return sources[i];

        }

        return sources[0];
    }


}
