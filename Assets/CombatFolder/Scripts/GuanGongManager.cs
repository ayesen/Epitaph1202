using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuanGongManager : MonoBehaviour
{
	[Header("For Spawning Small Bears Around Player")]
	public GameObject bear_prefab;
	public int amountToSpawn;
	public List<GameObject> bearsISpawned;
	public float randomAmount;
	public float detectDis_bear;
	public float detectDis_player;
	public float playerViewAngle;

	[Header("For Spawning GuanSword")]
	public GameObject sword;
	public bool sword_picked;

	[Header("For Swapping GG")]
	public GameObject GG_og; // og gg
	public GameObject GG_withoutSword;
	public GameObject GG_wS_dialogue_swordPicked; // dialogue that knows player picked up the sword: this will swap the GG without sword with a GG with sword
	public GameObject GG_wS_dialogue_swordNotPicked; // dialogue that knows player haven't picked up the sword

	[Header("For Swapping GG the second time")]
	public GameObject GG_withSword; // this will open the door

	#region Events
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
		// spawn sword
		Event_SpawnSword();
		// swap GG
		Event_SwapGG_1();

		SoundMan.SoundManager.LordGuanTrigger();
	}

	public void Event_SpawnSword()
    {
		sword.SetActive(true);
    }

	public void Event_PickUpSword()
    {
		sword_picked = true;
		GG_wS_dialogue_swordPicked.SetActive(true);
		GG_wS_dialogue_swordNotPicked.SetActive(false);
		Destroy(sword);
		foreach (var bear in bearsISpawned)
        {
			Destroy(bear);
        }
		bearsISpawned.Clear();
    }

	public void Event_SwapGG_1()
    {
		GG_og.SetActive(false);
		GG_withoutSword.SetActive(true);
		// keep the dialogue trigger inactive
		GG_wS_dialogue_swordPicked.SetActive(false);
		GG_wS_dialogue_swordNotPicked.SetActive(false);
	}
	public void Event_SetActive_Dialogue()
	{
		GG_wS_dialogue_swordNotPicked.SetActive(true);
	}

	public void Event_SwapGG_2()
    {
		GG_withoutSword.SetActive(false);
		GG_withSword.SetActive(true);
    }
    #endregion

    #region For Spawning Small Bears around player
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
    #endregion
}