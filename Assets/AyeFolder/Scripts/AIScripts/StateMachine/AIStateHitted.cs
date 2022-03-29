using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AIStateHitted : AIStateBase
{
    public float hitTimer;
    private float timer_flashWhite;
    private Material ogMat;
    //public float myEnemyVelocity;
    public override void StartState(Enemy myEnemy)
    {
        ogMat = myEnemy.GetComponentInChildren<SkinnedMeshRenderer>().material;
        timer_flashWhite = .2f;
        FlashWhite(myEnemy);
        myEnemy.ghostRider.enabled = false;
        myEnemy.AIAnimator.Play("Hitted", 0, 0);
        FlashWhite(myEnemy);
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
    }

    private void FlashWhite(Enemy myEnemy)
    {
        myEnemy.GetComponentInChildren<SkinnedMeshRenderer>().material = myEnemy.flashWhite;
    }
}
