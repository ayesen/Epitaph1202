using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WallHider : MonoBehaviour
{
	public static WallHider me;
	private List<GameObject> _masks;
	private List<GameObject> _walls;
	private List<GameObject> _lights;
	private List<GameObject> _floor;
	private List<GameObject> _enemies;

	public float doorOffset_x;
	public float doorOffset_z;

	public float windowOffset_x;

	public enum Room
	{
		spawnHallway,
		entryWay,
		livingRoom,
		kitchen,
		balcany,
		sublivingRoom,
		bathroom,
		bedroom,
		hideout,
		studyRoom,
		studyBalcony,
		subHallway,
		subbedRoom,
		subBathroom,
		candleRoom,
		bossRoom
	}
	public Room roomPlayerIsIn;

	private void Awake()
	{
		me = this;
	}

	private void Start()
	{
		_masks = GameObject.FindGameObjectsWithTag("Wall Hide Mask").ToList();
		_walls = GameObject.FindGameObjectsWithTag("Wall").ToList();
		_lights = GameObject.FindGameObjectsWithTag("Light").ToList();
		_enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
		//_floor = GameObject.FindGameObjectsWithTag("Floor").ToList();
	}

	private void Update()
	{
		switch (roomPlayerIsIn) // depends on which room player is in, hide walls and show masks
		{
			case Room.spawnHallway:
				foreach (var wall in _walls)
				{
					if (wall != null)
                    {
	                    if (wall.GetComponent<ShowScript>().whenSpawnHallway)
						{
							ShowWall(wall);
						}
						else
						{
							HideWall(wall);
						}
					}
				}
				foreach (var mask in _masks)
				{
					if (mask.GetComponent<ShowScript>().whenSpawnHallway)
					{
						mask.GetComponent<SpriteRenderer>().enabled = true;
					}
					else
					{
						mask.GetComponent<SpriteRenderer>().enabled = false;
					}
				}
				foreach (var light in _lights)
				{
					if (light.GetComponent<ShowScript>().whenSpawnHallway)
					{
						light.SetActive(true);
					}
					else
					{
						light.SetActive(false);
					}
				}
				foreach (var enemy in _enemies)
				{
					if (enemy && enemy.GetComponent<ShowScript>())
					{
						if (enemy.GetComponent<ShowScript>().whenSpawnHallway)
						{
							enemy.SetActive(true);
						}
						else
						{
							enemy.SetActive(false);
						}
					}
				}
				//foreach (var floor in _floor)
				{
					// if (floor.GetComponent<ShowScript>().whenSpawnHallway)
     //                {
					// 	floor.GetComponent<MeshRenderer>().enabled = true;
     //                }
     //                else
     //                {
					// 	floor.GetComponent<MeshRenderer>().enabled = false;
					// }
                }
				break;
			case Room.entryWay:
				foreach (var wall in _walls)
				{
					if (wall != null)
                    {
						if (wall.GetComponent<ShowScript>() != null && wall.GetComponent<ShowScript>().whenEntryWay)
						{
							ShowWall(wall);
						}
						else
						{
							HideWall(wall);
						}
					}
				}
				foreach (var mask in _masks)
				{
					if (mask.GetComponent<ShowScript>().whenEntryWay)
					{
						mask.GetComponent<SpriteRenderer>().enabled = true;
					}
					else
					{
						mask.GetComponent<SpriteRenderer>().enabled = false;
					}
				}
				foreach (var light in _lights)
				{
					if (light.GetComponent<ShowScript>().whenEntryWay)
					{
						light.SetActive(true);
					}
					else
					{
						light.SetActive(false);
					}
				}
				foreach (var enemy in _enemies)
				{
					if (enemy && enemy.GetComponent<ShowScript>())
					{
						if (enemy.GetComponent<ShowScript>().whenEntryWay)
						{
							enemy.SetActive(true);
						}
						else
						{
							enemy.SetActive(false);
						}
					}
				}
				//foreach (var floor in _floor)
				{
					// if (floor.GetComponent<ShowScript>().whenEntryWay)
					// {
					// 	floor.GetComponent<MeshRenderer>().enabled = true;
					// }
					// else
					// {
					// 	floor.GetComponent<MeshRenderer>().enabled = false;
					// }
				}
				break;
			case Room.livingRoom:
				foreach (var wall in _walls)
				{
					if (wall.GetComponent<ShowScript>()==null)
					{
						print(wall.name);
					}
					if (wall != null)
                    {
						if (wall.GetComponent<ShowScript>().whenLivingRoom)
						{
							ShowWall(wall);
						}
						else
						{
							HideWall(wall);
						}
					}
				}
				foreach (var mask in _masks)
				{
					if (mask.GetComponent<ShowScript>().whenLivingRoom)
					{
						mask.GetComponent<SpriteRenderer>().enabled = true;
					}
					else
					{
						mask.GetComponent<SpriteRenderer>().enabled = false;
					}
				}
				foreach (var light in _lights)
				{
					if (light.GetComponent<ShowScript>().whenLivingRoom)
					{
						light.SetActive(true);
					}
					else
					{
						light.SetActive(false);
					}
				}
				foreach (var enemy in _enemies)
				{
					if (enemy && enemy.GetComponent<ShowScript>())
					{
						if (enemy.GetComponent<ShowScript>().whenLivingRoom)
						{
							enemy.SetActive(true);
						}
						else
						{
							enemy.SetActive(false);
						}
					}
				}
				//foreach (var floor in _floor)
				{
					// if (floor.GetComponent<ShowScript>().whenLivingRoom)
					// {
					// 	floor.GetComponent<MeshRenderer>().enabled = true;
					// }
					// else
					// {
					// 	floor.GetComponent<MeshRenderer>().enabled = false;
					// }
				}
				break;
			case Room.kitchen:
				foreach (var wall in _walls)
				{
					if (wall != null)
                    {
						if (wall.GetComponent<ShowScript>().whenKitchen)
						{
							ShowWall(wall);
						}
						else
						{
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
						}
					}
				}
				foreach (var mask in _masks)
				{
					if (mask.GetComponent<ShowScript>().whenKitchen)
					{
						mask.GetComponent<SpriteRenderer>().enabled = true;
					}
					else
					{
						mask.GetComponent<SpriteRenderer>().enabled = false;
					}
				}
				foreach (var light in _lights)
				{
					if (light.GetComponent<ShowScript>().whenKitchen)
					{
						light.SetActive(true);
					}
					else
					{
						light.SetActive(false);
					}
				}
				foreach (var enemy in _enemies)
				{
					if (enemy && enemy.GetComponent<ShowScript>())
					{
						if (enemy.GetComponent<ShowScript>().whenKitchen)
						{
							enemy.SetActive(true);
						}
						else
						{
							enemy.SetActive(false);
						}
					}
				}
				//foreach (var floor in _floor)
				{
					// if (floor.GetComponent<ShowScript>().whenKitchen)
					// {
					// 	floor.GetComponent<MeshRenderer>().enabled = true;
					// }
					// else
					// {
					// 	floor.GetComponent<MeshRenderer>().enabled = false;
					// }
				}
				break;
			case Room.balcany:
				foreach (var wall in _walls)
				{
					if (wall != null)
					{
						if (wall.GetComponent<ShowScript>().whenBalcany)
						{
							ShowWall(wall);
						}
						else
						{
							HideWall(wall);
						}
					}
				}
				foreach (var mask in _masks)
				{
					if (mask.GetComponent<ShowScript>().whenBalcany)
					{
						mask.GetComponent<SpriteRenderer>().enabled = true;
					}
					else
					{
						mask.GetComponent<SpriteRenderer>().enabled = false;
					}
				}
				foreach (var light in _lights)
				{
					if (light.GetComponent<ShowScript>().whenBalcany)
					{
						light.SetActive(true);
					}
					else
					{
						light.SetActive(false);
					}
				}
				foreach (var enemy in _enemies)
				{
					if (enemy && enemy.GetComponent<ShowScript>())
					{
						if (enemy.GetComponent<ShowScript>().whenBalcany)
						{
							enemy.SetActive(true);
						}
						else
						{
							enemy.SetActive(false);
						}
					}
				}
				//foreach (var floor in _floor)
				{
					// if (floor.GetComponent<ShowScript>().whenBalcany)
					// {
					// 	floor.GetComponent<MeshRenderer>().enabled = true;
					// }
					// else
					// {
					// 	floor.GetComponent<MeshRenderer>().enabled = false;
					// }
				}
				break;
			case Room.sublivingRoom:
				foreach (var wall in _walls)
				{
					if (wall != null)
                    {
						if (wall.GetComponent<ShowScript>().whenSubLivingRoom)
						{
							ShowWall(wall);
						}
						else
						{
							HideWall(wall);
						}
					}
				}
				foreach (var mask in _masks)
				{
					if (mask.GetComponent<ShowScript>().whenSubLivingRoom)
					{
						mask.GetComponent<SpriteRenderer>().enabled = true;
					}
					else
					{
						mask.GetComponent<SpriteRenderer>().enabled = false;
					}
				}
				foreach (var light in _lights)
				{
					if (light.GetComponent<ShowScript>().whenSubLivingRoom)
					{
						light.SetActive(true);
					}
					else
					{
						light.SetActive(false);
					}
				}
				foreach (var enemy in _enemies)
				{
					if (enemy && enemy.GetComponent<ShowScript>())
					{
						if (enemy.GetComponent<ShowScript>().whenSubLivingRoom)
						{
							enemy.SetActive(true);
						}
						else
						{
							enemy.SetActive(false);
						}
					}
				}
				//foreach (var floor in _floor)
				{
					// if (floor.GetComponent<ShowScript>().whenSubLivingRoom)
					// {
					// 	floor.GetComponent<MeshRenderer>().enabled = true;
					// }
					// else
					// {
					// 	floor.GetComponent<MeshRenderer>().enabled = false;
					// }
				}
				break;
			case Room.bathroom:
				foreach (var wall in _walls)
				{
					if (wall != null)
                    {
						if (wall.GetComponent<ShowScript>().whenBathRoom)
						{
							ShowWall(wall);
						}
						else
						{
							HideWall(wall);
						}
					}
				}
				foreach (var mask in _masks)
				{
					if (mask.GetComponent<ShowScript>().whenBathRoom)
					{
						mask.GetComponent<SpriteRenderer>().enabled = true;
					}
					else
					{
						mask.GetComponent<SpriteRenderer>().enabled = false;
					}
				}
				foreach (var light in _lights)
				{
					if (light.GetComponent<ShowScript>().whenBathRoom)
					{
						light.SetActive(true);
					}
					else
					{
						light.SetActive(false);
					}
				}
				foreach (var enemy in _enemies)
				{
					if (enemy && enemy.GetComponent<ShowScript>())
					{
						if (enemy.GetComponent<ShowScript>().whenBathRoom)
						{
							enemy.SetActive(true);
						}
						else
						{
							enemy.SetActive(false);
						}
					}
				}
				//foreach (var floor in _floor)
				//{
				//	if (floor.GetComponent<ShowScript>().whenBathRoom)
				//	{
				//		floor.GetComponent<MeshRenderer>().enabled = true;
				//	}
				//	else
				//	{
				//		floor.GetComponent<MeshRenderer>().enabled = false;
				//	}
				//}
				break;
			case Room.bedroom:
				foreach (var wall in _walls)
				{
					if (wall != null)
                    {
						if (wall.GetComponent<ShowScript>().whenBedRoom)
						{
							ShowWall(wall);
						}
						else
						{
							HideWall(wall);
						}
					}
				}
				foreach (var mask in _masks)
				{
					if (mask.GetComponent<ShowScript>().whenBedRoom)
					{
						mask.GetComponent<SpriteRenderer>().enabled = true;
					}
					else
					{
						mask.GetComponent<SpriteRenderer>().enabled = false;
					}
				}
				foreach (var light in _lights)
				{
					if (light.GetComponent<ShowScript>().whenBedRoom)
					{
						light.SetActive(true);
					}
					else
					{
						light.SetActive(false);
					}
				}
				foreach (var enemy in _enemies)
				{
					if (enemy && enemy.GetComponent<ShowScript>())
					{
						if (enemy.GetComponent<ShowScript>().whenBedRoom)
						{
							enemy.SetActive(true);
						}
						else
						{
							enemy.SetActive(false);
						}
					}
				}
				//foreach (var floor in _floor)
				//{
				//	if (floor.GetComponent<ShowScript>().whenBedRoom)
				//	{
				//		floor.GetComponent<MeshRenderer>().enabled = true;
				//	}
				//	else
				//	{
				//		floor.GetComponent<MeshRenderer>().enabled = false;
				//	}
				//}
				break;
			case Room.hideout:
				foreach (var wall in _walls)
				{
					if (wall != null)
					{
						if (wall.GetComponent<ShowScript>().whemHideout)
						{
							ShowWall(wall);
						}
						else
						{
							HideWall(wall);
						}
					}
				}
				foreach (var mask in _masks)
				{
					if (mask.GetComponent<ShowScript>().whemHideout)
					{
						mask.GetComponent<SpriteRenderer>().enabled = true;
					}
					else
					{
						mask.GetComponent<SpriteRenderer>().enabled = false;
					}
				}
				foreach (var light in _lights)
				{
					if (light.GetComponent<ShowScript>().whemHideout)
					{
						light.SetActive(true);
					}
					else
					{
						light.SetActive(false);
					}
				}
				foreach (var enemy in _enemies)
				{
					if (enemy && enemy.GetComponent<ShowScript>())
                    {
						if (enemy.GetComponent<ShowScript>().whemHideout)
						{
							enemy.SetActive(true);
						}
						else
						{
							enemy.SetActive(false);
						}
					}
				}
				//foreach (var floor in _floor)
				//{
				//	if (floor.GetComponent<ShowScript>().whemHideout)
				//	{
				//		floor.GetComponent<MeshRenderer>().enabled = true;
				//	}
				//	else
				//	{
				//		floor.GetComponent<MeshRenderer>().enabled = false;
				//	}
				//}
				break;
			case Room.studyRoom:
				foreach (var wall in _walls)
				{
					if (wall != null)
					{
						if (wall.GetComponent<ShowScript>().whenStudyRoom)
						{
							ShowWall(wall);
						}
						else
						{
							HideWall(wall);
						}
					}
				}
				foreach (var mask in _masks)
				{
					if (mask.GetComponent<ShowScript>().whenStudyRoom)
					{
						mask.GetComponent<SpriteRenderer>().enabled = true;
					}
					else
					{
						mask.GetComponent<SpriteRenderer>().enabled = false;
					}
				}
				foreach (var light in _lights)
				{
					if (light.GetComponent<ShowScript>().whenStudyRoom)
					{
						light.SetActive(true);
					}
					else
					{
						light.SetActive(false);
					}
				}
				foreach (var enemy in _enemies)
				{
					if (enemy && enemy.GetComponent<ShowScript>())
					{
						if (enemy.GetComponent<ShowScript>().whenStudyRoom)
						{
							enemy.SetActive(true);
						}
						else
						{
							enemy.SetActive(false);
						}
					}
				}
				//foreach (var floor in _floor)
				//{
				//	if (floor.GetComponent<ShowScript>().whenStudyRoom)
				//	{
				//		floor.GetComponent<MeshRenderer>().enabled = true;
				//	}
				//	else
				//	{
				//		floor.GetComponent<MeshRenderer>().enabled = false;
				//	}
				//}
				break;
			case Room.studyBalcony:
				foreach (var wall in _walls)
				{
					if (wall != null)
					{
						if (wall.GetComponent<ShowScript>().whenStudyBalcony)
						{
							ShowWall(wall);
						}
						else
						{
							HideWall(wall);
						}
					}
				}
				foreach (var mask in _masks)
				{
					if (mask.GetComponent<ShowScript>().whenStudyBalcony)
					{
						mask.GetComponent<SpriteRenderer>().enabled = true;
					}
					else
					{
						mask.GetComponent<SpriteRenderer>().enabled = false;
					}
				}
				foreach (var light in _lights)
				{
					if (light.GetComponent<ShowScript>().whenStudyBalcony)
					{
						light.SetActive(true);
					}
					else
					{
						light.SetActive(false);
					}
				}
				foreach (var enemy in _enemies)
				{
					if (enemy && enemy.GetComponent<ShowScript>())
					{
						if (enemy.GetComponent<ShowScript>().whenStudyBalcony)
						{
							enemy.SetActive(true);
						}
						else
						{
							enemy.SetActive(false);
						}
					}
				}
				//foreach (var floor in _floor)
				//{
				//	if (floor.GetComponent<ShowScript>().whenStudyBalcony)
				//	{
				//		floor.GetComponent<MeshRenderer>().enabled = true;
				//	}
				//	else
				//	{
				//		floor.GetComponent<MeshRenderer>().enabled = false;
				//	}
				//}
				break;
			case Room.subHallway:
				foreach (var wall in _walls)
				{
					if (wall != null)
					{
						if (wall.GetComponent<ShowScript>().whenSubHallway)
						{
							ShowWall(wall);
						}
						else
						{
							HideWall(wall);
						}
					}
				}
				foreach (var mask in _masks)
				{
					if (mask.GetComponent<ShowScript>().whenSubHallway)
					{
						mask.GetComponent<SpriteRenderer>().enabled = true;
					}
					else
					{
						mask.GetComponent<SpriteRenderer>().enabled = false;
					}
				}
				foreach (var light in _lights)
				{
					if (light.GetComponent<ShowScript>().whenSubHallway)
					{
						light.SetActive(true);
					}
					else
					{
						light.SetActive(false);
					}
				}
				foreach (var enemy in _enemies)
				{
					if (enemy && enemy.GetComponent<ShowScript>())
					{
						if (enemy.GetComponent<ShowScript>().whenSubHallway)
						{
							enemy.SetActive(true);
						}
						else
						{
							enemy.SetActive(false);
						}
					}
				}
				//foreach (var floor in _floor)
				//{
				//	if (floor.GetComponent<ShowScript>().whenSubHallway)
				//	{
				//		floor.GetComponent<MeshRenderer>().enabled = true;
				//	}
				//	else
				//	{
				//		floor.GetComponent<MeshRenderer>().enabled = false;
				//	}
				//}
				break;
			case Room.subbedRoom:
				foreach (var wall in _walls)
				{
					if (wall != null)
					{
						if (wall.GetComponent<ShowScript>().whenSubBedRoom)
						{
							ShowWall(wall);
						}
						else
						{
							HideWall(wall);
						}
					}
				}
				foreach (var mask in _masks)
				{
					if (mask.GetComponent<ShowScript>().whenSubBedRoom)
					{
						mask.GetComponent<SpriteRenderer>().enabled = true;
					}
					else
					{
						mask.GetComponent<SpriteRenderer>().enabled = false;
					}
				}
				foreach (var light in _lights)
				{
					if (light.GetComponent<ShowScript>().whenSubBedRoom)
					{
						light.SetActive(true);
					}
					else
					{
						light.SetActive(false);
					}
				}
				foreach (var enemy in _enemies)
				{
					if (enemy && enemy.GetComponent<ShowScript>())
					{
						if (enemy.GetComponent<ShowScript>().whenSubBedRoom)
						{
							enemy.SetActive(true);
						}
						else
						{
							enemy.SetActive(false);
						}
					}
				}
				//foreach (var floor in _floor)
				//{
				//	if (floor.GetComponent<ShowScript>().whenSubBedRoom)
				//	{
				//		floor.GetComponent<MeshRenderer>().enabled = true;
				//	}
				//	else
				//	{
				//		floor.GetComponent<MeshRenderer>().enabled = false;
				//	}
				//}
				break;
			case Room.subBathroom:
				foreach (var wall in _walls)
				{
					if (wall != null)
					{
						if (wall.GetComponent<ShowScript>().whenSubBathRoom)
						{
							ShowWall(wall);
						}
						else
						{
							HideWall(wall);
						}
					}
				}
				foreach (var mask in _masks)
				{
					if (mask.GetComponent<ShowScript>().whenSubBathRoom)
					{
						mask.GetComponent<SpriteRenderer>().enabled = true;
					}
					else
					{
						mask.GetComponent<SpriteRenderer>().enabled = false;
					}
				}
				foreach (var light in _lights)
				{
					if (light.GetComponent<ShowScript>().whenSubBathRoom)
					{
						light.SetActive(true);
					}
					else
					{
						light.SetActive(false);
					}
				}
				foreach (var enemy in _enemies)
				{
					if (enemy && enemy.GetComponent<ShowScript>())
					{
						if (enemy.GetComponent<ShowScript>().whenSubBathRoom)
						{
							enemy.SetActive(true);
						}
						else
						{
							enemy.SetActive(false);
						}
					}
				}
				//foreach (var floor in _floor)
				//{
				//	if (floor.GetComponent<ShowScript>().whenSubBathRoom)
				//	{
				//		floor.GetComponent<MeshRenderer>().enabled = true;
				//	}
				//	else
				//	{
				//		floor.GetComponent<MeshRenderer>().enabled = false;
				//	}
				//}
				break;
			case Room.candleRoom:
				foreach (var wall in _walls)
				{
					if (wall != null)
					{
						if (wall.GetComponent<ShowScript>().whenCandleRoom)
						{
							ShowWall(wall);
						}
						else
						{
							HideWall(wall);
						}
					}
				}
				foreach (var mask in _masks)
				{
					if (mask.GetComponent<ShowScript>().whenCandleRoom)
					{
						mask.GetComponent<SpriteRenderer>().enabled = true;
					}
					else
					{
						mask.GetComponent<SpriteRenderer>().enabled = false;
					}
				}
				foreach (var light in _lights)
				{
					if (light.GetComponent<ShowScript>().whenCandleRoom)
					{
						light.SetActive(true);
					}
					else
					{
						light.SetActive(false);
					}
				}
				foreach (var enemy in _enemies)
				{
					if (enemy && enemy.GetComponent<ShowScript>())
					{
						if (enemy.GetComponent<ShowScript>().whenCandleRoom)
						{
							enemy.SetActive(true);
						}
						else
						{
							enemy.SetActive(false);
						}
					}
				}
				//foreach (var floor in _floor)
				//{
				//	if (floor.GetComponent<ShowScript>().whenCandleRoom)
				//	{
				//		floor.GetComponent<MeshRenderer>().enabled = true;
				//	}
				//	else
				//	{
				//		floor.GetComponent<MeshRenderer>().enabled = false;
				//	}
				//}
				break;
			case Room.bossRoom:
				foreach (var wall in _walls)
				{
					if (wall != null)
					{
						if (wall.GetComponent<ShowScript>().whenBossRoom)
						{
							ShowWall(wall);
						}
						else
						{
							HideWall(wall);
						}
					}
				}
				foreach (var mask in _masks)
				{
					if (mask.GetComponent<ShowScript>().whenBossRoom)
					{
						mask.GetComponent<SpriteRenderer>().enabled = true;
					}
					else
					{
						mask.GetComponent<SpriteRenderer>().enabled = false;
					}
				}
				foreach (var light in _lights)
				{
					if (light.GetComponent<ShowScript>().whenBossRoom)
					{
						light.SetActive(true);
					}
					else
					{
						light.SetActive(false);
					}
				}
				foreach (var enemy in _enemies)
				{
					if (enemy && enemy.GetComponent<ShowScript>())
					{
						if (enemy.GetComponent<ShowScript>().whenBossRoom)
						{
							enemy.SetActive(true);
						}
						else
						{
							enemy.SetActive(false);
						}
					}
				}
				//foreach (var floor in _floor)
				//{
				//	if (floor.GetComponent<ShowScript>().whenBossRoom)
				//	{
				//		floor.GetComponent<MeshRenderer>().enabled = true;
				//	}
				//	else
				//	{
				//		floor.GetComponent<MeshRenderer>().enabled = false;
				//	}
				//}
				break;
		}
	}

	private void ShowWall(GameObject wall)
	{
		wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
		if (wall.transform.childCount > 0)
		{
			Transform childTransform = wall.transform.GetChild(0).transform;
			if (wall.layer == 12) // wall
			{
				childTransform.localScale = new Vector3(childTransform.localScale.x, childTransform.localScale.y, 1f);
			}
			else if (wall.layer == 20) // door
			{
				childTransform.localScale = new Vector3(childTransform.localScale.x, childTransform.localScale.y, 2f);
				childTransform.localPosition = new Vector3(doorOffset_x, childTransform.localPosition.y, doorOffset_z);
			}
			else if (wall.layer == 19) // window
			{
				childTransform.localScale = new Vector3(childTransform.localScale.x, childTransform.localScale.y, 2f);
				childTransform.localPosition = new Vector3(windowOffset_x, childTransform.localPosition.y, childTransform.localPosition.z);
			}
			wall.transform.GetChild(0).GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
		}
	}

	private void HideWall(GameObject wall)
	{
		wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
		if (wall.transform.childCount > 0)
		{
			Transform childTransform = wall.transform.GetChild(0).transform;
			wall.transform.GetChild(0).GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
			if (wall.layer == 12) // wall
			{
				childTransform.localScale = new Vector3(childTransform.localScale.x, childTransform.localScale.y, 1f);
			}
			else if (wall.layer == 20) // door
			{
				childTransform.localScale = new Vector3(childTransform.localScale.x, childTransform.localScale.y, 2f);
				childTransform.localPosition = new Vector3(doorOffset_x, childTransform.localPosition.y, doorOffset_z);
			}
			else if (wall.layer == 19) // window
			{
				childTransform.localScale = new Vector3(childTransform.localScale.x, childTransform.localScale.y, 2f);
				childTransform.localPosition = new Vector3(windowOffset_x, childTransform.localPosition.y, childTransform.localPosition.z);
			}
		}
	}
}
