using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestructionScript : MonoBehaviour
{
	Rigidbody rb;
	public float flyAmount;
	public float sinkSpd;
	public bool sink;

	public float death_time;
	private float death_timer;

	public GameObject impact_prefab;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		death_timer = death_time;
	}

	private void Update()
	{
		if (sink)
		{
			Sinking();
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Spell"))
		{
			if (collision.gameObject.GetComponent<SpellScript>() != null)
			{
				Vector3 dir = transform.position - collision.transform.position;
				rb.AddForce(dir.normalized * flyAmount, ForceMode.Impulse);
				StartCoroutine(StartSink());
				GameObject impact_sphere = Instantiate(impact_prefab);
				impact_sphere.transform.position = collision.transform.position;
				foreach (var mat in collision.gameObject.GetComponent<SpellScript>().mats)
				{
					impact_sphere.GetComponent<ImpactBallScript>().mats.Add(mat);
				}
			}
			else if (collision.gameObject.GetComponent<ImpactBallScript>() != null)
			{
				print(gameObject.name + ": collide");
				Vector3 dir = transform.position - collision.transform.position;
				rb.AddForce(dir.normalized * flyAmount, ForceMode.Impulse);
				StartCoroutine(StartSink());
			}
		}
	}

	IEnumerator StartSink()
	{
		yield return new WaitForSeconds(2);
		sink = true;
		GetComponent<Rigidbody>().isKinematic = true;
		if (GetComponent<MeshCollider>() != null)
		{
			GetComponent<MeshCollider>().enabled = false;
		}
		if (GetComponent<BoxCollider>() != null)
		{
			GetComponent<BoxCollider>().enabled = false;
		}
	}

	private void Sinking()
	{
		float y = transform.position.y - sinkSpd * Time.deltaTime;
		print(gameObject.name);
		transform.position = new Vector3(transform.position.x, y, transform.position.z);
		
		// count down self destruction
		if (death_timer > 0)
		{
			death_timer -= Time.deltaTime;
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
