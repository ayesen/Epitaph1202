using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitchScript : MonoBehaviour
{
	public List<DoorScript> doors_iCtrl;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player") && doors_iCtrl.Count > 0)
		{
			foreach (var door in doors_iCtrl)
			{
				if (door.isOpen)
				{
					door.CloseFront();
				}
			}
		}
	}
}
