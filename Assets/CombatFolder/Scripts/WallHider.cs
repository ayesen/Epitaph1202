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
					if (floor.GetComponent<ShowScript>().whenLivingRoom)
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
						if (wall.GetComponent<ShowScript>().whenCorridor)
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
					if (mask.GetComponent<ShowScript>().whenCorridor)
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
					if (light.GetComponent<ShowScript>().whenCorridor)
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
					if (floor.GetComponent<ShowScript>().whenCorridor)
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
						if (wall.GetComponent<ShowScript>().whenRestroom)
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
					if (mask.GetComponent<ShowScript>().whenRestroom)
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
					if (light.GetComponent<ShowScript>().whenRestroom)
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
					if (floor.GetComponent<ShowScript>().whenRestroom)
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
						if (wall.GetComponent<ShowScript>().whenSmallCorridor)
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
					if (mask.GetComponent<ShowScript>().whenSmallCorridor)
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
					if (light.GetComponent<ShowScript>().whenSmallCorridor)
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
					if (floor.GetComponent<ShowScript>().whenSmallCorridor)
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
					if (wall.GetComponent<ShowScript>().whenStorage)
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
					if (mask.GetComponent<ShowScript>().whenStorage)
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
					if (light.GetComponent<ShowScript>().whenStorage)
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
					if (floor.GetComponent<ShowScript>().whenStorage)
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
						if (wall.GetComponent<ShowScript>().whenMasterRoom)
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
					if (mask.GetComponent<ShowScript>().whenMasterRoom)
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
					if (light.GetComponent<ShowScript>().whenMasterRoom)
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
					if (floor.GetComponent<ShowScript>().whenMasterRoom)
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
						if (wall.GetComponent<ShowScript>().whenBalcony)
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
					if (mask.GetComponent<ShowScript>().whenBalcony)
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
					if (light.GetComponent<ShowScript>().whenBalcony)
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
					if (floor.GetComponent<ShowScript>().whenBalcony)
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
						if (wall.GetComponent<ShowScript>().whenDaughtorRoom)
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
					if (mask.GetComponent<ShowScript>().whenDaughtorRoom)
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
					if (light.GetComponent<ShowScript>().whenDaughtorRoom)
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
					if (floor.GetComponent<ShowScript>().whenDaughtorRoom)
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
