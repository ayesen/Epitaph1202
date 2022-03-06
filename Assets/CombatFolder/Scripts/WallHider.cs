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
						if (wall.GetComponent<HideScript>().whenSpawnHallway)
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
					if (mask.GetComponent<HideScript>().whenSpawnHallway)
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
					if (light.GetComponent<HideScript>().whenSpawnHallway)
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
					if (floor.GetComponent<HideScript>().whenSpawnHallway)
                    {
						floor.GetComponent<MeshRenderer>().enabled = true;
                    }
                    else
                    {
						floor.GetComponent<MeshRenderer>().enabled = false;
					}
                }
				break;
			case Room.entryWay:
				foreach (var wall in _walls)
				{
					if (wall != null)
                    {
						if (wall.GetComponent<HideScript>().whenEntryWay)
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
					if (mask.GetComponent<HideScript>().whenEntryWay)
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
					if (light.GetComponent<HideScript>().whenEntryWay)
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
					if (floor.GetComponent<HideScript>().whenEntryWay)
					{
						floor.GetComponent<MeshRenderer>().enabled = true;
					}
					else
					{
						floor.GetComponent<MeshRenderer>().enabled = false;
					}
				}
				break;
			case Room.livingRoom:
				foreach (var wall in _walls)
				{
					if (wall != null)
                    {
						if (wall.GetComponent<HideScript>().whenLivingRoom)
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
					if (mask.GetComponent<HideScript>().whenLivingRoom)
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
					if (light.GetComponent<HideScript>().whenLivingRoom)
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
					if (floor.GetComponent<HideScript>().whenLivingRoom)
					{
						floor.GetComponent<MeshRenderer>().enabled = true;
					}
					else
					{
						floor.GetComponent<MeshRenderer>().enabled = false;
					}
				}
				break;
			case Room.kitchen:
				foreach (var wall in _walls)
				{
					if (wall != null)
                    {
						if (wall.GetComponent<HideScript>().whenKitchen)
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
					if (mask.GetComponent<HideScript>().whenKitchen)
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
					if (light.GetComponent<HideScript>().whenKitchen)
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
					if (floor.GetComponent<HideScript>().whenKitchen)
					{
						floor.GetComponent<MeshRenderer>().enabled = true;
					}
					else
					{
						floor.GetComponent<MeshRenderer>().enabled = false;
					}
				}
				break;
			case Room.balcany:
				foreach (var wall in _walls)
				{
					if (wall.GetComponent<HideScript>().whenBalcany)
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
					if (mask.GetComponent<HideScript>().whenBalcany)
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
					if (light.GetComponent<HideScript>().whenBalcany)
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
					if (floor.GetComponent<HideScript>().whenBalcany)
					{
						floor.GetComponent<MeshRenderer>().enabled = true;
					}
					else
					{
						floor.GetComponent<MeshRenderer>().enabled = false;
					}
				}
				break;
			case Room.sublivingRoom:
				foreach (var wall in _walls)
				{
					if (wall != null)
                    {
						if (wall.GetComponent<HideScript>().whenSubLivingRoom)
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
					if (mask.GetComponent<HideScript>().whenSubLivingRoom)
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
					if (light.GetComponent<HideScript>().whenSubLivingRoom)
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
					if (floor.GetComponent<HideScript>().whenSubLivingRoom)
					{
						floor.GetComponent<MeshRenderer>().enabled = true;
					}
					else
					{
						floor.GetComponent<MeshRenderer>().enabled = false;
					}
				}
				break;
			case Room.bathroom:
				foreach (var wall in _walls)
				{
					if (wall != null)
                    {
						if (wall.GetComponent<HideScript>().whenBathRoom)
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
					if (mask.GetComponent<HideScript>().whenBathRoom)
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
					if (light.GetComponent<HideScript>().whenBathRoom)
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
					if (floor.GetComponent<HideScript>().whenBathRoom)
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
						if (wall.GetComponent<HideScript>().whenBedRoom)
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
					if (mask.GetComponent<HideScript>().whenBedRoom)
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
					if (light.GetComponent<HideScript>().whenBedRoom)
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
					if (floor.GetComponent<HideScript>().whenBedRoom)
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
						if (wall.GetComponent<HideScript>().whemHideout)
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
					if (mask.GetComponent<HideScript>().whemHideout)
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
					if (light.GetComponent<HideScript>().whemHideout)
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
					if (floor.GetComponent<HideScript>().whemHideout)
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
						if (wall.GetComponent<HideScript>().whenStudyRoom)
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
					if (mask.GetComponent<HideScript>().whenStudyRoom)
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
					if (light.GetComponent<HideScript>().whenStudyRoom)
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
					if (floor.GetComponent<HideScript>().whenStudyRoom)
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
						if (wall.GetComponent<HideScript>().whenStudyBalcony)
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
					if (mask.GetComponent<HideScript>().whenStudyBalcony)
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
					if (light.GetComponent<HideScript>().whenStudyBalcony)
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
					if (floor.GetComponent<HideScript>().whenStudyBalcony)
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
						if (wall.GetComponent<HideScript>().whenSubHallway)
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
					if (mask.GetComponent<HideScript>().whenSubHallway)
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
					if (light.GetComponent<HideScript>().whenSubHallway)
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
					if (floor.GetComponent<HideScript>().whenSubHallway)
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
						if (wall.GetComponent<HideScript>().whenSubBedRoom)
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
					if (mask.GetComponent<HideScript>().whenSubBedRoom)
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
					if (light.GetComponent<HideScript>().whenSubBedRoom)
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
					if (floor.GetComponent<HideScript>().whenSubBedRoom)
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
						if (wall.GetComponent<HideScript>().whenSubBathRoom)
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
					if (mask.GetComponent<HideScript>().whenSubBathRoom)
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
					if (light.GetComponent<HideScript>().whenSubBathRoom)
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
					if (floor.GetComponent<HideScript>().whenSubBathRoom)
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
						if (wall.GetComponent<HideScript>().whenCandleRoom)
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
					if (mask.GetComponent<HideScript>().whenCandleRoom)
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
					if (light.GetComponent<HideScript>().whenCandleRoom)
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
					if (floor.GetComponent<HideScript>().whenCandleRoom)
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
						if (wall.GetComponent<HideScript>().whenBossRoom)
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
					if (mask.GetComponent<HideScript>().whenBossRoom)
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
					if (light.GetComponent<HideScript>().whenBossRoom)
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
					if (floor.GetComponent<HideScript>().whenBossRoom)
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
