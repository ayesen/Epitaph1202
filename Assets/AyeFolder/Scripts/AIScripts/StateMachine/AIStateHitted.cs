using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateHitted : AIStateBase
{
    public AIStateBase prevState;
    public override void StartState(Enemy myEnemy)
    {
        myEnemy.ghostRider.enabled = false;
    }

    public override void Update(Enemy myEnemy)
    {



    }

    public override void LeaveState(Enemy myEnemy)
    {
        myEnemy.ghostRider.enabled = true;
    }
}
