using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class AIStateChangePhase : AIStateBase
{
    public float changePhaseTimer;
    public override void StartState(Enemy myEnemy)
    {
        myEnemy.AIAnimator.Play("RoarBear");
        if (myEnemy.interruptedState != myEnemy.myAC.hittedState)
        {
            if (myEnemy.phase == Enemy.AIPhase.InBattle2)
            {
                myEnemy.changeLimit -= 1;
            }
        }

    }

    public override void Update(Enemy myEnemy)
    {
		changePhaseTimer += Time.fixedDeltaTime;
		if (changePhaseTimer > myEnemy.changePhaseTime)
		{
            Debug.Log("is it you");
			myEnemy.myAC.ChangeState(myEnemy.myAC.idleState);
		}
	}

    public override void LeaveState(Enemy myEnemy)
    {
        changePhaseTimer = 0;
        if (myEnemy.Mother != null)
        {
            if (myEnemy.phase == Enemy.AIPhase.InBattle2)
            {
                myEnemy.walkable = false;
                myEnemy.def = myEnemy.def_weak;
                
                myEnemy.Mother.OutKids();
            }
            else if (myEnemy.phase == Enemy.AIPhase.InBattle1)
            {
                myEnemy.walkable = true;
                myEnemy.def = myEnemy.def_normal;
                //myEnemy.Mother.BackKids();
            }
        }
    }
}
