using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIStateBase
{
 
    public abstract void StartState(Enemy myEnemy);

    public abstract void Update(Enemy myEnemy);

    public abstract void LeaveState(Enemy myEnemy);
}
