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
            OpenFront();
		}
	}

	public doorAnim animState;

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
                break;
            case doorAnim.closeBack:
                stateName = "DoorBackClose";
                isOpen = false;
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
    
    public void OpenFront()
    {
        DoorAnimator.Play("DoorFrontOpen");
        isOpen = true;
    }

    public void CloseFront()
    {
        DoorAnimator.Play("DoorFrontClose");
        isOpen = false;
    }

    public void OpenBack()
    {
        DoorAnimator.Play("DoorBackOpen");
        isOpen = true;
    }

    public void CloseBack()
    {
        DoorAnimator.Play("DoorBackClose");
        isOpen = false;
    }

    public void OpenBack_and_reactivateBears() // front actually
	{
        DoorAnimator.Play("DoorFrontOpen");
        isOpen = true;
		foreach (var bear in bearsBehind)
		{
            print(bear.gameObject.name);
            bear.GetComponent<AIController>().enabled = true;
		}
	}
}
