using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMan : MonoBehaviour
{
    public static SoundMan SoundManager;
    public int maxAudioSouces;
    private AudioSource[] sources;
    public AudioSource sourcePrefab;
    public AudioMixer mainAudioMixer;
    [Header("SFX")]
    public AudioClip[] manWalkClips;
    public AudioClip[] bearWalkClips;
    public AudioClip[] smallBearWalkClips;
    public AudioClip[] playerHittenClips;
    public AudioClip castFlying;
    public AudioClip materialSelect;
    public AudioClip enemyHitten;
    public AudioClip droppingBall;
    public AudioClip inspection;
    public AudioClip logChangePage;
    public AudioClip logClose;
    public AudioClip logOpen;
    public AudioClip playerCast;
    //public AudioClip playerHitten;
    public AudioClip safehouseMaterialSelect;
    public AudioClip safehouseMaterialSwap;
    public AudioClip bearRoar;
    public AudioClip wallBreak;
    public AudioClip jumpScare;
    public AudioClip cannotAccess;
    public AudioClip doorOpen;
    public AudioClip doorLocked;
    [Header("BattleVO")]
    public AudioClip[] battleVOPhaseOne;
    public AudioClip[] battleVOPhaseTwo;
    int lastManWalk;
    int lastBearWalk;
    int lastSmallBearWalk;
    int lastPlayerHitten;
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

    public void AudioPauseOrUnpause()
    {
        AudioListener.pause = !AudioListener.pause;
    }

    public void PlayerLowHealthFilter(float playerHealth) //player health should percentage from 0-1
    {
        float cutOffHz = 200.0f;
        mainAudioMixer.SetFloat("lowPass", cutOffHz);
    }

    /*The SFX Funcions*/
    public void CannotAccess()
    {
        AudioSource source = GetSource();
        FindSFXGroup(source);
        source.clip = cannotAccess;
        source.Play();
    }

    public void DoorOpen()
    {
        AudioSource source = GetSource();
        FindSFXGroup(source);
        source.clip = doorOpen;
        source.Play();
    }

    public void DoorLocked()
    {
        AudioSource source = GetSource();
        FindSFXGroup(source);
        source.clip = doorLocked;
        source.Play();
    }

    public void WallBreaks()  //maybe need to add something to prevent it over playing
    {
        AudioSource source = GetSource();
        FindSFXGroup(source);
        source.clip = wallBreak;
        source.Play();
    }

    public void BearRoar()
    {
        AudioSource source = GetSource();
        FindSFXGroup(source);
        source.clip = bearRoar;
        source.Play();
    }

    public void SafehouseMaterialSelect() // this couold also be used as the UI selection sound, such as in pause menu and start menu.
    {
        AudioSource source = GetSource();
        source.ignoreListenerPause = true;
        FindSFXGroup(source);
        source.clip = safehouseMaterialSelect;
        source.Play();
    }

    public void SafehouseMaterialSwap()
    {
        AudioSource source = GetSource();
        FindSFXGroup(source);
        source.clip = safehouseMaterialSwap;
        source.Play();
    }
    public void PlayerCast()
    {
        AudioSource source = GetSource();
        FindSFXGroup(source);
        source.clip = playerCast;
        source.Play();
    }

    public void PlayerHitten()
    {
        AudioSource source = GetSource();
        FindSFXGroup(source);
        int clipNum = GetClipIndex(playerHittenClips.Length, lastPlayerHitten);
        lastPlayerHitten = clipNum;
        source.clip = playerHittenClips[clipNum];
        //source.transform.position = pos;
        source.Play();
    }

    //public void LogClose()
    //{
    //    AudioSource source = GetSource();
    //    source.clip = logClose;
    //    source.Play();
    //}

    //public void LogOpen()
    //{
    //    AudioSource source = GetSource();
    //    source.clip = logOpen;
    //    source.Play();
    //}

    //public void LogChangePage()
    //{
    //    AudioSource source = GetSource();
    //    source.clip = logChangePage;
    //    source.Play();
    //}

    public void ItemInspection()
    {
        AudioSource source = GetSource();
        FindSFXGroup(source);
        source.clip = inspection;
        source.Play();
    }

    public void DroppingBall()
    {
        AudioSource source = GetSource();
        FindSFXGroup(source);
        source.clip = droppingBall;
        source.Play();
    }

    public void EnemyHitten()
    {
        AudioSource source = GetSource();
        FindSFXGroup(source);
        source.clip = enemyHitten;
        source.Play();
    }

    public void CastFlying()
    {
        AudioSource source = GetSource();
        FindSFXGroup(source);
        source.clip = castFlying;
        source.Play();
    }

    public void MaterialSelect()
    {
        AudioSource source = GetSource();
        FindSFXGroup(source);
        source.clip = materialSelect;
        source.Play();
    }

    public void JumpScare()
    {
        AudioSource source = GetSource();
        FindSFXGroup(source);
        source.clip = jumpScare;
        source.Play();
    }

    public void ManWalk()
    {
        AudioSource source = GetSource();
        FindWalkingGroup(source);
        int clipNum = GetClipIndex(manWalkClips.Length, lastManWalk);
        lastManWalk = clipNum;
        source.clip = manWalkClips[clipNum];
        //source.transform.position = pos;
        source.Play();
    }

    public void BearWalk()
    {
        AudioSource source = GetSource();
        FindEnemyWalkingGroup(source);
        int clipNum = GetClipIndex(bearWalkClips.Length, lastBearWalk);
        lastBearWalk = clipNum;
        source.clip = bearWalkClips[clipNum];
        //source.transform.position = pos;
        source.Play();
    }

    public void SmallBearWalk()
    {
        AudioSource source = GetSource();
        FindEnemyWalkingGroup(source);
        int clipNum = GetClipIndex(smallBearWalkClips.Length, lastSmallBearWalk);
        lastSmallBearWalk = clipNum;
        source.clip = smallBearWalkClips[clipNum];
        //source.transform.position = pos;
        source.Play();
    }


    /*Audio Mixer Snapshots management functions*/
    public void ChangeToNormalSnapshot()
    {
        mainAudioMixer.FindSnapshot("InGame_Normal_Snapshot").TransitionTo(0.5f);
    }

    public void ChangeToCombatSnapshot()
    {
        mainAudioMixer.FindSnapshot("InGame_Combat_Snapshot").TransitionTo(0.5f);
    }

    public void ChangeToSafeHouseSnapshot()
    {
        mainAudioMixer.FindSnapshot("SafeHouse_Snapshot").TransitionTo(0.5f);
    }

    public void FindSFXGroup(AudioSource aS)
    {
        aS.outputAudioMixerGroup = mainAudioMixer.FindMatchingGroups("SFX")[0];
    }

    public void FindEnemyWalkingGroup(AudioSource aS)
    {
        aS.outputAudioMixerGroup = mainAudioMixer.FindMatchingGroups("SFX/EnemyWalking")[0];
    }

    public void FindWalkingGroup(AudioSource aS)
    {
        aS.outputAudioMixerGroup = mainAudioMixer.FindMatchingGroups("SFX/Walking")[0];
    }

    public void FindBGMGroup(AudioSource aS)
    {
        aS.outputAudioMixerGroup = mainAudioMixer.FindMatchingGroups("BGM")[0];
    }

    public void FindAmbienceGroup(AudioSource aS)
    {
        aS.outputAudioMixerGroup = mainAudioMixer.FindMatchingGroups("Ambience")[0];
    }

    public void FindVOGroup(AudioSource aS)
    {
        aS.outputAudioMixerGroup = mainAudioMixer.FindMatchingGroups("VO")[0];
    }


    /*Initialization functions*/
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

    /* not in use functions*/
    public void BattlePhaseOneVO() //not in use right now
    {
        AudioSource source = GetSource();

        FindVOGroup(source);

        int clipNum = GetClipIndex(battleVOPhaseOne.Length, lastBPOne);
        lastBPOne = clipNum;
        source.clip = battleVOPhaseOne[clipNum];
        //source.transform.position = pos;
        source.volume = 1;
        source.Play();
    }

    public void BattlePhaseTwoVO() //also not in use
    {
        AudioSource source = GetSource();

        FindVOGroup(source);

        int clipNum = GetClipIndex(battleVOPhaseTwo.Length, lastBPTwo);
        lastBPTwo = clipNum;
        source.clip = battleVOPhaseTwo[clipNum];
        //source.transform.position = pos;
        source.Play();
    }


}
