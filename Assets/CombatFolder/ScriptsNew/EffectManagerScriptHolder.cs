using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class EffectManagerScriptHolder : MonoBehaviour
{
	private List<GameObject> enemyInfoUI;
	// this script is for calling function from effect manager new in animation

	private void Start()
	{
		enemyInfoUI = GameObject.FindGameObjectsWithTag("EnemyInfoUI").ToList();
	}

	public void Casting()
	{
		ConditionStruct cs = new ConditionStruct
		{
			condition = EffectStructNew.Condition.casting,
			conditionTrigger = PlayerScriptNew.me.gameObject
		};
		EffectManagerNew.me.conditionProcessList.Add(cs);
		// clear all combat info
		foreach (var ui in enemyInfoUI)
		{
			ui.GetComponent<TextMeshProUGUI>().text = "";
		}
	}
}
