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
		livingRoom,
		corridor,
		restRoom,
		smallCorridor,
		storage,
		masterRoom,
		balcony,
		DaughtorRoom
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
			case Room.corridor:
				foreach (var wall in _walls)
				{
					if (wall != null)
                    {
						if (wall.GetComponent<HideScript>().whenCorridor)
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
					if (mask.GetComponent<HideScript>().whenCorridor)
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
					if (light.GetComponent<HideScript>().whenCorridor)
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
					if (floor.GetComponent<HideScript>().whenCorridor)
					{
						floor.GetComponent<MeshRenderer>().enabled = true;
					}
					else
					{
						floor.GetComponent<MeshRenderer>().enabled = false;
					}
				}
				break;
			case Room.restRoom:
				foreach (var wall in _walls)
				{
					if (wall != null)
                    {
						if (wall.GetComponent<HideScript>().whenRestroom)
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
					if (mask.GetComponent<HideScript>().whenRestroom)
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
					if (light.GetComponent<HideScript>().whenRestroom)
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
					if (floor.GetComponent<HideScript>().whenRestroom)
					{
						floor.GetComponent<MeshRenderer>().enabled = true;
					}
					else
					{
						floor.GetComponent<MeshRenderer>().enabled = false;
					}
				}
				break;
			case Room.smallCorridor:
				foreach (var wall in _walls)
				{
					if (wall != null)
                    {
						if (wall.GetComponent<HideScript>().whenSmallCorridor)
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
					if (mask.GetComponent<HideScript>().whenSmallCorridor)
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
					if (light.GetComponent<HideScript>().whenSmallCorridor)
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
					if (floor.GetComponent<HideScript>().whenSmallCorridor)
					{
						floor.GetComponent<MeshRenderer>().enabled = true;
					}
					else
					{
						floor.GetComponent<MeshRenderer>().enabled = false;
					}
				}
				break;
			case Room.storage:
				foreach (var wall in _walls)
				{
					if (wall.GetComponent<HideScript>().whenStorage)
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
					if (mask.GetComponent<HideScript>().whenStorage)
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
					if (light.GetComponent<HideScript>().whenStorage)
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
					if (floor.GetComponent<HideScript>().whenStorage)
					{
						floor.GetComponent<MeshRenderer>().enabled = true;
					}
					else
					{
						floor.GetComponent<MeshRenderer>().enabled = false;
					}
				}
				break;
			case Room.masterRoom:
				foreach (var wall in _walls)
				{
					if (wall != null)
                    {
						if (wall.GetComponent<HideScript>().whenMasterRoom)
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
					if (mask.GetComponent<HideScript>().whenMasterRoom)
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
					if (light.GetComponent<HideScript>().whenMasterRoom)
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
					if (floor.GetComponent<HideScript>().whenMasterRoom)
					{
						floor.GetComponent<MeshRenderer>().enabled = true;
					}
					else
					{
						floor.GetComponent<MeshRenderer>().enabled = false;
					}
				}
				break;
			case Room.balcony:
				foreach (var wall in _walls)
				{
					if (wall != null)
                    {
						if (wall.GetComponent<HideScript>().whenBalcony)
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
					if (mask.GetComponent<HideScript>().whenBalcony)
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
					if (light.GetComponent<HideScript>().whenBalcony)
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
					if (floor.GetComponent<HideScript>().whenBalcony)
					{
						floor.GetComponent<MeshRenderer>().enabled = true;
					}
					else
					{
						floor.GetComponent<MeshRenderer>().enabled = false;
					}
				}
				break;
			case Room.DaughtorRoom:
				foreach (var wall in _walls)
				{
					if (wall != null)
                    {
						if (wall.GetComponent<HideScript>().whenDaughtorRoom)
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
					if (mask.GetComponent<HideScript>().whenDaughtorRoom)
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
					if (light.GetComponent<HideScript>().whenDaughtorRoom)
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
					if (floor.GetComponent<HideScript>().whenDaughtorRoom)
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
