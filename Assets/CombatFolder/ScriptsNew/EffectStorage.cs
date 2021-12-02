using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EffectStorage : MonoBehaviour
{
    public static EffectStorage me;
	[Header("DOT Manager")]
	public float dot_interval;
	public ParticleSystem fragments_dot;
	[Header("Break Manager")]
	public float droppedMat_flyAmount;
	public List<GameObject> droppableMat;
	public GameObject droppedMat_prefab;
	[Header("AOE Manager")]
	public GameObject collisionPrefab;
	
	private void Awake()
	{
		me = this;
	}
	private void Start()
	{
		GetDroppableMats();
	}
	#region DMG
	public void HurtEnemy(EffectHolderScript ehs, GameObject enemy)
	{
		enemy.GetComponent<Enemy>().LoseHealth((int)ehs.myEffect.forHowMuch);
	}
	public void HurtEnemyBasedOnDis(EffectHolderScript ehs, GameObject enemy, float dis)
	{
		float dmgToDeal = 1f / dis * ehs.myEffect.forHowMuch;
		enemy.GetComponent<Enemy>().LoseHealth((int)dmgToDeal);
	}
	public void DotEnemy(EffectHolderScript ehs, GameObject enemy)
	{
		StartCoroutine(DoDot(ehs, enemy));
	}
	IEnumerator DoDot(EffectHolderScript ehs, GameObject enemy)
	{
		yield return new WaitForSeconds(dot_interval);
		float timer = ehs.myEffect.forHowLong;
		while (timer > 0)
		{
			timer--;
			enemy.GetComponent<Enemy>().LoseHealth((int)ehs.myEffect.forHowMuch);
			SpawnParticle(fragments_dot, enemy.transform.position);
			yield return new WaitForSeconds(dot_interval);
		}
	}
	#endregion
	#region CTRL
	public void StunEnemy(EffectHolderScript ehs, GameObject enemy)
	{
		enemy.GetComponent<Enemy>().walkable = false;
		enemy.GetComponent<Enemy>().attackable = false;
		StartCoroutine(ResetEnemyStatus(enemy, EffectManagerNew.CtrlType.cantWalk, ehs.myEffect.forHowMuch));
		StartCoroutine(ResetEnemyStatus(enemy, EffectManagerNew.CtrlType.cantAttack, ehs.myEffect.forHowMuch));
	}
	IEnumerator ResetEnemyStatus(GameObject target, EffectManagerNew.CtrlType type, float duration)
	{
		yield return new WaitForSeconds(duration);
		if (type == EffectManagerNew.CtrlType.cantAttack)
		{
			target.GetComponent<Enemy>().attackable = true;
		}
		else if (type == EffectManagerNew.CtrlType.cantWalk)
		{
			target.GetComponent<Enemy>().walkable = true;
		}
	}
	public void KnockBack(float amount, Vector3 erPos, GameObject ee)
	{
		if (ee.GetComponent<NavMeshAgent>())
		{
			ee.GetComponent<NavMeshAgent>().enabled = false;
			ee.GetComponent<Rigidbody>().isKinematic = false;
		}
		Vector3 dir = ee.transform.position - erPos;
		ee.GetComponent<Rigidbody>().AddForce(dir.normalized * amount, ForceMode.Impulse);
	}
	#endregion
	#region BREAK
	public void Break(EffectHolderScript ehs, GameObject enemy)
	{
		if (enemy.GetComponent<Enemy>() != null &&
			droppableMat.Count > 0)
		{
			Enemy eS = enemy.GetComponent<Enemy>();
			eS.breakMeter -= ehs.myEffect.forHowMuch;
			if (eS.breakMeter <= 0)
			{
				eS.breakMeter = eS.breakMeterMax;
				BreakNSpawnMat(enemy);
			}
		}
	}
	private void GetDroppableMats()
	{
		foreach (var mat in PlayerScriptNew.me.matSlots)
		{
			droppableMat.Add(mat);
		}
	}
	private void BreakNSpawnMat(GameObject enemy)
	{
		foreach (var mat in droppableMat)
		{
			GameObject matDropped = mat;
			Vector3 spawnPos = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 0.7f, enemy.transform.position.z);
			GameObject droppedMat = Instantiate(droppedMat_prefab, spawnPos, Random.rotation);
			droppedMat.GetComponent<DroppedMatScript>().myMat = matDropped;
			droppedMat.GetComponent<DroppedMatScript>().amount = matDropped.GetComponent<MatScriptNew>().amount_max;
			droppedMat.GetComponent<Rigidbody>().AddForce(
				new Vector3(Random.Range(-droppedMat_flyAmount, droppedMat_flyAmount),
				3, // force upward
				Random.Range(-droppedMat_flyAmount, droppedMat_flyAmount)),
				ForceMode.Impulse);
		}
	}
	#endregion
	#region HEAL AND BUFFS
	public void Heal(EffectHolderScript ehs, GameObject target, ConditionStruct condition)
	{
		if (target.CompareTag("Player"))
		{
			PlayerScriptNew.me.hp += (int)(ehs.myEffect.forHowMuch + ProcessModifers(ehs.myEffect, condition));
		}
	}
	#endregion
	#region SPAWN SPELL RELATED
	public void SpawnExtraSpell(EffectHolderScript ehs)
	{
		EffectManagerNew.me.spawnCount += (int)ehs.myEffect.forHowMuch;
	}
	public void ExtraCollisionDetection(EffectHolderScript ehs)
	{
		EffectManagerNew.me.hitCount += (int)ehs.myEffect.forHowMuch;
	}
	#endregion
	#region DEATHWORD RELATED
	public void SpawnAOE(EffectStructNew effect, GameObject spell)
	{
		GameObject collisionDetector = Instantiate(collisionPrefab, spell.transform.position, Quaternion.identity);
		collisionDetector.transform.localScale = new Vector3(effect.forHowMuch, effect.forHowMuch, effect.forHowMuch);
		collisionDetector.GetComponent<AOECollisionDetectorScript>().lifeSpan = effect.forHowLong;
		foreach (var effectToBePassed in spell.GetComponent<SpellScript>().myEffects)
		{
			collisionDetector.GetComponent<AOECollisionDetectorScript>().myEffects.Add(effectToBePassed);
		}
	}
	#endregion
	public void SpawnParticle(ParticleSystem particle, Vector3 pos)
	{
		ParticleSystem f = Instantiate(particle);
		f.transform.position = pos;
	}
	private float ProcessModifers(EffectStructNew es, ConditionStruct condition)
	{
		float outcome = 0;
		foreach (var modifier in es.myModifiers)
		{
			if (modifier.modifierType == EffectStructNew.Modifier.dmgDealt)
			{
				if (modifier.plus > 0)
				{
					outcome = modifier.plus + condition.dmgAmount;
				}
				else if (modifier.times > 0)
				{
					outcome = modifier.times * condition.dmgAmount;
				}
			}
		}
		return outcome;
	}
}
