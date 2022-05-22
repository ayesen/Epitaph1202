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

        switch (WallHider.me.roomPlayerIsIn)
        {
            case WallHider.Room.spawnHallway:
                AmbienceManager.ambienceManager.HallwayAmbiencePlay();
                break;
            case WallHider.Room.balcany:
            case WallHider.Room.studyBalcony:
                AmbienceManager.ambienceManager.BalconyAmbiencePlay();
                break;
            default:
                AmbienceManager.ambienceManager.RoomAmbiencePlay();
                break;
        }
        
        //for player footstep sound
        switch (WallHider.me.roomPlayerIsIn)
        {
            case WallHider.Room.spawnHallway:
            case WallHider.Room.balcany:
            case WallHider.Room.studyBalcony:
                SoundMan.SoundManager.FootStepSwitchToConcret();
                break;
            case WallHider.Room.bedroom:
            case WallHider.Room.studyRoom:
            case WallHider.Room.subbedRoom:
                SoundMan.SoundManager.FootStepSwitchToWood();
                break;
            default:
                SoundMan.SoundManager.FootStepSwitchToMarble();
                break;
        }
    }
}