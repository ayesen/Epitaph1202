using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManagerScript : MonoBehaviour
{
	public static TutorialManagerScript me;

    public GameObject tutorBear;

	// state machine
    private int tut_state = 0;
    private int state_poise = 1;
    private int state_cd = 2;
    private int state_dmg = 3;
	private int state_finished = 4;

	// tutor bear monitor
	public AIStateBase tutBear_currentState;

	// open door
	public DoorScript doorToOpen;

	// dialogues
	public GameObject dialg_pd;
	public GameObject dialg_cd;
	public GameObject dialg_dmg;
	public GameObject dialg_finished;

	private void Awake()
	{
		me = this;
	}

	private void OnTriggerEnter(Collider other)
	{
		// 激活小熊，削韧教学
		if (other.gameObject.CompareTag("Player") && tut_state == 0)
		{
			// set state
			tut_state = state_poise;
			// enable small bear
			tutorBear.GetComponent<SmallBear>().enabled = true;
			tutorBear.GetComponent<AIController>().enabled = true;
			dialg_pd.GetComponent<DialogueScript>().enabled = true; // enable dialogue script, show dialogue
		}
	}

	private void Update()
	{
		print(tut_state);
		TutStateMachine();
		if (tut_state != 0 && tut_state != state_finished) 
		{
			// keep player's mat amount above 0
			PlayerScriptNew.me.matSlots[0].GetComponent<MatScriptNew>().amount = Mathf.Clamp(PlayerScriptNew.me.matSlots[0].GetComponent<MatScriptNew>().amount, 1, int.MaxValue);
			PlayerScriptNew.me.matSlots[1].GetComponent<MatScriptNew>().amount = Mathf.Clamp(PlayerScriptNew.me.matSlots[1].GetComponent<MatScriptNew>().amount, 1, int.MaxValue);
			PlayerScriptNew.me.matSlots[2].GetComponent<MatScriptNew>().amount = Mathf.Clamp(PlayerScriptNew.me.matSlots[2].GetComponent<MatScriptNew>().amount, 1, int.MaxValue);
			// keep bear hp above 0 until should be killed
			if (tut_state != state_dmg)
			{
				tutorBear.GetComponent<SmallBear>().health = Mathf.Clamp(tutorBear.GetComponent<SmallBear>().health, 30, int.MaxValue);
			}
		}
	}

	private void TutStateMachine()
	{
		if (tutBear_currentState == tutorBear.GetComponent<AIController>().downedState &&
			tut_state == state_poise)
		{
			tut_state = state_cd;
			dialg_cd.GetComponent<DialogueScript>().enabled = true;
		}
		else if (tutBear_currentState == tutorBear.GetComponent<AIController>().downedState &&
			tut_state == state_cd &&
			PlayerScriptNew.me.matSlots[1].GetComponent<MatScriptNew>().amount == 5)
		{
			tut_state = state_dmg;
			dialg_dmg.GetComponent<DialogueScript>().enabled = true;
		}
		else if (tut_state == state_dmg &&
			tutorBear.GetComponent<SmallBear>().health <= 0)
		{
			dialg_finished.GetComponent<DialogueScript>().enabled = true;
			tut_state = state_finished;
			if (doorToOpen != null)
			{
				doorToOpen.OpenFront();
			}
		}
	}
}
