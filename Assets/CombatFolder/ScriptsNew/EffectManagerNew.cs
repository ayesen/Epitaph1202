using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EffectManagerNew : MonoBehaviour
{
    public static EffectManagerNew me;
	public List<GameObject> currentMats;

	[Header("Condition Manager")]
	[HideInInspector]
	public List<ConditionStruct> conditionProcessList; // conditions to be processed
	[HideInInspector]
	public List<GameObject> enemiesEffected; // enemies that has effects on them

	[Header("Spawn Manager")]
	public float spellSpd;
	public Transform spellSpawnLoc;
	public GameObject spell_proj_prefab;
	public float extraSpawn_angleRange;
	public int spawnCount = 1; // number of spells to spawn, default is 1
	public int hitCount = 1; // number of detection, default is 1

	[Header("Effect Infliction")]
	public GameObject effectHolder_prefab;

	public enum CtrlType
	{
		cantWalk,
		cantAttack
	};

	private void Awake()
	{
		me = this;
	}

	private void Update()
	{
		ProcessConditions();
	}

	public void RefreshCurrentMats()
	{
		currentMats.Clear();
		foreach (var mat in PlayerScriptNew.me.selectedMats)
		{
			currentMats.Add(mat);
		}
	}

	public void ProcessConditions()
	{
		if (conditionProcessList.Count > 0)
		{
			foreach (var conditionProcess in conditionProcessList.ToList())
			{
				if (conditionProcess.condition == EffectStructNew.Condition.casting)
				{
					CastingEvent_Inflict();
					CastingEvent(conditionProcess.conditionTrigger);
				}
				else if (conditionProcess.condition == EffectStructNew.Condition.dealtDmg)
				{
					DmgDealtEvents(conditionProcess);
				}
				else if (conditionProcess.condition == EffectStructNew.Condition.collision_enemy)
				{
					EnemyHitEvent();
				}
				//NoneEvents();
				//print("condition processed: " + conditionProcess.condition + " condition trigger: " + conditionProcess.conditionTrigger);
				conditionProcessList.Remove(conditionProcess);
			}
		}
		// no condition events, effect will take place immediately with no condition check whatsoever
		NoneEvents();
	}

	#region Events
	#region Casting Events
	private void CastingEvent_Inflict() // record effects on targets, now all effects that player inflicts on themselves are recorded when casted
	{
		foreach (var mat in currentMats)
		{
			MatScriptNew ms = mat.GetComponent<MatScriptNew>();
			// inflict effects on target
			bool recordEffects = true;
			foreach (var effect in ms.myEffects)
			{
				if (effect.doThis == EffectStructNew.Effect.spawnAOEDetectionAfterDeath)
				{
					recordEffects = false;
				}
			}
			if (recordEffects)
			{
				foreach (var effect in ms.myEffects)
				{
					if (effect.toWhom == EffectStructNew.Target.player) // pick out effects that works on player
					{
						SpawnEffectHolders(PlayerScriptNew.me.gameObject, effect, gameObject.transform.position);
					}
				}
			}
		}
	}

	public void CastingEvent(GameObject conditionTrigger) // process effects on targets
	{
		// get effects inflicted on player
		foreach (var effectHolder in PlayerScriptNew.me.gameObject.GetComponent<EffectHoldersHolderScript>().effectHolders)
		{
			EffectHolderScript ehs = effectHolder.GetComponent<EffectHolderScript>();
			if (ehs.myEffect.when == EffectStructNew.ConditionTriggeredBy.player &&
				conditionTrigger == PlayerScriptNew.me.gameObject) // casting can only be triggered by player, but for the sake of unification, still checks for triggerer
			{
				if (ehs.myEffect.iS == EffectStructNew.Condition.casting) // only process effects that take place when casting condition is met
				{
					if (ehs.myEffect.doThis == EffectStructNew.Effect.spawnExtraSpell) // if effect spawns extra spells, add it to spawn count
					{
						EffectStorage.me.SpawnExtraSpell(ehs);
						ehs.destroy = true;
					}
					if (ehs.myEffect.doThis == EffectStructNew.Effect.spawnExtraCollisionDetection) // if effect increases hit count, add it to default hit count
					{
						EffectStorage.me.ExtraCollisionDetection(ehs);
						ehs.destroy = true;
					}
				}
			}
		}

		// get effects inflicted on enemy
		foreach (var enemy in enemiesEffected)
		{
			foreach (var effectHolder in enemy.GetComponent<EffectHoldersHolderScript>().effectHolders)
			{
				EffectHolderScript ehs = effectHolder.GetComponent<EffectHolderScript>();
				if (ehs.myEffect.when == EffectStructNew.ConditionTriggeredBy.player &&
					conditionTrigger == PlayerScriptNew.me.gameObject)
				{
					// effects
				}
			}
		}
		SpawnSpell(spawnCount, hitCount);
	}
	#endregion
	#region Enemy Hit Events
	// effects on enemy are inflicted, or in another word, recorded, when the collider hit an enemy collider
	public void EnemyHitEvent() // 
	{
		// check effects on player
		foreach (var effectHolder in PlayerScriptNew.me.gameObject.GetComponent<EffectHoldersHolderScript>().effectHolders)
		{
			EffectHolderScript ehs = effectHolder.GetComponent<EffectHolderScript>();
			if (ehs.myEffect.iS == EffectStructNew.Condition.collision_enemy)
			{
				
			}
		}
		
		// check effects on enemies
		foreach (var enemy in enemiesEffected)
		{
			foreach (var effectHolder in enemy.GetComponent<EffectHoldersHolderScript>().effectHolders)
			{
				EffectHolderScript ehs = effectHolder.GetComponent<EffectHolderScript>();
				if (ehs.myEffect.iS == EffectStructNew.Condition.collision_enemy)
				{
					if (ehs.myEffect.when == EffectStructNew.ConditionTriggeredBy.owner_enemy)
					{

					}
				}
			}
		}
	}
	#endregion
	#region None Events
	public void NoneEvents() // these effects will happen right after they're applied
	{
		// check effects on player
		foreach (var effectHolder in PlayerScriptNew.me.gameObject.GetComponent<EffectHoldersHolderScript>().effectHolders)
		{
			EffectHolderScript ehs = effectHolder.GetComponent<EffectHolderScript>();
			if (ehs.myEffect.iS == EffectStructNew.Condition.none)
			{
				if (ehs.myEffect.doThis == EffectStructNew.Effect.knockBack)
				{
					EffectStorage.me.KnockBack(ehs.myEffect.forHowMuch, ehs.myCreatorPos, PlayerScriptNew.me.gameObject);
					ehs.destroy = true;
				}
			}
		}

		// check effects on enemies
		foreach (var enemy in enemiesEffected) // get every enemies that have effects on them
		{
			foreach (var effectHolder in enemy.GetComponent<EffectHoldersHolderScript>().effectHolders) // get these enemies' effect holders
			{
				EffectHolderScript ehs = effectHolder.GetComponent<EffectHolderScript>();
				if (ehs.myEffect.iS == EffectStructNew.Condition.none)
				{ 
					// for each holder, check its effect
					if (ehs.myEffect.doThis == EffectStructNew.Effect.hurt)
					{
						EffectStorage.me.HurtEnemy(ehs, enemy);
						ehs.destroy = true;
					}
					if (ehs.myEffect.doThis == EffectStructNew.Effect.hurt_DOT)
					{
						EffectStorage.me.DotEnemy(ehs, enemy);
						ehs.destroy = true;
					}
					if (ehs.myEffect.doThis == EffectStructNew.Effect.stun)
					{
						EffectStorage.me.StunEnemy(ehs, enemy);
						ehs.destroy = true;
					}
					if (ehs.myEffect.doThis == EffectStructNew.Effect.break_atk)
					{
						//EffectStorage.me.Break(ehs, enemy);
						ehs.destroy = true;
					}
					if (ehs.myEffect.doThis == EffectStructNew.Effect.knockBack)
					{
						EffectStorage.me.KnockBack(ehs.myEffect.forHowMuch, ehs.myCreatorPos, enemy);
						ehs.destroy = true;
					}
					if (ehs.myEffect.doThis == EffectStructNew.Effect.hurt_basedOnDis)
					{
						EffectStorage.me.HurtEnemyBasedOnDis(ehs, enemy, Vector3.Distance(ehs.myCreatorPos, enemy.transform.position));
						ehs.destroy = true;
					}
					if (ehs.myEffect.doThis == EffectStructNew.Effect.ampDummy)
					{
						EffectStorage.me.Break(ehs, enemy);
						ehs.destroy = true;
					}
				}
			}
		}
	}
	#endregion
	#region Damge Dealt Events
	public void DmgDealtEvents(ConditionStruct condition)
	{
		// check effects on player
		foreach (var effectHolder in PlayerScriptNew.me.gameObject.GetComponent<EffectHoldersHolderScript>().effectHolders)
		{
			EffectHolderScript ehs = effectHolder.GetComponent<EffectHolderScript>();
			if (ehs.myEffect.iS == EffectStructNew.Condition.dealtDmg)
			{
				if (ehs.myEffect.doThis == EffectStructNew.Effect.heal)
				{
					EffectStorage.me.Heal(ehs, PlayerScriptNew.me.gameObject, condition);
				}
			}
		}

		// check effects on enemies
		foreach (var enemy in enemiesEffected) // get every enemies that have effects on them
		{
			foreach (var effectHolder in enemy.GetComponent<EffectHoldersHolderScript>().effectHolders) // get these enemies' effect holders
			{
				EffectHolderScript ehs = effectHolder.GetComponent<EffectHolderScript>();
				
				if (ehs.myEffect.iS == EffectStructNew.Condition.dealtDmg) // check effect situation
				{
					if (ehs.myEffect.when == EffectStructNew.ConditionTriggeredBy.owner_enemy && // if this condition need to be triggered by the owner of the effect holder
						ehs.myOwner == condition.conditionTrigger) // if the owner of the effect holder matches the condition trigger
					{
						if (ehs.myEffect.doThis == EffectStructNew.Effect.stun) // check effect
						{
							EffectStorage.me.StunEnemy(ehs, enemy);
						}
						if (ehs.myEffect.doThis == EffectStructNew.Effect.break_atk)
						{
							//EffectStorage.me.Break(ehs, enemy);
						}
					}
				}
			}
		}
	}
	#endregion
	//! events that happen when the spell is destroyed due to lifespan is in spell scripts
	#endregion
	
	public void SpawnEffectHolders(GameObject target, EffectStructNew effect, Vector3 creatorPos)
	{
		GameObject effectHolder = Instantiate(effectHolder_prefab, target.transform);
		EffectHolderScript ehs = effectHolder.GetComponent<EffectHolderScript>();
		ehs.myOwner = target;
		ehs.myEffect = effect;
		ehs.myCreatorPos = creatorPos;
		if (target != null && target.GetComponent<EffectHoldersHolderScript>().enabled)
		{
			target.GetComponent<EffectHoldersHolderScript>().effectHolders.Add(effectHolder);
		}
	}
	#region spawn spell functions
	private void SpawnSpell(int spawnAmount, int hitAmount)
	{
		int mAmount = spawnAmount;
		while (mAmount > 0)
		{
			RotateSpellSpawnLoc();
			if (mAmount == 1)
			{
				ResetSpellSpawnLoc();
			}
			DefaultSpawn(hitAmount);
			mAmount--;
		}
		spawnCount = 1;
		hitCount = 1;
		SoundMan.SoundManager.CastFlying();
	}

	private void DefaultSpawn(int hitAmount)
	{
		GameObject spell = Instantiate(spell_proj_prefab, spellSpawnLoc.position, spellSpawnLoc.rotation);
		spell.GetComponent<Rigidbody>().AddForce(spellSpawnLoc.transform.forward * spellSpd, ForceMode.Impulse);
		spell.GetComponent<SpellScript>().hit_amount = hitAmount;
		spell.GetComponent<SpellScript>().mats = currentMats.ToList();
		foreach (var mat in currentMats)
		{
			foreach (var effect in mat.GetComponent<MatScriptNew>().myEffects)
			{
				spell.GetComponent<SpellScript>().myEffects.Add(effect);
			}
		}
	}

	#region for spawning extra spells
	private void RotateSpellSpawnLoc()
	{
		float dir_x = Random.Range(-extraSpawn_angleRange, extraSpawn_angleRange);
		float dir_y = Random.Range(-extraSpawn_angleRange, extraSpawn_angleRange);
		float dir_z = Random.Range(-extraSpawn_angleRange, extraSpawn_angleRange);
		spellSpawnLoc.rotation = Quaternion.Euler(dir_x + spellSpawnLoc.rotation.eulerAngles.x, dir_y + spellSpawnLoc.rotation.eulerAngles.y, dir_z + spellSpawnLoc.rotation.eulerAngles.z);
	}

	private void ResetSpellSpawnLoc()
	{
		spellSpawnLoc.localRotation = Quaternion.Euler(0, 0, 0);
	}
	#endregion

	#endregion
}
