using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStatePreAttack : AIStateBase
{
    public float preAtkTimer = 0;
    public override void StartState(Enemy myEnemy)
    {
        //Debug.Log("enter preattack");

        //myEnemy.myTrigger.myMR.enabled = true;
        if (myEnemy.phase == Enemy.AIPhase.InBattle1)
        {
            myEnemy.AIAnimator.Play("PreAttack1");
        }
        else if (myEnemy.phase == Enemy.AIPhase.InBattle2)
        {
            myEnemy.AIAnimator.Play("PreAttack2");
        }
    }

    public override void Update(Enemy myEnemy)
    {
        if (!MenuManager.GameIsPaused)
        {
            if (myEnemy.attackable)
            {
                preAtkTimer += Time.fixedDeltaTime;//change to after animation is over
                //myEnemy.TempPre(preAtkTimer);

                if (preAtkTimer > myEnemy.preAtkSpd)
                {
                    myEnemy.myAC.ChangeState(myEnemy.myAC.attackState);
                }
            }
            else if (!myEnemy.attackable)
            {
                myEnemy.myAC.ChangeState(myEnemy.myAC.idleState);
            }
        }
    }

    public override void LeaveState(Enemy myEnemy)
    {
        preAtkTimer = 0;
        
    }
}
