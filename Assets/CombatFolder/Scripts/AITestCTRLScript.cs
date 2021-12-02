using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITestCTRLScript : MonoBehaviour
{
    public Transform goal;
	public Animator playerAnim;
	NavMeshAgent agent;
	Enemy eS;
	public GameObject dropMeterUI;

	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		eS = GetComponent<Enemy>();
	}

	private void Update()
	{
		dropMeterUI.GetComponent<TextMesh>().text = eS.breakMeter.ToString();
		if (eS.GetComponent<NavMeshAgent>().enabled)
		{
			if (eS.walkable)
			{
				agent.isStopped = false;
				agent.destination = goal.position;
			}
			else
			{
				agent.isStopped = true;
			}
		}
		if (!GetComponent<Rigidbody>().isKinematic)
		{
			if (GetComponent<Rigidbody>().velocity.magnitude < 0.01f &&
				!playerAnim.GetCurrentAnimatorStateInfo(0).IsTag("windup"))
			{
				GetComponent<NavMeshAgent>().enabled = true;
				GetComponent<Rigidbody>().isKinematic = true;
			}
		}
	}
}
