using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CollisionDetectorScript : MonoBehaviour
{
    public float lifeSpan;
	public List<EffectStructNew> myEffects;
	public EffectStructNew dummyEffectForDmg;
	public EffectStructNew dummyEffectForBreak;

	private void Update()
	{
		Destroy(gameObject, lifeSpan);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!CompareTag("PlayerSpawnedBear"))
		{
			print(other.gameObject.name);
			InflictEffects(other.gameObject);
		}
	}

	public void InflictEffects(GameObject target)
	{
		//print("effects: "+myEffects.Count);
		//foreach (var effect in myEffects.ToList())
		{
			if (target.CompareTag("Enemy")) //&&
				//effect.toWhom == EffectStructNew.Target.collisionEnemy)
			{
				float dummyATK = 0;
				float dummyAMP = 1;
				float dummy_break_amp = 0;
				for (int i = 0; i < myEffects.Count; i++) // loop through each effect this spell contains
				{
					if (myEffects[i].toWhom == EffectStructNew.Target.collisionEnemy) // check if the effect is applied when collidiing an enemy
					{
						dummyATK += myEffects[i].atk; // add effects' atks together to dummy atk
						dummyAMP *= myEffects[i].amp; // times effects' amps together to dummy amp
						dummy_break_amp += myEffects[i].amp; // add amp toghther to break
						EffectStructNew tempEffectStruct = myEffects[i]; // temp struct so that we can alter the effects' atk and amp
						//tempEffectStruct.atk = 0; // set to zero since we took the atk out
						//tempEffectStruct.amp = 0;
						myEffects[i] = tempEffectStruct; // set it back
						//EffectManagerNew.me.SpawnEffectHolders(target.gameObject, myEffects[i], gameObject.transform.position); // record effects (without atk and amp) to the enemy
					}
					
				}
				// record dmg effect dummy to deal dmg
				dummyEffectForDmg.amp = dummyAMP;
				dummyEffectForDmg.atk = dummyATK;
				//print("dummyATK"+dummyATK);
				dummyEffectForBreak.amp = dummy_break_amp;
				dummyEffectForBreak.doThis = EffectStructNew.Effect.ampDummy;
				EffectManagerNew.me.SpawnEffectHolders(target.gameObject, dummyEffectForDmg, gameObject.transform.position);
				EffectManagerNew.me.SpawnEffectHolders(target.gameObject, dummyEffectForBreak, gameObject.transform.position);
			}
			else if (target.CompareTag("Player"))
			{
				foreach (var effect in myEffects.ToList())
				{
					if (effect.toWhom == EffectStructNew.Target.player)
					{
						EffectManagerNew.me.SpawnEffectHolders(PlayerScript.me.gameObject, effect, gameObject.transform.position);
					}
				}	
			}
			else
			{
				print(target.gameObject.name + ": " + target.gameObject.tag);
			}
		}
	}
}
