using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomEndGameScript : MonoBehaviour
{
    private Enemy boss;
    public GameObject dialogueTrigger;
    public List<GameObject> smallBears;
    private bool endDialogue_called = false;

    private void Start()
    {
        if (EffectStorage.me.mainEnemyOfThisLevel != null)
        {
            boss = EffectStorage.me.mainEnemyOfThisLevel.GetComponent<Enemy>();
        }
    }

    private void Update()
    {
        if (boss.health <= 0 && dialogueTrigger != null)
        {
			foreach (var bear in smallBears)
			{
                if (bear.GetComponent<SmallBear>())
				{
                    bear.GetComponent<SmallBear>().health = 0;
				}
			}
            if (!endDialogue_called)
			{
                endDialogue_called = true;
                dialogueTrigger.SetActive(true);
            }
        }
    }
}
