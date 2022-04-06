using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomEndGameScript : MonoBehaviour
{
    private Enemy boss;
    public GameObject dialogueTrigger;

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
            dialogueTrigger.SetActive(true);
        }
    }
}
