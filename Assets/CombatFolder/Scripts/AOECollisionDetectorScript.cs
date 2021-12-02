using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOECollisionDetectorScript : MonoBehaviour
{
    public float lifeSpan;
	public List<EffectStructNew> myEffects;

	private void Start()
	{
		foreach (var effect in myEffects)
		{
			print(effect.doThis);
		}
	}

	private void Update()
	{
		Destroy(gameObject, lifeSpan);
	}

	private void OnTriggerEnter(Collider other)
	{
		foreach (var effect in myEffects)
		{
			if (other.CompareTag("Enemy") &&
				effect.toWhom == EffectStructNew.Target.collisionEnemy)
			{
				EffectManagerNew.me.SpawnEffectHolders(other.gameObject, effect, gameObject.transform.position);
			}
			else if (other.CompareTag("Player") &&
				effect.toWhom == EffectStructNew.Target.player)
			{
				EffectManagerNew.me.SpawnEffectHolders(PlayerScript.me.gameObject, effect, gameObject.transform.position);
			}
		}
	}
}
