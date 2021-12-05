using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceManager : MonoBehaviour
{
    private GameObject player;

    private AudioSource ambienceSource;
    
    public AudioClip[] ambienceClips; //0 is hall way, 1 is living room

    private BoxCollider hallwayTrigger;
    private BoxCollider roomtrigger;
    private void Start()
    {
        ambienceSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("RealPlayer");
        hallwayTrigger = GameObject.Find("HallwayTrigger").GetComponent<BoxCollider>();
        roomtrigger = GameObject.Find("RoomTrigger").GetComponent<BoxCollider>();
    }

    void Update()
    {
        transform.position = player.transform.position; //ambience play at the player
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other == hallwayTrigger)
        {
            HallwayAmbiencePlay();
        }

        if (other == roomtrigger)
        {
            RoomAmbiencePlay();
        }
    }

    public void HallwayAmbiencePlay()
    {
        if (ambienceSource.clip != ambienceClips[0])
        {
            ambienceSource.clip = ambienceClips[0];
            ambienceSource.volume = 0.1f;
            ambienceSource.Play();
        }
    }
    public void RoomAmbiencePlay()
    {
        if (ambienceSource.clip != ambienceClips[1])
        {
            ambienceSource.clip = ambienceClips[1];
            ambienceSource.volume = 1f;
            ambienceSource.Play();
        }
    }
}
