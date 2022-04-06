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

        switch (WallHider.me.roomPlayerIsIn)
        {
            case WallHider.Room.spawnHallway:
                AmbienceManager.ambienceManager.HallwayAmbiencePlay();
                break;
            case WallHider.Room.balcany:
                AmbienceManager.ambienceManager.BalconyAmbiencePlay();
                break;
            default:
                AmbienceManager.ambienceManager.RoomAmbiencePlay();
                break;
        }
    }
}