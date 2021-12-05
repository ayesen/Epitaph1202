using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetectorScript : MonoBehaviour
{
    public float lifeSpan;
	public List<EffectStructNew> myEffects;

	private void Update()
	{
		Destroy(gameObject, lifeSpan);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!CompareTag("PlayerSpawnedBear"))
		{
			InflictEffects(other.gameObject);
		}
	}

	public void InflictEffects(GameObject target)
	{
		foreach (var effect in myEffects)
		{
			if (target.CompareTag("Enemy") &&
				effect.toWhom == EffectStructNew.Target.collisionEnemy)
			{
				print("inflicted effects on: " + target.name);
				EffectManagerNew.me.SpawnEffectHolders(target.gameObject, effect, gameObject.transform.position);
			}
			else if (target.CompareTag("Player") &&
				effect.toWhom == EffectStructNew.Target.player)
			{
				EffectManagerNew.me.SpawnEffectHolders(PlayerScript.me.gameObject, effect, gameObject.transform.position);
			}
		}
	}
}
