using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallExtender : MonoBehaviour
{
    public GameObject invisWall_prefab;

	private void Start()
	{
		if (invisWall_prefab != null)
		{
			GameObject invisWall = Instantiate(invisWall_prefab, transform);
			invisWall.transform.position = new Vector3(transform.position.x, 11, transform.position.z);
			invisWall.transform.rotation = transform.rotation;
			invisWall.transform.localScale = new Vector3(1, 50, 1);
		}
	}
}
