using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIStateWalking : AIStateBase
{

    public override void StartState(Enemy myEnemy)
    {
        myEnemy.AIAnimator.Play("Walk");
        myEnemy.myTrigger.myMR.enabled = false;
        myEnemy.ghostRider.isStopped = false;
    }

    public override void Update(Enemy myEnemy)
    {

        if (myEnemy.walkable)
        {
            myEnemy.ChaseTarget();

            if (myEnemy.InRange())
            {
                if (myEnemy.attackable)
                {
                    myEnemy.myAC.ChangeState(myEnemy.myAC.preAttackState);
                }
                else if (!myEnemy.attackable)
                {
                    myEnemy.myAC.ChangeState(myEnemy.myAC.idleState);
                }
            }
            
			else // if chasing down event target location
			{
    //            if (Vector3.Distance(myEnemy.gameObject.transform.position, myEnemy.eventTarget.position) < myEnemy.stopDis && // if the enemy reaches the event target location
    //                GameManager.me.stateOfLevel == 1) // if it's the second time the player enters the level, which is level state 2
				//{
    //                EnemyDialogueManagerScript.me.SpawnDialogueTrigger(0);
				//}
			}
        }
        else if(!myEnemy.walkable)
        {
            myEnemy.myAC.ChangeState(myEnemy.myAC.idleState);
        }
    }

    public override void LeaveState(Enemy myEnemy)
    {
        myEnemy.ghostRider.isStopped = true;
    }
}
