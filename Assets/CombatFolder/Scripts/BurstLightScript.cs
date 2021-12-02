using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstLightScript : MonoBehaviour
{
	public float dimSpd;
	private Light _light;

	private void Start()
	{
		//Destroy(gameObject, life);
		_light = GetComponent<Light>();
	}

	private void Update()
	{
		if (_light.intensity > 0)
		{
			_light.intensity -= dimSpd * Time.deltaTime;
		}
	}
}
