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
						if (wall.GetComponent<WallScript>().whenLivingRoom)
						{
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
						}
						else
						{
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
						}
					}
				}
				foreach (var mask in _masks)
				{
					if (mask.GetComponent<WallScript>().whenLivingRoom)
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
					if (light.GetComponent<WallScript>().whenLivingRoom)
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
					if (floor.GetComponent<WallScript>().whenLivingRoom)
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
						if (wall.GetComponent<WallScript>().whenCorridor)
						{
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
						}
						else
						{
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
						}
					}
				}
				foreach (var mask in _masks)
				{
					if (mask.GetComponent<WallScript>().whenCorridor)
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
					if (light.GetComponent<WallScript>().whenCorridor)
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
					if (floor.GetComponent<WallScript>().whenCorridor)
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
						if (wall.GetComponent<WallScript>().whenRestroom)
						{
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
						}
						else
						{
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
						}
					}
				}
				foreach (var mask in _masks)
				{
					if (mask.GetComponent<WallScript>().whenRestroom)
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
					if (light.GetComponent<WallScript>().whenRestroom)
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
					if (floor.GetComponent<WallScript>().whenRestroom)
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
						if (wall.GetComponent<WallScript>().whenSmallCorridor)
						{
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
						}
						else
						{
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
						}
					}
				}
				foreach (var mask in _masks)
				{
					if (mask.GetComponent<WallScript>().whenSmallCorridor)
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
					if (light.GetComponent<WallScript>().whenSmallCorridor)
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
					if (floor.GetComponent<WallScript>().whenSmallCorridor)
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
					if (wall.GetComponent<WallScript>().whenStorage)
					{
						wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
					}
					else
					{
						wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
					}
				}
				foreach (var mask in _masks)
				{
					if (mask.GetComponent<WallScript>().whenStorage)
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
					if (light.GetComponent<WallScript>().whenStorage)
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
					if (floor.GetComponent<WallScript>().whenStorage)
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
						if (wall.GetComponent<WallScript>().whenMasterRoom)
						{
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
						}
						else
						{
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
						}
					}
				}
				foreach (var mask in _masks)
				{
					if (mask.GetComponent<WallScript>().whenMasterRoom)
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
					if (light.GetComponent<WallScript>().whenMasterRoom)
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
					if (floor.GetComponent<WallScript>().whenMasterRoom)
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
						if (wall.GetComponent<WallScript>().whenBalcony)
						{
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
						}
						else
						{
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
						}
					}
				}
				foreach (var mask in _masks)
				{
					if (mask.GetComponent<WallScript>().whenBalcony)
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
					if (light.GetComponent<WallScript>().whenBalcony)
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
					if (floor.GetComponent<WallScript>().whenBalcony)
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
						if (wall.GetComponent<WallScript>().whenDaughtorRoom)
						{
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
						}
						else
						{
							wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
						}
					}
				}
				foreach (var mask in _masks)
				{
					if (mask.GetComponent<WallScript>().whenDaughtorRoom)
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
					if (light.GetComponent<WallScript>().whenDaughtorRoom)
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
					if (floor.GetComponent<WallScript>().whenDaughtorRoom)
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
