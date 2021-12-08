using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMMan : MonoBehaviour
{
    AudioSource BGMAudioSource;

    public AudioClip battleMusic;
    public AudioClip daughterRoom;
    public AudioClip bathroomBlood;
    public AudioClip yangTaiPuzzle;
    public AudioClip theme;

    private void Start()
    {
        BGMAudioSource = GetComponent<AudioSource>();
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
    public void StartBattleMusic()
    {
        BGMAudioSource.Stop();
        BGMAudioSource.loop = true;
        BGMAudioSource.clip = battleMusic;
        BGMAudioSource.Play();
    }

    public void EndBattleMusic()
    {
        BGMAudioSource.Stop();
        BGMAudioSource.loop = false;

    }
}
