using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManagerScriptHolder : MonoBehaviour
{
	// this script is for calling function from effect manager new in animation
    public void Casting()
	{
		ConditionStruct cs = new ConditionStruct
		{
			condition = EffectStructNew.Condition.casting,
			conditionTrigger = PlayerScriptNew.me.gameObject
		};
		EffectManagerNew.me.conditionProcessList.Add(cs);
	}
}
