using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class TutorialManagerScript : MonoBehaviour
{
	public static TutorialManagerScript me;

    public GameObject tutorBear;

	public GameObject init_DialgScript;

	// state machine
    public int tut_state = 0;
	public float timer = 10;
	private int state_poise = 1;
    private int state_cd = 2;
    private int state_dmg = 3;
	private int state_finished = 4;
	private float timer_default = 10;

	// tutor bear monitor
	public AIStateBase tutBear_currentState;

	// open door
	public List<DoorScript> doorToOpen;

	// dialogues
	public GameObject dialg_pd;
	public GameObject dialg_pdRD;
	public GameObject dialg_cd;
	public GameObject dialg_cdRD;
	public GameObject dialg_dmg;
	public GameObject dialg_dmgRD;
	public GameObject dialg_finished;
	
	// mats check
	public List<GameObject> combination;
	public GameObject match;
	public GameObject nail;
	public GameObject cotton;

	private Coroutine cur_coroutine;

	private void Awake()
	{
		me = this;
		timer_default = timer;
	}

	private void OnTriggerEnter(Collider other)
	{
		// 激活小熊，削韧教学
		if (other.gameObject.CompareTag("Player") && tut_state == 0)
		{
			// set state
			tut_state = state_poise;
			// enable small bear
			// tutorBear.GetComponent<SmallBear>().enabled = true;
			// tutorBear.GetComponent<AIController>().enabled = true;
			dialg_pd.GetComponent<DialogueScript>().enabled = true; // enable dialogue script, show dialogue
			if (cur_coroutine == null)
				cur_coroutine = StartCoroutine(RepeatDialogue());
			init_DialgScript.GetComponent<DialogueScript>().enabled = true;
		}
	}

	private void Update()
	{
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
				tutorBear.GetComponent<SmallBear>().health = Mathf.Clamp(tutorBear.GetComponent<SmallBear>().health, 25, int.MaxValue);
			}
		}
	}

	private void TutStateMachine()
	{
		if (combination.Contains(nail) &&
		    combination.Contains(cotton) &&
			tut_state == state_poise)
		{
			timer = timer_default;
			tut_state = state_cd;
			dialg_cd.GetComponent<DialogueScript>().enabled = true;
		}
		else if (tutBear_currentState == tutorBear.GetComponent<AIController>().downedState &&
			tut_state == state_cd &&
			combination.Contains(match) &&
			combination.Contains(cotton))
		{
			timer = timer_default;
			tut_state = state_dmg;
			dialg_dmg.GetComponent<DialogueScript>().enabled = true;
		}
		else if (tut_state == state_dmg &&
			tutorBear.GetComponent<SmallBear>().health <= 0)
		{
			StopAllCoroutines();
			dialg_finished.GetComponent<DialogueScript>().enabled = true;
			tut_state = state_finished;

			if (doorToOpen.Count > 0)
			{
				foreach (var door in doorToOpen)
				{
					door.ControllDoor();
				}
			}
		}

		
	}
	
	private IEnumerator RepeatDialogue()
	{
		while (true)
		{
			yield return null;
			timer -= Time.deltaTime;
			if (timer <= 0)
			{
				if (tut_state == 1)
				{
					GameObject RPDialg = Instantiate(dialg_pdRD, transform);
					RPDialg.GetComponent<DialogueScript>().enabled = true;
				}
				else if (tut_state == 2)
				{
					GameObject RPDialg = Instantiate(dialg_cdRD, transform);
					RPDialg.GetComponent<DialogueScript>().enabled = true;
				}
				else if (tut_state == 3)
				{
					GameObject RPDialg = Instantiate(dialg_dmgRD, transform);
					RPDialg.GetComponent<DialogueScript>().enabled = true;
				}
				timer = timer_default;
			}
		}
	}

	private void ActivateBear()
	{
		// enable small bear
		tutorBear.GetComponent<SmallBear>().enabled = true;
		tutorBear.GetComponent<AIController>().enabled = true;
		tutorBear.GetComponent<EffectHoldersHolderScript>().enabled = true;
	}
	
	public void PassCombination(List<GameObject> comb)
	{
		combination = comb;
	}
}
