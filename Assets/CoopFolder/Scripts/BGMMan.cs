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
        
        BGMAudioSource.loop = false;
        FadeTrack(daughterRoom);
    }

    public void BathRoomBloodMusic()
    {

        BGMAudioSource.loop = false;
        FadeTrack(bathroomBlood);
    }

    public void YangtaiPuzzleMusic()
    {
        
        BGMAudioSource.loop = false;
        FadeTrack(yangTaiPuzzle);
    }

    public void EndCreditMusic()
    {
        
        BGMAudioSource.loop = false;
        FadeTrack(theme);
    }

    public void EnterSafeHoueBaguaMusic()
    {
        SoundMan.SoundManager.ChangeToSafeHouseSnapshot();
        
        BGMAudioSource.loop = true;
        FadeTrack(safeHouse);
    }

    public void EndSafeHoueBaguaMusic()
    {
        SoundMan.SoundManager.ChangeToSafeHouseSnapshot();
        
        BGMAudioSource.loop = false;
        FadeTrack(null);
    }

    public void StartBattleMusic()
    {
        SoundMan.SoundManager.ChangeToCombatSnapshot();
        
        BGMAudioSource.loop = true;
        FadeTrack(battleMusic);
    }

    public void EndBattleMusic()
    {
        SoundMan.SoundManager.ChangeToNormalSnapshot();
        
        BGMAudioSource.loop = false;
        FadeTrack(null);

    }

    private IEnumerator FadeTrack(AudioClip clip)
    {
        float fadeInTime = 0.25f;
        float fadeInTimeElapsed = 0f;

        while (fadeInTimeElapsed < fadeInTime)
        {
            BGMAudioSource.volume = Mathf.Lerp(1, 0, fadeInTimeElapsed / fadeInTime);
            fadeInTimeElapsed += Time.deltaTime;
            yield return null;
        }

        BGMAudioSource.Stop();
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
