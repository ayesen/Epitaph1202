using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStatePostAttack : AIStateBase
{
    public float postAtkTimer;
    public override void StartState(Enemy myEnemy)
    {
       
    }

    public override void Update(Enemy myEnemy)
    {
        if (myEnemy.attackable)
        {
            postAtkTimer += Time.fixedDeltaTime;//change to after animation is over
            myEnemy.TempPost(postAtkTimer);

            if (postAtkTimer > myEnemy.postAtkSpd)
            {
                myEnemy.myAC.ChangeState(myEnemy.myAC.idleState);
            }
        }
        else if (!myEnemy.attackable)
        {
            myEnemy.myAC.ChangeState(myEnemy.myAC.idleState);
        }

    }

    public override void LeaveState(Enemy myEnemy)
    {
        postAtkTimer = 0;
    }
}
