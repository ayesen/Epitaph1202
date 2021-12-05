using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateDie : AIStateBase
{
    public AIStateBase oldState;
    public override void StartState(Enemy myEnemy)
    {
        myEnemy.myTrigger.myMR.enabled = false;
        myEnemy.ghostRider.enabled = false;
        if(myEnemy.Mother != null)
        {
            myEnemy.Mother.BackKids();

        }
    }

    public override void Update(Enemy myEnemy)
    {
        myEnemy.hittedStates.text = "DEAD";
    }

    public override void LeaveState(Enemy myEnemy)
    {
        myEnemy.myTrigger.myMR.enabled = true;
        myEnemy.ghostRider.enabled = true;
        myEnemy.hittedStates.text = "";
    }
}
