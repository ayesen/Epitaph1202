using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateDie : AIStateBase
{
    public AIStateBase oldState;
    private float timer_flashWhite;
    public override void StartState(Enemy myEnemy)
    {
        timer_flashWhite = .2f;
        FlashWhite(myEnemy);
        myEnemy.AIAnimator.Play("Die");
        myEnemy.myTrigger.myMR.enabled = false;
        myEnemy.ghostRider.enabled = false;
        myEnemy.GetComponent<Rigidbody>().useGravity = false;
        myEnemy.GetComponent<Rigidbody>().isKinematic = true;

        if(myEnemy.Mother != null)
        {
            //myEnemy.Mother.BackKids();

        }
        myEnemy.doorTrigger = true;
    }

    public override void Update(Enemy myEnemy)
    {
        //myEnemy.hittedStates.text = "DEAD";
        myEnemy.GetComponent<CapsuleCollider>().enabled = false;
        if (timer_flashWhite > 0)
        {
            timer_flashWhite -= Time.deltaTime;
        }
        else
        {
            myEnemy.GetComponentInChildren<SkinnedMeshRenderer>().material = myEnemy.ogMat;
        }
    }

    public override void LeaveState(Enemy myEnemy)
    {
        Debug.Log(myEnemy.GetComponentInChildren<SkinnedMeshRenderer>().material);
        myEnemy.GetComponentInChildren<SkinnedMeshRenderer>().material = myEnemy.ogMat;
        myEnemy.myTrigger.myMR.enabled = true;
        myEnemy.ghostRider.enabled = true;
        //myEnemy.hittedStates.text = "";
    }
    private void FlashWhite(Enemy myEnemy)
    {
        myEnemy.GetComponentInChildren<SkinnedMeshRenderer>().material = myEnemy.flashWhite;
    }
}
