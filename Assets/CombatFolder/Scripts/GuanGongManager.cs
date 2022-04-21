using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuanGongManager : MonoBehaviour
{
	public GameObject bear_prefab;
	public int amountToSpawn;
	public List<GameObject> bearsISpawned;
	public float randomAmount;
	public float detectDis_bear;
	public float detectDis_player;
	public float playerViewAngle;

    public void Event_SpawnSmallBear()
    {
		for (int i = 0; i < amountToSpawn; i++)
		{
			Vector3 bearPos = ReturnSpawnPos();
			if (bearPos != Vector3.zero)
            {
				GameObject bear = Instantiate(bear_prefab);
				bear.transform.position = bearPos;
				bearsISpawned.Add(bear);
			}
		}
		// look towards player
        foreach (var bear in bearsISpawned)
        {
			Vector3 lookTarget = new Vector3(PlayerScriptNew.me.transform.position.x, bear.transform.position.y, PlayerScriptNew.me.transform.position.z);
			bear.transform.LookAt(lookTarget);
        }
    }

	private Vector3 ReturnSpawnPos()
    {
		Vector3 playerPos = PlayerScriptNew.me.transform.position;

		Vector3 bearPos = new Vector3(playerPos.x + Random.Range(-randomAmount, randomAmount),
				4.56f,
				playerPos.z + Random.Range(-randomAmount, randomAmount));
		if (bearsISpawned.Count > 0)
        {
			foreach (var anotherBear in bearsISpawned)
			{
				if (Vector3.Distance(bearPos, anotherBear.transform.position) < detectDis_bear)
                {
					return Vector3.zero;
                }
			}
			if (Vector3.Distance(bearPos, playerPos) > detectDis_player &&
                PlayersViewCheck(bearPos))
            {
				return bearPos;
			}
		}
        else
        {
			if (PlayersViewCheck(bearPos))
            {
				return bearPos;
			}
        }
		return Vector3.zero;
	}

	private bool PlayersViewCheck(Vector3 pos)
    {
		Transform playerTransform = PlayerScriptNew.me.transform;
		Vector3 bearDir = pos - playerTransform.position;
		if (Vector3.Angle(playerTransform.forward, bearDir) < playerViewAngle)
        {
			return false;
		}
		return true;
    }
}