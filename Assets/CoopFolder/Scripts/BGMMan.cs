using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMMan : MonoBehaviour
{
    public static BGMMan bGMManger;
    public AudioSource BGMAudioSource;

    public AudioClip battleMusic;
    public AudioClip lordGuan;
    bool lordGuanPlayed;
    public AudioClip firstTinyTeddy;
    bool firstTinyTeddyPlayed;
    public AudioClip tinyBattleMusic;
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

    public void SecondGuangongMusic()
    {
        if (!lordGuanPlayed)
        {
            lordGuanPlayed = true;
            BGMAudioSource.loop = false;
            StartCoroutine(FadeTrack(lordGuan));
        }

    }

    public void FirstTinyTeddyMusic() //after the first teddy disappeared
    {
        if (!firstTinyTeddyPlayed)
        {
            firstTinyTeddyPlayed = true;
            BGMAudioSource.loop = false;
            StartCoroutine(FadeTrack(firstTinyTeddy));
        }


    }

    public void EndCreditMusic()
    {
        
        BGMAudioSource.loop = false;
        StartCoroutine(FadeTrack(theme));
    }

    public void EnterSafeHoueBaguaMusic()
    {
        SoundMan.SoundManager.ChangeToSafeHouseSnapshot();
        
        BGMAudioSource.loop = true;
        StartCoroutine(FadeTrack(safeHouse));
    }

    public void EndSafeHoueBaguaMusic()
    {
        SoundMan.SoundManager.ChangeToNormalSnapshot();
        
        BGMAudioSource.loop = false;
        StartCoroutine(FadeTrack(null));
    }

    public void StartTinyTeddyCombatMusic()
    {
        if(BGMAudioSource.clip != tinyBattleMusic)
        {
            print("Start Tiny Combat Music");
            SoundMan.SoundManager.ChangeToCombatSnapshot();
            BGMAudioSource.loop = true;
            StartCoroutine(FadeTrack(tinyBattleMusic));
        }

    }

    public void EndTinyTeddyMusic()
    {
        SoundMan.SoundManager.ChangeToNormalSnapshot();

        BGMAudioSource.loop = false;
        StartCoroutine(FadeTrack(null));
    }

    public void StartTeddyBattleMusic()
    {
        if(!(BGMAudioSource.isPlaying && (BGMAudioSource.clip == battleMusic)))


        SoundMan.SoundManager.ChangeToCombatSnapshot();
        
        BGMAudioSource.loop = true;
        StartCoroutine(FadeTrack(battleMusic));
    }

    public void EndTeddyBattleMusic()
    {
        SoundMan.SoundManager.ChangeToNormalSnapshot();
        
        BGMAudioSource.loop = false;
        StartCoroutine(FadeTrack(null));

    }

    private IEnumerator FadeTrack(AudioClip clip)
    {
        if (BGMAudioSource.isPlaying)
        {
            float fadeInTime = 0.25f;
            float fadeInTimeElapsed = 0f;
            float currentVolume = BGMAudioSource.volume;
            
            while (fadeInTimeElapsed < fadeInTime)
            {
                BGMAudioSource.volume = Mathf.Lerp(1, 0, fadeInTimeElapsed / fadeInTime);
                fadeInTimeElapsed += Time.deltaTime;
                yield return null;
            }
            
            BGMAudioSource.Stop();
        }

        if (clip != null)
        {
            BGMAudioSource.clip = clip;
            BGMAudioSource.Play();
            
            float fadeOutTime = 0.25f;
            float fadeOutTimeElapsed = 0f;
            
            while (fadeOutTimeElapsed < fadeOutTime)
            {
                BGMAudioSource.volume = Mathf.Lerp(0, 1, fadeOutTimeElapsed / fadeOutTime);
                fadeOutTimeElapsed += Time.deltaTime;
                yield return null;
            }
        }
        

    }
}
