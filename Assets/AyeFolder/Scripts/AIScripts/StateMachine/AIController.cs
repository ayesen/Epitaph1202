using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public Enemy myEnemy;

    public AIStateBase currentState;

    public AIStateBase idleState = new AIStateIdle();
    public AIStateBase walkingState = new AIStateWalking();
    public AIStateBase preAttackState = new AIStatePreAttack();
    public AIStateBase attackState = new AIStateAttacking();
    public AIStateBase postAttackState = new AIStatePostAttack();
    public AIStateBase changePhaseState = new AIStateChangePhase();
    public AIStateBase hittedState = new AIStateHitted();
    public AIStateBase dieState = new AIStateDie();
    public AIStateBase downedState = new AIStateDowned();

    // for tutorial bear only
    public bool tutBear;

    public void ChangeState(AIStateBase newState)
    {
        if (currentState != null)
        {
            currentState.LeaveState(myEnemy);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.StartState(myEnemy);
        }

    }


    private void Awake()
    {
        currentState = idleState;
        myEnemy = GetComponent<Enemy>();
    }

    void Start()
    {
        ChangeState(idleState);
    }

    void Update()
    {
        currentState.Update(myEnemy);
        //Debug.Log(currentState);
        if (tutBear)
		{
            TutorialManagerScript.me.tutBear_currentState = currentState;
		}
    }
}
