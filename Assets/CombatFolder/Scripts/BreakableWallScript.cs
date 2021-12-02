using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWallScript : MonoBehaviour
{
    public GameObject wallbreak_vfx;
	public GameObject enemy;
	public float breakRange;

	private void Update()
	{
		if (Vector3.Distance(enemy.transform.position, transform.position) < breakRange)
		{
			Instantiate(wallbreak_vfx, transform.position, enemy.transform.rotation * Quaternion.AngleAxis(180, Vector3.up));
			Destroy(gameObject);
		}
	}
}
