using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTargetSetter : MonoBehaviour
{
    public List<Transform> pos;

	private void Update()
	{
		if (pos.Count == 2)
		{
			//transform.position = pos[GameManager.me.stateOfLevel].position;
		}
		else
		{
			Debug.LogError("there should be two pos, for two ai break-ins");
		}
	}
}
