using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBearRoomManager : MonoBehaviour
{
    public List<GameObject> bears_iCtrl;
	public List<DoorScript> doors_iCtrl;

    public void BearStart()
	{
		BGMMan.bGMManger.StartTinyTeddyCombatMusic();
		foreach (var bear in bears_iCtrl)
		{
			bear.GetComponent<AIController>().enabled = true;
			bear.GetComponent<SmallBear>().enabled = true;
			bear.GetComponent<EffectHoldersHolderScript>().enabled = true;
		}
	}

	private void Update()
	{
		bool clear = true;
		if (bears_iCtrl.Count > 0)
		{
			foreach (var bear in bears_iCtrl)
			{
				if (bear.GetComponent<SmallBear>()!=null && bear.GetComponent<SmallBear>().health > 0)
				{
					clear = false;
				}
			}
			if (clear)
			{
				BGMMan.bGMManger.EndTinyTeddyMusic();
				if (doors_iCtrl.Count > 0)
				{
					foreach (var door in doors_iCtrl)
					{
						door.ControllDoor();
					}
				}
			}
		}
		
	}
}
