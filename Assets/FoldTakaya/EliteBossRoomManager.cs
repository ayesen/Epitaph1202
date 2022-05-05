using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteBossRoomManager : MonoBehaviour
{
    public GameObject FoPai;

    public void ActiveFoPai()
    {
        if(FoPai != null)
        {
            if(FoPai.GetComponent<BoxCollider>() != null)
            {
                if(FoPai.GetComponent<DialogueScript>() != null)
                {
                    FoPai.GetComponent<BoxCollider>().enabled = true;
                    FoPai.GetComponent<DialogueScript>().enabled = true;
                }
            }
        }
    }
}
