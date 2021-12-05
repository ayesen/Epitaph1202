using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateIdle : AIStateBase
{
    public float IdleTimer;

    public override void StartState(Enemy myEnemy)
    {
        myEnemy.AIAnimator.Play("Idle");
        if (myEnemy.ghostRider.enabled == true)
        {
            myEnemy.ghostRider.isStopped = true;
        }
    }

    public override void Update(Enemy myEnemy)
    {

        if (myEnemy.phase != Enemy.AIPhase.NotInBattle || myEnemy.phase != SmallBear.AIPhase.NotInBattle) // in battle phase 1 or 2
        {
            myEnemy.Idleing();
            if (myEnemy.InRange())
            {
                if (myEnemy.attackable)
                {
                    IdleTimer += Time.fixedDeltaTime;
                    if (IdleTimer > myEnemy.atkSpd)
                    {
                        myEnemy.myAC.ChangeState(myEnemy.myAC.preAttackState);
                    }
                }
                else if (!myEnemy.attackable)
                {

                }
            }
            else if (!myEnemy.InRange())
            {
                if (myEnemy.walkable)
                {
                    myEnemy.myAC.ChangeState(myEnemy.myAC.walkingState);
                }
                else if (!myEnemy.walkable)
                {
                    
                }
            }
        }
    }

    public override void LeaveState(Enemy myEnemy)
    {
        IdleTimer = 0;
    }
}
