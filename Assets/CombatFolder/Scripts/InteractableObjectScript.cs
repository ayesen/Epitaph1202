using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectScript : MonoBehaviour
{
	public GameObject reactionMat;
	public List<GameObject> objectsToShow;
	public List<GameObject> objectsToHide;
	
	public string methodToCall;
	public GameObject methodsOwner;

	public bool triggerOn = false;

    private void Start()
    {
		triggerOn = false;
    }

    public void Reaction()
	{
		print("have reaction");
		if (objectsToShow.Count > 0) // show things
		{
			foreach (var thing in objectsToShow)
			{
				thing.SetActive(true);
			}
		}
		if (objectsToHide.Count > 0) // hide things
		{
			foreach (var thing in objectsToHide)
			{
				thing.SetActive(false);
			}
		}
		if (methodToCall.Length > 0)
        {
			methodsOwner.SendMessage(methodToCall);
		}
		triggerOn = true;
	}
}
