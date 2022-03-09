using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBearRoomManager : MonoBehaviour
{
    public List<GameObject> bears_iCtrl;
	public List<DoorScript> doors_iCtrl;

    public void BearStart()
	{
		foreach (var bear in bears_iCtrl)
		{
			bear.GetComponent<AIController>().enabled = true;
			bear.GetComponent<SmallBear>().enabled = true;
		}
	}

	private void Update()
	{
		bool clear = true;
		if (bears_iCtrl.Count > 0)
		{
			foreach (var bear in bears_iCtrl)
			{
				if (bear.GetComponent<SmallBear>().health > 0)
				{
					clear = false;
				}
			}
			if (clear)
			{
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
