using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateHitted : AIStateBase
{
    public float hitTimer;
    public float myEnemyVelocity;
    public override void StartState(Enemy myEnemy)
    {
        myEnemy.ghostRider.enabled = false;
        myEnemy.AIAnimator.Play("Hitted");
        myEnemyVelocity = myEnemy.GetComponent<Rigidbody>().velocity.magnitude;
    }

    public override void Update(Enemy myEnemy)
    {
        if(myEnemyVelocity >= 1f)
        {
            hitTimer += Time.fixedDeltaTime;
            if (hitTimer > myEnemy.hittedTime)
            {
                if (myEnemy.interruptedState != myEnemy.myAC.dieState || myEnemy.interruptedState != myEnemy.myAC.changePhaseState)
                {
                    myEnemy.myAC.ChangeState(myEnemy.myAC.idleState);
                }
                else
                    myEnemy.myAC.ChangeState(myEnemy.interruptedState);
            }

        }


    }

    public override void LeaveState(Enemy myEnemy)
    {
        myEnemy.ghostRider.enabled = true;
    }
}
