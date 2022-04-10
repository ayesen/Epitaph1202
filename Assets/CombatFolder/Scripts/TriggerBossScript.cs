using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBossScript : MonoBehaviour
{
    public Enemy boss;
    public List<DoorScript> entrance_doors;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(("Player")))
        {
            BGMMan.bGMManger.StartTeddyBattleMusic();
            boss.phase = Enemy.AIPhase.InBattle1;
            boss.GetComponent<Enemy>().myEntrances = new List<DoorScript>(entrance_doors); // copy entrance list so that all bears know which door to close when player dies
            Destroy(gameObject);
        }
    }
}
