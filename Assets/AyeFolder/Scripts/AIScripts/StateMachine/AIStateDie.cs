using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateDie : AIStateBase
{
    public AIStateBase oldState;
    public override void StartState(Enemy myEnemy)
    {
        myEnemy.AIAnimator.Play("Die");
        myEnemy.myTrigger.myMR.enabled = false;
        myEnemy.ghostRider.enabled = false;
        myEnemy.GetComponent<Rigidbody>().useGravity = false;
        myEnemy.GetComponent<Rigidbody>().isKinematic = true;

        if(myEnemy.Mother != null)
        {
            myEnemy.Mother.BackKids();

        }
    }

    public override void Update(Enemy myEnemy)
    {
        myEnemy.hittedStates.text = "DEAD";
        myEnemy.GetComponent<CapsuleCollider>().enabled = false;
    }

    public override void LeaveState(Enemy myEnemy)
    {
        myEnemy.myTrigger.myMR.enabled = true;
        myEnemy.ghostRider.enabled = true;
        myEnemy.hittedStates.text = "";
    }
}
