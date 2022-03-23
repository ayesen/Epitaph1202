using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceManager : MonoBehaviour
{
    public static AmbienceManager ambienceManager;
    
    private GameObject player;

    private AudioSource ambienceSource;
    
    public AudioClip[] ambienceClips; //0 is hall way, 1 is living room


    private void Awake()
    {
        if (ambienceManager != null)
        {
            Destroy(gameObject);
            return;
        }
        ambienceManager = this;
    }

    private void Start()
    {
        ambienceSource = GetComponent<AudioSource>();
        SoundMan.SoundManager.FindAmbienceGroup(ambienceSource);
        player = GameObject.FindGameObjectWithTag("RealPlayer");

    }

    void Update()
    {
        transform.position = player.transform.position; //ambience play at the player

    }

    //public void OnTriggerEnter(Collider other)
    //{
    //    if (other == hallwayTrigger)
    //    {
    //        HallwayAmbiencePlay();
    //    }

    //    if (other == roomTriggerOne || other == roomTriggerTwo || other == roomTriggerThree)
    //    {
    //        RoomAmbiencePlay();
    //    }

    //    //if(other == balconyTriggerOne || other == balconyTriggerTwo)
    //    //{
    //    //    BalconyAmbiencePlay();
    //    //}

    //}

    public void HallwayAmbiencePlay()
    {
        if (ambienceSource.clip != ambienceClips[0])
        {
            SoundMan.SoundManager.ChangeToNormalSnapshot();
            ambienceSource.clip = ambienceClips[0];
            ambienceSource.volume = 0.1f;
            ambienceSource.Play();
        }
    }
    public void RoomAmbiencePlay()
    {
        if (ambienceSource.clip != ambienceClips[1])
        {
            SoundMan.SoundManager.ChangeToNormalSnapshot();
            ambienceSource.clip = ambienceClips[1];
            ambienceSource.volume = 0.5f;
            ambienceSource.Play();
        }
    }

    public void BalconyAmbiencePlay()
    {
        if (ambienceSource.clip != ambienceClips[2])
        {
            SoundMan.SoundManager.ChangeToNormalSnapshot();
            ambienceSource.clip = ambienceClips[2];
            ambienceSource.volume = 1f;
            ambienceSource.Play();
        }
    }

    //public void SafeHouseAmbiencePlay()
    //{
    //    BGMMan.bGMManger.BGMAudioSource.Stop();
    //    SoundMan.SoundManager.ChangeToSafeHouseSnapshot();
    //    ambienceSource.clip = ambienceClips[3];
    //    ambienceSource.volume = 1f;
    //    ambienceSource.Play();
    //}
}
