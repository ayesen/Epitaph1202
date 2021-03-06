using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueStruct
{
    [TextArea]
    public string description_cht;
    [TextArea]
    public string description_eng;
    public AudioClip clip;
    public float time; // if this dialogue should be played automatically, this is the time for its duration
    public List<OptionStruct> options;
    public int logX;
    public int logY;

    // for roll back
    public bool rollBack;
    public int rollBack_index;

    // for calling a function
    public GameObject actor_oneLine;
    public string funcToCall_oneLine;
}
