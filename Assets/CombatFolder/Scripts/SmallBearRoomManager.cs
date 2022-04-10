using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBearRoomManager : MonoBehaviour
{
    public List<GameObject> bears_iCtrl;
	public List<DoorScript> doors_iCtrl;
	bool cleared = false;
	public List<DoorScript> entrance_doors;
	
	public void BearStart()
	{
		print("bear start");
		BGMMan.bGMManger.StartTinyTeddyCombatMusic();
		foreach (var bear in bears_iCtrl)
		{
			bear.GetComponent<AIController>().enabled = true;
			bear.GetComponent<SmallBear>().enabled = true;
			bear.GetComponent<EffectHoldersHolderScript>().enabled = true;
			bear.GetComponent<Enemy>().myEntrances = new List<DoorScript>(entrance_doors); // copy entrance list so that all bears know which door to close when player dies
		}
	}

	private void Update()
	{
		bool clear = true;
		if (bears_iCtrl.Count > 0)
		{
			foreach (var bear in bears_iCtrl)
			{
				if (bear.GetComponent<SmallBear>() != null && bear.GetComponent<SmallBear>().health > 0)
				{
					clear = false;
				}
			}
			if (clear && !cleared)
			{
				cleared = true;
				BGMMan.bGMManger.EndMusic();
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
