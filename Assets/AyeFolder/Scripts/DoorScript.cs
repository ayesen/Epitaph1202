using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Animator DoorAnimator;
    public bool isOpen = false;

    public enum doorAnim
    {
        doorDefault,
        openFront,
        openBack,
        closeFront,
        closeBack   
    }

    public doorAnim animState;
    void Start()
    {
        
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            //OpenFront();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            //CloseFront();
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
                break;
            case doorAnim.openBack:
                stateName = "DoorBackOpen";
                isOpen = true;
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



}
