using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct OptionStruct
{
    public string optionContent; // the text on the option button
    public List<DialogueStruct> dialogues; // the texts after choosing this option
}
