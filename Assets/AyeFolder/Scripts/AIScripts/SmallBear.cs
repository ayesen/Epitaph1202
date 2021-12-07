using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBear : Enemy
{
    private int smallBearHealthRecord;

    private void Awake()
    {
        Debug.Log(this.gameObject.name);
        this.smallBearHealthRecord = maxHealth;
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
        //Debug.Log(phase);
        //Debug.Log(myAC.currentState);
        if (knockedBack)
        {
            ReactivateNavMesh();
        }
    }

    public void ResetSmallBear()
    {
        maxHealth = this.smallBearHealthRecord;
        health = this.smallBearHealthRecord;
        ChangePhase(AIPhase.NotInBattle, 0);
    }
    

}
