using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBear : Enemy
{
    private int healthRecord;

    private void Awake()
    {
        healthRecord = maxHealth;
        myTrigger = myTriggerObj.GetComponent<AtkTrigger>();
        myAC = GetComponent<AIController>();
        health = maxHealth;
        ghostRider = GetComponent<UnityEngine.AI.NavMeshAgent>();
        phase = Enemy.AIPhase.InBattle1;
    }
    void Start()
    {
        
    }


    void Update()
    {
        HittedStatesIndication();
        AIDead();
        Debug.Log(phase);
        Debug.Log(myAC.currentState);
    }

    public void ResetSmallBear()
    {
        maxHealth = healthRecord;
        health = healthRecord;
        ChangePhase(AIPhase.NotInBattle, 1);
    }
    

}
