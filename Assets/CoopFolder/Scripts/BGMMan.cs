using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMMan : MonoBehaviour
{
    public static BGMMan bGMManger;
    public AudioSource BGMAudioSource;

    public AudioClip battleMusic;
    public AudioClip daughterRoom;
    public AudioClip bathroomBlood;
    public AudioClip yangTaiPuzzle;
    public AudioClip theme;
    public AudioClip safeHouse;

    private void Awake()
    {
        if (bGMManger != null)
        {
            Destroy(gameObject);
            return;
        }
        bGMManger = this;
    }
    private void Start()
    {
        BGMAudioSource = GetComponent<AudioSource>();
        SoundMan.SoundManager.FindBGMGroup(BGMAudioSource);
    }

    public void DaughterRoomMusic()
    {
        BGMAudioSource.Stop();
        BGMAudioSource.loop = false;
        BGMAudioSource.clip = daughterRoom;
        BGMAudioSource.Play();
    }

    public void BathRoomBloodMusic()
    {
        BGMAudioSource.Stop();
        BGMAudioSource.loop = false;
        BGMAudioSource.clip = bathroomBlood;
        BGMAudioSource.Play();
    }

    public void YangtaiPuzzleMusic()
    {
        BGMAudioSource.Stop();
        BGMAudioSource.loop = false;
        BGMAudioSource.clip = yangTaiPuzzle;
        BGMAudioSource.Play();
    }

    public void EndCreditMusic()
    {
        BGMAudioSource.Stop();
        BGMAudioSource.loop = false;
        BGMAudioSource.clip = theme;
        BGMAudioSource.Play();
    }

    public void EnterSafeHoueBaguaMusic()
    {
        SoundMan.SoundManager.ChangeToSafeHouseSnapshot();
        BGMAudioSource.Stop();
        BGMAudioSource.loop = true;
        BGMAudioSource.clip = safeHouse;
        BGMAudioSource.Play();
    }

    public void EndSafeHoueBaguaMusic()
    {
        SoundMan.SoundManager.ChangeToSafeHouseSnapshot();
        BGMAudioSource.Stop();
        BGMAudioSource.loop = false;
    }

    public void StartBattleMusic()
    {
        SoundMan.SoundManager.ChangeToCombatSnapshot();
        BGMAudioSource.Stop();
        BGMAudioSource.loop = true;
        BGMAudioSource.clip = battleMusic;
        BGMAudioSource.Play();
    }

    public void EndBattleMusic()
    {
        SoundMan.SoundManager.ChangeToNormalSnapshot();
        BGMAudioSource.Stop();
        BGMAudioSource.loop = false;

    }
}
