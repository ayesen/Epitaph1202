using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetector : MonoBehaviour
{
    public WallHider.Room whichRoomIAm;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
			WallHider.me.roomPlayerIsIn = whichRoomIAm;
	}
}
