using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EffectStructNew
{
	public enum ConditionTriggeredBy // describe for this effect to take place, who need to trigger the condition
	{
		anyone, // anyone who triggers the condition
		owner_enemy, // owner enemy of the effect
		player // player
	};
	[Header("CONDITION")]
	public ConditionTriggeredBy when;
	public enum Condition // the condition for this effect to take place
	{
		none, // this will take place when the spell collides
		dealtDmg, // when the condition trigger is dealt dmg, only work after this effect is applied before
		collision_enemy, // when an enemy is hit. Effects that need to be triggered once attached need to have a condition of none
		collision_player, // when a player is hit, effects that need to be triggered once attached need to have a condition of none
		casting // when attack frame
	};
	
	public Condition iS;

	

	public enum Effect // describes the effect
	{
		stun,
		hurt,
		hurt_basedOnDis,
		hurt_DOT,
		break_atk,
		spawnExtraCollisionDetection,
		spawnExtraSpell,
		knockBack,
		spawnAOEDetectionAfterDeath,
		spawnSmallBearAfterDeath,
		heal,
		atkDummy,
		ampDummy
	};
	[Header("EFFECTS")]
	public Effect doThis;

	public enum Target // describes who this effect is applyed to
	{
		player,
		collisionEnemy,
		air
	};
	public Target toWhom;
	public enum Modifier
	{
		none,
		dmgDealt
	}
	public float forHowMuch; // describes the effect numericals
	public float atk; // 攻击
	public float amp; // 倍率
	public List<ModifierStruct> myModifiers; // describes the modifier to numericals
	public float forHowLong; // describes how long this effect will last
}
