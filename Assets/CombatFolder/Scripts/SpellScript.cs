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
	public EffectStructNew dummyEffectForDmg;
	public EffectStructNew dummyEffectForBreak;
	[Header("LASTWORD EVENT")]
	public GameObject collisionPrefab;
	[Header("VFX")]
	public GameObject red_light;
	public GameObject yellow_light;
	public GameObject blue_light;

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
		DecideLightColor();
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
		if (collision.gameObject.CompareTag("InteractableObject"))
		{
			if (mats.Contains(collision.gameObject.GetComponent<CandleScript>().reactionMat))
			{
				collision.gameObject.SendMessage("Reaction");
			}
			Destroy(gameObject);
		}
		else if (!collision.gameObject.CompareTag("Player"))
		{
			StartCoroutine(Detection(hit_amount, collision, collision.GetContact(0).point));
			GetComponent<BoxCollider>().enabled = false;
			GetComponent<MeshRenderer>().enabled = false;
			DestroyEvent();
		}
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
					if (effect.doThis == EffectStructNew.Effect.spawnAOEDetectionAfterDeath || effect.doThis == EffectStructNew.Effect.spawnSmallBearAfterDeath)
					{
						recordEffect = false;
					}
				}
				if (recordEffect)
				{
					float dummyATK = 0;
					float dummyAMP = 1;
					float dummy_break_amp = 0;
					for (int i = 0; i < myEffects.Count; i++) // loop through each effect this spell contains
					{
						//if (myEffects[i].toWhom == EffectStructNew.Target.collisionEnemy) // check if the effect is applied when collidiing an enemy
						{
							dummyATK += myEffects[i].atk; // add effects' atks together to dummy atk
							dummyAMP *= myEffects[i].amp; // times effects' amps together to dummy amp
							dummy_break_amp += myEffects[i].amp; // add amp toghther to break
							EffectStructNew tempEffectStruct = myEffects[i]; // temp struct so that we can alter the effects' atk and amp
							tempEffectStruct.atk = 0; // set to zero since we took the atk out
							tempEffectStruct.amp = 0;
							myEffects[i] = tempEffectStruct; // set it back
							//EffectManagerNew.me.SpawnEffectHolders(hit.gameObject, myEffects[i], gameObject.transform.position); // record effects (without atk and amp) to the enemy
						}
					}
					// record dmg effect dummy to deal dmg
					dummyEffectForDmg.amp = dummyAMP;
					dummyEffectForDmg.atk = dummyATK;
					dummyEffectForBreak.amp = dummy_break_amp;
					dummyEffectForBreak.doThis = EffectStructNew.Effect.ampDummy;
					EffectManagerNew.me.SpawnEffectHolders(hit.gameObject, dummyEffectForDmg, gameObject.transform.position);
					EffectManagerNew.me.SpawnEffectHolders(hit.gameObject, dummyEffectForBreak, gameObject.transform.position);
					if (TutorialManagerScript.me.tut_state != 0 && TutorialManagerScript.me.tut_state != 4)
					{
						TutorialManagerScript.me.PassCombination(mats);
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

	private void DecideLightColor()
	{
		bool amp = false;
		bool atk = false;

		foreach (var mat in mats)
		{
			switch (mat.GetComponent<MatScriptNew>().myType)
			{
				case MatScriptNew.MatType.amp:
					amp = true;
					break;
				case MatScriptNew.MatType.atk:
					atk = true;
					break;
			}
		}
		
		if (amp && atk)
		{
			red_light.SetActive(true);
		}
		else if (amp)
		{
			blue_light.SetActive(true);
		}
		else if (atk)
		{
			yellow_light.SetActive(true);
		}
	}
}
