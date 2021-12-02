using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBear : Enemy
{

    private void Awake()
    {
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
    

}
