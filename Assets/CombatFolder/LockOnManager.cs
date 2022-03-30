using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LockOnManager : MonoBehaviour
{
	static public LockOnManager me;
    private List<GameObject> bears;
	public List<GameObject> bears_canBeLockedOn;
	public float max_lockon_dis;

	private void Awake()
	{
		me = this;
	}

	private void Start()
	{
		bears = GameObject.FindGameObjectsWithTag("Enemy").ToList();
	}

	private void Update()
	{
		// get bears that are activated
		foreach (var bear in bears.ToList())
		{
			if (bear != null)
			{
				if (bear.GetComponent<SmallBear>()) // small bear
				{
					if (bear.GetComponent<AIController>().enabled && 
						!bears_canBeLockedOn.Contains(bear) &&
						bear.GetComponent<SmallBear>().health > 0)
					{
						bears_canBeLockedOn.Add(bear);
					}
				}
				else // big bear
				{
					if (bear.GetComponent<Enemy>() != null && 
						!bear.GetComponent<Enemy>().phase.Equals(Enemy.AIPhase.NotInBattle) &&
						bear.GetComponent<Enemy>().health > 0)
					{
						if (!bears_canBeLockedOn.Contains(bear))
						{
							bears_canBeLockedOn.Add(bear);
						}
					}
				}
			}
			else
			{
				bears.Remove(bear);
			}
		}

		// remove bears outside of boundary
		foreach (var bear in bears_canBeLockedOn.ToList())
		{
			if (bear!=null && Vector3.Distance(transform.position, bear.transform.position) > max_lockon_dis)
			{
				bears_canBeLockedOn.Remove(bear);
			}
		}
	}

	public GameObject GetClosest_right()
	{
		float smallest_angle = float.MaxValue;
		GameObject closest_right_bear = bears_canBeLockedOn[0];
		foreach (var bear in bears_canBeLockedOn)
		{
			Vector3 bearDir = bear.transform.position - transform.position;
			float angle = Vector3.SignedAngle(bearDir, PlayerScriptNew.me.transform.forward, Vector3.up);
			if (angle < smallest_angle && angle > 0)
			{
				smallest_angle = angle;
				closest_right_bear = bear;
			}
		}
		return closest_right_bear;
	}

	public GameObject GetClosest_left()
	{
		float smallest_angle = float.MinValue;
		GameObject closest_left_bear = bears_canBeLockedOn[0];
		foreach (var bear in bears_canBeLockedOn)
		{
			Vector3 bearDir = bear.transform.position - transform.position;
			float angle = Vector3.SignedAngle(bearDir, PlayerScriptNew.me.transform.forward, Vector3.up);
			if (angle > smallest_angle && angle < 0)
			{
				smallest_angle = angle;
				closest_left_bear = bear;
			}
		}
		return closest_left_bear;
	}
}