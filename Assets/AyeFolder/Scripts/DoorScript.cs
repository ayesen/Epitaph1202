using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Animator DoorAnimator;
    public bool isOpen = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.B))
        {
            OpenFront();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            CloseFront();
        }*/
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
