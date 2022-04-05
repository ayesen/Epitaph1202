using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class ShootAimTutorScript : MonoBehaviour
{
	public GameObject tutorBear;

	// state machine
	public int tut_state = 0;
	private int state_select = 1;
	private int state_aim = 2;
	private int state_shoot = 3;
	private int state_finished = 4;
	public float timer;
	private float timer_default;

	// tutor bear monitor
	public AIStateBase tutBear_currentState;

	// open door
	public List<DoorScript> doorToOpen;

	// dialogues
	public GameObject dialg_select;
	public GameObject dialg_selectRP;
	public GameObject dialg_aim;
	public GameObject dialg_aimRP;
	//public GameObject dialg_shoot;
	//public GameObject dialg_shootRP;
	public GameObject dialg_finished;
	public GameObject Corridor_Trigger;

	private Coroutine cur_coroutine;

	// mats check
	//public List<GameObject> combination;
	public GameObject match;
    //public GameObject nail;
    //public GameObject cotton;

    private void Awake()
    {
		timer_default = timer;
    }

    private void OnTriggerEnter(Collider other)
	{
		// 激活小熊，瞄准教学
		if (other.gameObject.CompareTag("Player") && tut_state == 0)
		{
			// face bear
			StartCoroutine(FaceBear());
			// cancel select
			PlayerScriptNew.me.selectedMats.Clear();
			// set state
			tut_state = state_select;
			// enable small bear
			// tutorBear.GetComponent<SmallBear>().enabled = true;
			// tutorBear.GetComponent<AIController>().enabled = true;
			dialg_select.GetComponent<DialogueScript>().enabled = true; // enable dialogue script, show dialogue
			BGMMan.bGMManger.FirstTinyTeddyMusic();
			if (cur_coroutine == null)
				cur_coroutine =  StartCoroutine(RepeatDialogue());
		}
	}

	private void Update()
	{
		TutStateMachine();
		if (tut_state != 0 && tut_state != state_finished)
		{
			// keep player's mat amount above 0
			PlayerScriptNew.me.matSlots[0].GetComponent<MatScriptNew>().amount = Mathf.Clamp(PlayerScriptNew.me.matSlots[0].GetComponent<MatScriptNew>().amount, 1, int.MaxValue);
			// keep bear hp above 0 until should be killed
			tutorBear.GetComponent<SmallBear>().health = Mathf.Clamp(tutorBear.GetComponent<SmallBear>().health, 30, int.MaxValue);
		}
	}

	private void TutStateMachine()
	{
		if ((Input.GetButtonUp("XButton") || Input.GetKeyUp(KeyCode.Alpha1)) &&
			tut_state == state_select)
		{
			timer = timer_default;
			tut_state = state_aim;
			dialg_aim.GetComponent<DialogueScript>().enabled = true;
		}
		else if ((Input.GetAxis("LT") > 0 || Input.GetKeyDown(KeyCode.Mouse1)) &&
			tut_state == state_aim)
		{
			timer = timer_default;
			StopAllCoroutines();
			dialg_finished.GetComponent<DialogueScript>().enabled = true;
			tut_state = state_finished;
			if (!Corridor_Trigger.activeSelf)
				Corridor_Trigger.SetActive(true);

			if (doorToOpen.Count > 0)
			{
				foreach (var door in doorToOpen)
				{
					door.ControllDoor();
				}
			}
			// small bear runs away
			ActivateBear();
			tutorBear.GetComponent<SmallBear>().GoToLoc();
			
			SoundMan.SoundManager.JumpScare();
			tutorBear.GetComponent<SmallBear>().moveSpeed = 8;
			LockOnManager.me.bears_canBeLockedOn.Remove(tutorBear);
			Destroy(tutorBear, 2f);
		}
	}

	private IEnumerator FaceBear()
    {
		float rotateSpeed = 0;
		while(rotateSpeed <= 1)
        {
			Vector3 direction = tutorBear.transform.position - PlayerScriptNew.me.transform.position;
			direction = new Vector3(direction.x, 0, direction.y);
			Quaternion toRotation = Quaternion.LookRotation(direction);
			PlayerScriptNew.me.transform.rotation = Quaternion.Lerp(PlayerScriptNew.me.transform.rotation, toRotation, rotateSpeed);
			rotateSpeed += 0.3f * Time.deltaTime;
			yield return new WaitForSeconds(0.005f * Time.deltaTime);
		}
	}

    private IEnumerator RepeatDialogue()
	{
		while (true) 
		{
			yield return null;
			timer -= Time.deltaTime;
			if(timer <= 0)
            {
				if (tut_state == 1)
				{
					GameObject RPDialg = Instantiate(dialg_selectRP, transform);
					RPDialg.GetComponent<DialogueScript>().enabled = true;
				}
				else if (tut_state == 2)
				{
					GameObject RPDialg = Instantiate(dialg_aimRP, transform);
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
}