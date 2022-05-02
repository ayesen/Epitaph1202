using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SavePointManager : MonoBehaviour
{
	static public SavePointManager me;
	private GameObject _player;
	private PlayerScriptNew _ps;
	private List<GameObject> _allBears;
	private List<GameObject> _activatedBears;
	public SafeHouseTrigger last_checkPoint;
	public Transform backUp_checkPoint;

	private void Awake()
	{
		me = this;
	}

	private void Start()
	{
		_player = PlayerScriptNew.me.gameObject;
		_ps = PlayerScriptNew.me;
		_allBears = GameObject.FindGameObjectsWithTag("Enemy").ToList();
		_activatedBears = new List<GameObject>();
	}

	public void ResetPlayer()
	{
		// set player position
		if (last_checkPoint == null)
		{
			_player.transform.position = backUp_checkPoint.position;
		}
		else
		{
			_player.transform.position = last_checkPoint.transform.position;
		}
		// reset player stats
		_ps.hp = _ps.maxHP;
		// reset player mats
		if (_ps.matSlots[0] != null)
		{
			_ps.matSlots[0].GetComponent<MatScriptNew>().amount = _ps.matSlots[0].GetComponent<MatScriptNew>().amount_max;
		}
		if (_ps.matSlots[1] != null)
		{
			_ps.matSlots[1].GetComponent<MatScriptNew>().amount = _ps.matSlots[1].GetComponent<MatScriptNew>().amount_max;
		}
		if (_ps.matSlots[2] != null)
		{
			_ps.matSlots[2].GetComponent<MatScriptNew>().amount = _ps.matSlots[2].GetComponent<MatScriptNew>().amount_max;
		}
		//_ps.matSlots[3] = null;
		// reset audios
		SoundMan.SoundManager.CheckPointRevive();
		// reset animation
		_ps.anim.SetBool("died", false);
		_ps.anim.SetFloat("velocity x", 0);
		_ps.anim.SetFloat("velocity z", 0);
	}

	public void ResetBears()
	{
		GetActivatedBears();
		foreach (var bear in _activatedBears)
		{
			bear.transform.position = bear.GetComponent<Enemy>().ogTransform.position;
			bear.transform.rotation = bear.GetComponent<Enemy>().ogTransform.rotation;
			bear.GetComponent<AIController>().enabled = false;
			foreach (var door in bear.GetComponent<Enemy>().myEntrances)
			{
				door.CloseDoor();
				if (door.GetComponent<DialogueScript>() != null)
				{
					door.GetComponent<DialogueScript>().enabled = false;
				}
				door.myOpenTrigger.SetActive(true);
				door.myOpenTrigger.GetComponent<DialogueScript>().enabled = true;
				door.myOpenTrigger.GetComponent<DialogueScript>().inspected = false;
				door.bearsBehind.Add(bear);
			}
		}
	}

	private void GetActivatedBears()
	{
		_activatedBears.Clear();
		// get bears that are activated
		foreach (var bear in _allBears.ToList())
		{
			if (bear != null)
			{
				if (bear.GetComponent<SmallBear>()) // small bear
				{
					if (bear.GetComponent<AIController>().enabled &&
						bear.GetComponent<SmallBear>().health > 0)
					{
						
						if (!_activatedBears.Contains(bear))
						{
							_activatedBears.Add(bear);
						}
					}
				}
				else // big bear
				{
					if (bear.GetComponent<Enemy>() != null &&
						!bear.GetComponent<Enemy>().phase.Equals(Enemy.AIPhase.NotInBattle) &&
						bear.GetComponent<Enemy>().health > 0)
					{
						if (!_activatedBears.Contains(bear))
						{
							_activatedBears.Add(bear);
						}
					}
				}
			}
		}
	}
}