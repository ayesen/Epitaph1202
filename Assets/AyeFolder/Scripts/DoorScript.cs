using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Animator DoorAnimator;
    public bool isOpen = false;
    public bool open;
    public GameObject myOpenTrigger;
    public List<GameObject> bearsBehind;

    public enum doorAnim
    {
        doorDefault,
        openFront,
        openBack,
        closeFront,
        closeBack   
    }

	private void Start()
	{
		if (open)
		{
            isOpen = true;
            OpenFront();
        }
	}

	private void Update()
	{
		if (PlayerScriptNew.me.hp <= 0)
		{
            if (open)
			{
                OpenDoor();
			}
		}
	}

	public doorAnim animState;

    public void SwitchDoor()
    {
        if (isOpen)
        {
            switch (animState)
            {
                case DoorScript.doorAnim.openFront:
                    animState = DoorScript.doorAnim.closeFront;
                    break;
                case DoorScript.doorAnim.openBack:
                    animState = DoorScript.doorAnim.closeBack;
                    break;
                default:
                    animState = DoorScript.doorAnim.closeFront;
                    break;
            }
            ControllDoor();
        }
        else if (!isOpen)
        {
            switch (animState)
            {
                case DoorScript.doorAnim.closeFront:
                    animState = DoorScript.doorAnim.openFront;
                    break;
                case DoorScript.doorAnim.closeBack:
                    animState = DoorScript.doorAnim.openBack;
                    break;
                default:
                    animState = DoorScript.doorAnim.openFront;
                    break;
            }
            ControllDoor();
        }
    }

    public void ControllDoor()
    {
        string stateName = "";
        switch (animState)
        {
            case doorAnim.openFront:
                stateName = "DoorFrontOpen";
                isOpen = true;
                Destroy(gameObject.GetComponent<DialogueScript>());
                SoundMan.SoundManager.DoorOpen();
                break;
            case doorAnim.openBack:
                stateName = "DoorBackOpen";
                isOpen = true;
                Destroy(gameObject.GetComponent<DialogueScript>());
                SoundMan.SoundManager.DoorOpen();
                break;
            case doorAnim.closeFront:
                stateName = "DoorFrontClose";
                isOpen = false;
                SoundMan.SoundManager.DoorOpen();
                break;
            case doorAnim.closeBack:
                stateName = "DoorBackClose";
                isOpen = false;
                SoundMan.SoundManager.DoorOpen();
                break;
            default:
                stateName = "DoorFrontOpen";
                isOpen = true;
                Destroy(gameObject.GetComponent<DialogueScript>());
                SoundMan.SoundManager.DoorOpen();
                break;
        }
        DoorAnimator.Play(stateName);
    }
    
    private void OpenFront()
    {
        DoorAnimator.Play("DoorFrontOpen");
        isOpen = true;
    }

    private void CloseFront()
    {
        DoorAnimator.Play("DoorFrontClose");
        isOpen = false;
    }

    private void OpenBack()
    {
        DoorAnimator.Play("DoorBackOpen");
        isOpen = true;
    }

    private void CloseBack()
    {
        DoorAnimator.Play("DoorBackClose");
        isOpen = false;
    }

    public void OpenBack_and_reactivateBears() // front actually
	{
        SwitchDoor();
        isOpen = true;
        BGMMan.bGMManger.StartTeddyBattleMusic();
        foreach (var bear in bearsBehind)
		{
            bear.GetComponent<AIController>().enabled = true;
		}
	}

    public void CloseDoor()
	{
        if (isOpen)
        {
            switch (animState)
            {
                case DoorScript.doorAnim.openFront:
                    animState = DoorScript.doorAnim.closeFront;
                    break;
                case DoorScript.doorAnim.openBack:
                    animState = DoorScript.doorAnim.closeBack;
                    break;
                default:
                    animState = DoorScript.doorAnim.closeFront;
                    break;
            }
            ControllDoor();
        }
    }

    public void OpenDoor()
	{
        if (!isOpen)
		{
			switch (animState)
			{
                case DoorScript.doorAnim.closeFront:
                    animState = DoorScript.doorAnim.openFront;
                    break;
                case DoorScript.doorAnim.closeBack:
                    animState = DoorScript.doorAnim.openBack;
                    break;
                default:
                    animState = DoorScript.doorAnim.openFront;
                    break;
            }
            ControllDoor();
        }
	}
}
