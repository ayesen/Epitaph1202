using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpellScript : MonoBehaviour
{
	public List<GameObject> mats;
	public ParticleSystem burst;
	public ParticleSystem fragments;
	public List<EffectStructNew> myEffects;
	public int hit_amount;
	public float hit_interval;
	private float lifespan;
	private float deathTimer;
	[Header("LASTWORD EVENT")]
	public GameObject collisionPrefab;

	private void Start()
	{
		float life = float.MaxValue;
		foreach (var mat in PlayerScriptNew.me.selectedMats)
		{
			if (mat.GetComponent<MatScriptNew>().lifespan < life)
			{
				life = mat.GetComponent<MatScriptNew>().lifespan;
			}
		}
		lifespan = life;
		deathTimer = lifespan;
	}

	private void Update()
	{
		if (deathTimer > 0)
		{
			deathTimer -= Time.deltaTime;
		}
		else
		{
			DestroyEvent();
		}
	}
	private void OnCollisionEnter(Collision collision)
	{
		//print(("collided with " + collision.gameObject.name));
		if (collision.gameObject.CompareTag("InteractableObject"))
		{
			print(mats);
			if (mats.Contains(collision.gameObject.GetComponent<InteractableObjectScript>().reactionMat))
			{
				collision.gameObject.SendMessage("Reaction");
			}
		}
		else if (!collision.gameObject.CompareTag("Player"))
		{
			StartCoroutine(Detection(hit_amount, collision, collision.GetContact(0).point));
			GetComponent<BoxCollider>().enabled = false;
			GetComponent<MeshRenderer>().enabled = false;
		}
		
		DestroyEvent();
	}

	IEnumerator Detection(int hitAmount, Collision hit, Vector3 hitPos)
	{
		int amount = hitAmount;
		while (amount > 0)
		{
			if (hit.gameObject.CompareTag("Enemy")) // if hit enemy, inflict effects on enemy and spawn fragments vfx
			{
				ConditionStruct cs = new ConditionStruct
				{
					condition = EffectStructNew.Condition.collision_enemy,
					conditionTrigger = hit.gameObject
				};
				EffectManagerNew.me.conditionProcessList.Add(cs);
				// record effects to enemies
				bool recordEffect = true;
				foreach (var effect in myEffects) // if this spell spawn hit detection collider after death, effects should be passed to the collider instead
				{
					if (effect.doThis == EffectStructNew.Effect.spawnAOEDetectionAfterDeath)
					{
						recordEffect = false;
					}
				}
				if (recordEffect)
				{
					foreach (var effect in myEffects)
					{
						if (effect.toWhom == EffectStructNew.Target.collisionEnemy)
						{
							EffectManagerNew.me.SpawnEffectHolders(hit.gameObject, effect, gameObject.transform.position);
						}
					}
				}
				// vfx
				if (fragments != null)
				{
					ParticleSystem f = Instantiate(fragments);
					f.transform.position = hitPos;
				}
			}
			if (burst != null) // if hit, spawn burst vfx
			{
				// vfx
				ParticleSystem b = Instantiate(burst);
				b.transform.position = hitPos;
			}
			amount--;
			yield return new WaitForSeconds(hit_interval);
		}
	}

	private void DestroyEvent()
	{
		foreach (var effect in myEffects.ToList())
		{
			if (effect.doThis == EffectStructNew.Effect.spawnAOEDetectionAfterDeath)
			{
				myEffects.Remove(effect);
				EffectStorage.me.SpawnAOE(effect, gameObject);
			}
			else if (effect.doThis == EffectStructNew.Effect.spawnSmallBearAfterDeath)
			{
				myEffects.Remove(effect);
				EffectStorage.me.SpawnSmallBear(effect, gameObject);
			}
		}
		Destroy(gameObject);
	}
}
