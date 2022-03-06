using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateDowned : AIStateBase
{
    public float hitTimer;
    public override void StartState(Enemy myEnemy)
    {
        myEnemy.ghostRider.enabled = false;
        myEnemy.AIAnimator.Play("Downed");
        myEnemy.def = myEnemy.def_weak;
    }

    public override void Update(Enemy myEnemy)
    {
        hitTimer += Time.deltaTime;
        if (hitTimer > myEnemy.hittedTime)
        {
            if (myEnemy.interruptedState != myEnemy.myAC.dieState || myEnemy.interruptedState != myEnemy.myAC.changePhaseState)
            {
                myEnemy.hittedTime = 0;
                hitTimer = 0;
                myEnemy.myAC.ChangeState(myEnemy.myAC.idleState);
            }
        }
    }

    public override void LeaveState(Enemy myEnemy)
    {
        myEnemy.ghostRider.enabled = true;
        myEnemy.def = myEnemy.def_normal;
    }
}
