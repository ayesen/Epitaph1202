using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateDowned : AIStateBase
{
    public float hitTimer;
    private float timer_flashWhite;
    private Material ogMat;

    public override void StartState(Enemy myEnemy)
    {
        ogMat = myEnemy.GetComponentInChildren<SkinnedMeshRenderer>().material;
        timer_flashWhite = .2f;
        FlashWhite(myEnemy);
        myEnemy.ghostRider.enabled = false;
        myEnemy.AIAnimator.Play("Downed");
        myEnemy.def = myEnemy.def_weak;
    }

    public override void Update(Enemy myEnemy)
    {
        hitTimer += Time.deltaTime;
        if (hitTimer > myEnemy.hittedTime &&
            !myEnemy.GetComponent<AIController>().tutBear)
        {
            if (myEnemy.interruptedState != myEnemy.myAC.dieState || myEnemy.interruptedState != myEnemy.myAC.changePhaseState)
            {
                myEnemy.hittedTime = 0;
                hitTimer = 0;
                myEnemy.myAC.ChangeState(myEnemy.myAC.idleState);
            }
        }
        if (timer_flashWhite > 0)
		{
            timer_flashWhite -= Time.deltaTime;
		}
		else
		{
            myEnemy.GetComponentInChildren<SkinnedMeshRenderer>().material = ogMat;
        }
    }

    public override void LeaveState(Enemy myEnemy)
    {
        myEnemy.ghostRider.enabled = true;
        myEnemy.def = myEnemy.def_normal;
        myEnemy.GetComponentInChildren<SkinnedMeshRenderer>().material = ogMat;
    }

    private void FlashWhite(Enemy myEnemy)
    {
        myEnemy.GetComponentInChildren<SkinnedMeshRenderer>().material = myEnemy.flashWhite;
    }
}
