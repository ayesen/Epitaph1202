using UnityEngine;

public class SavePointManager : MonoBehaviour
{
	static public SavePointManager me;
	private GameObject _player;
	private PlayerScriptNew _ps;
	public SafeHouseTrigger last_checkPoint;

	private void Start()
	{
		_player = PlayerScriptNew.me.gameObject;
		_ps = PlayerScriptNew.me;
		me = this;
	}

	public void ResetPlayer()
	{
		if (last_checkPoint.rebornPos != null)
		{
			// set player position
			_player.transform.position = last_checkPoint.rebornPos.position;
			// reset player stats
			_ps.hp = _ps.maxHP;
			_ps.anim.Play("testIdle");
			// reset player mats
			_ps.matSlots[0].GetComponent<MatScriptNew>().amount = _ps.matSlots[0].GetComponent<MatScriptNew>().amount_max;
			_ps.matSlots[1].GetComponent<MatScriptNew>().amount = _ps.matSlots[0].GetComponent<MatScriptNew>().amount_max;
			_ps.matSlots[2].GetComponent<MatScriptNew>().amount = _ps.matSlots[0].GetComponent<MatScriptNew>().amount_max;
			_ps.matSlots[3] = null;
		}
		else
		{
			Debug.LogError("Assign reborn position to safe house!");
		}
	}
}
