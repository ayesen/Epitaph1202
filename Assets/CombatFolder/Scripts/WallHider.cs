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
		_floor = GameObject.FindGameObjectsWithTag("Floor").ToList();
		foreach (var wall in _walls)
		{
			if (wall.GetComponent<ShowScript>() == null)
			{
				print(wall.name);
			}
		}
	}

	private void Update()
	{
		switch (roomPlayerIsIn) // depends on which room player is in, hide walls and show masks
		{
			case Room.spawnHallway:
				foreach (var wall in _walls)
				{
					if (wall.GetComponent<ShowScript>()==null)
					{
						print(wall.name);
					}
					if (wall != null)
                    {
	                    if (wall.GetComponent<ShowScript>().whenSpawnHallway)
						{
							
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
							//wall.GetComponent<Renderer>().material.color.a = 0.5f;
						}
						else
						{
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
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
                foreach (var floor in _floor)
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
							print(wall.gameObject.name);
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
						}
						else
						{
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
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
				foreach (var floor in _floor)
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
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
						}
						else
						{
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
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
				foreach (var floor in _floor)
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
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
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
				foreach (var floor in _floor)
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
					if (wall.GetComponent<ShowScript>().whenBalcany)
					{
						wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
					}
					else
					{
						wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
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
				foreach (var floor in _floor)
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
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
						}
						else
						{
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
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
				foreach (var floor in _floor)
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
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
						}
						else
						{
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
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
				foreach (var floor in _floor)
				{
					if (floor.GetComponent<ShowScript>().whenBathRoom)
					{
						floor.GetComponent<MeshRenderer>().enabled = true;
					}
					else
					{
						floor.GetComponent<MeshRenderer>().enabled = false;
					}
				}
				break;
			case Room.bedroom:
				foreach (var wall in _walls)
				{
					if (wall != null)
                    {
						if (wall.GetComponent<ShowScript>().whenBedRoom)
						{
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
						}
						else
						{
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
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
				foreach (var floor in _floor)
				{
					if (floor.GetComponent<ShowScript>().whenBedRoom)
					{
						floor.GetComponent<MeshRenderer>().enabled = true;
					}
					else
					{
						floor.GetComponent<MeshRenderer>().enabled = false;
					}
				}
				break;
			case Room.hideout:
				foreach (var wall in _walls)
				{
					if (wall != null)
					{
						if (wall.GetComponent<ShowScript>().whemHideout)
						{
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
						}
						else
						{
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
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
				foreach (var floor in _floor)
				{
					if (floor.GetComponent<ShowScript>().whemHideout)
					{
						floor.GetComponent<MeshRenderer>().enabled = true;
					}
					else
					{
						floor.GetComponent<MeshRenderer>().enabled = false;
					}
				}
				break;
			case Room.studyRoom:
				foreach (var wall in _walls)
				{
					if (wall != null)
					{
						if (wall.GetComponent<ShowScript>().whenStudyRoom)
						{
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
						}
						else
						{
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
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
				foreach (var floor in _floor)
				{
					if (floor.GetComponent<ShowScript>().whenStudyRoom)
					{
						floor.GetComponent<MeshRenderer>().enabled = true;
					}
					else
					{
						floor.GetComponent<MeshRenderer>().enabled = false;
					}
				}
				break;
			case Room.studyBalcony:
				foreach (var wall in _walls)
				{
					if (wall != null)
					{
						if (wall.GetComponent<ShowScript>().whenStudyBalcony)
						{
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
						}
						else
						{
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
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
				foreach (var floor in _floor)
				{
					if (floor.GetComponent<ShowScript>().whenStudyBalcony)
					{
						floor.GetComponent<MeshRenderer>().enabled = true;
					}
					else
					{
						floor.GetComponent<MeshRenderer>().enabled = false;
					}
				}
				break;
			case Room.subHallway:
				foreach (var wall in _walls)
				{
					if (wall != null)
					{
						if (wall.GetComponent<ShowScript>().whenSubHallway)
						{
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
						}
						else
						{
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
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
				foreach (var floor in _floor)
				{
					if (floor.GetComponent<ShowScript>().whenSubHallway)
					{
						floor.GetComponent<MeshRenderer>().enabled = true;
					}
					else
					{
						floor.GetComponent<MeshRenderer>().enabled = false;
					}
				}
				break;
			case Room.subbedRoom:
				foreach (var wall in _walls)
				{
					if (wall != null)
					{
						if (wall.GetComponent<ShowScript>().whenSubBedRoom)
						{
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
						}
						else
						{
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
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
				foreach (var floor in _floor)
				{
					if (floor.GetComponent<ShowScript>().whenSubBedRoom)
					{
						floor.GetComponent<MeshRenderer>().enabled = true;
					}
					else
					{
						floor.GetComponent<MeshRenderer>().enabled = false;
					}
				}
				break;
			case Room.subBathroom:
				foreach (var wall in _walls)
				{
					if (wall != null)
					{
						if (wall.GetComponent<ShowScript>().whenSubBathRoom)
						{
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
						}
						else
						{
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
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
				foreach (var floor in _floor)
				{
					if (floor.GetComponent<ShowScript>().whenSubBathRoom)
					{
						floor.GetComponent<MeshRenderer>().enabled = true;
					}
					else
					{
						floor.GetComponent<MeshRenderer>().enabled = false;
					}
				}
				break;
			case Room.candleRoom:
				foreach (var wall in _walls)
				{
					if (wall != null)
					{
						if (wall.GetComponent<ShowScript>().whenCandleRoom)
						{
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
						}
						else
						{
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
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
				foreach (var floor in _floor)
				{
					if (floor.GetComponent<ShowScript>().whenCandleRoom)
					{
						floor.GetComponent<MeshRenderer>().enabled = true;
					}
					else
					{
						floor.GetComponent<MeshRenderer>().enabled = false;
					}
				}
				break;
			case Room.bossRoom:
				foreach (var wall in _walls)
				{
					if (wall != null)
					{
						if (wall.GetComponent<ShowScript>().whenBossRoom)
						{
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
						}
						else
						{
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
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
				foreach (var floor in _floor)
				{
					if (floor.GetComponent<ShowScript>().whenBossRoom)
					{
						floor.GetComponent<MeshRenderer>().enabled = true;
					}
					else
					{
						floor.GetComponent<MeshRenderer>().enabled = false;
					}
				}
				break;
				
				
		}
	}
}
