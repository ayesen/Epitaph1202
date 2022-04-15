using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafehouseTutorScript : MonoBehaviour
{
    public GameObject DoorToCtrl;
    public GameObject Dialg_No;
    public GameObject Dialg_Yes;
    private bool doOnce;
    private bool triggered;

    private void OnTriggerEnter(Collider other)
    {
        triggered = true;

        if(triggered != doOnce && triggered)
        {
            doOnce = triggered;
            if (PlayerScriptNew.me.matSlots[0] != null &&
                PlayerScriptNew.me.matSlots[1] != null &&
                PlayerScriptNew.me.matSlots[2] != null)
            {
                GameObject Dialg = Instantiate(Dialg_Yes, transform);
                Dialg.GetComponent<DialogueScript>().enabled = true;
                DoorToCtrl.SendMessage("ControllDoor");
                DoorToCtrl.GetComponent<DoorScript>().open = true;
                Destroy(this);
            }
            else
            {
                GameObject Dialg = Instantiate(Dialg_No, transform);
                Dialg.GetComponent<DialogueScript>().enabled = true;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        triggered = false;
        doOnce = triggered;
    }
}
