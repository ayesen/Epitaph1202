using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBossScript : MonoBehaviour
{
    public Enemy boss;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(("Player")))
        {
            boss.phase = Enemy.AIPhase.InBattle1;
        }
    }
}
