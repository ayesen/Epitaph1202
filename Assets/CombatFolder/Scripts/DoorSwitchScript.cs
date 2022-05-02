using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DoorSwitchScript : MonoBehaviour
{
	public List<DoorScript> doors_iCtrl;
	public bool OpenDoor;

	private void OnTriggerEnter(Collider other)
	{
		SwitchDoor(other);
	}

	private void SwitchDoor(Collider other)
	{
		if (other.gameObject.CompareTag("Player") && doors_iCtrl.Count > 0)
		{
			foreach (var door in doors_iCtrl)
			{
				if (door.isOpen && !OpenDoor)
				{
					switch (door.animState)
					{
						case DoorScript.doorAnim.openFront:
							door.animState = DoorScript.doorAnim.closeFront;
							break;
						case DoorScript.doorAnim.openBack:
							door.animState = DoorScript.doorAnim.closeBack;
							break;
						default:
							door.animState = DoorScript.doorAnim.closeFront;
							break;
					}
					door.ControllDoor();
					enabled = false;
				}
				else if (!door.isOpen && OpenDoor)
				{
					switch (door.animState)
					{
						case DoorScript.doorAnim.closeFront:
							door.animState = DoorScript.doorAnim.openFront;
							break;
						case DoorScript.doorAnim.closeBack:
							door.animState = DoorScript.doorAnim.openBack;
							break;
						default:
							door.animState = DoorScript.doorAnim.openFront;
							break;
					}
					door.ControllDoor();
					enabled = false;
				}
			}
		}
	}
}
