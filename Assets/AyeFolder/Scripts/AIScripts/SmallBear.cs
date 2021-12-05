using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBear : Enemy
{
    private int healthRecord;

    private void Awake()
    {
        Debug.Log(this.gameObject.name);
        this.healthRecord = maxHealth;
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
        if (knockedBack)
        {
            ReactivateNavMesh();
        }
    }

    public void ResetSmallBear()
    {
        maxHealth = this.healthRecord;
        health = this.healthRecord;
        ChangePhase(AIPhase.InBattle1, 1);
    }
    

}
