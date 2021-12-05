using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMan : MonoBehaviour
{
    public static SoundMan SoundManager;
    public int maxAudioSouces;
    private AudioSource[] sources;
    public AudioSource sourcePrefab;
    public AudioClip[] interactClips;
    int lastInteract;

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
    
    
    
    public void Interact(Vector3 pos)
    {
        AudioSource source = GetSource();
        int clipNum = GetClipIndex(interactClips.Length, lastInteract);
        lastInteract = clipNum;
        source.clip = interactClips[clipNum];
        source.transform.position = pos;
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
