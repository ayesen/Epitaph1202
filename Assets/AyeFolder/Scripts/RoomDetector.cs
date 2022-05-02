using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDetector : MonoBehaviour
{
    public WallHider.Room whichRoomIAmIn;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
        {
            WallHider.me.roomPlayerIsIn = whichRoomIAmIn;
        }
        else
        {
            //print(other.gameObject.tag);
            print(other.gameObject.name);
        }

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