using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class DialogueScript : MonoBehaviour
{
	public float triggerRange; // the distance for player to trigger the dialogue
	public bool autoTrigger; // if this dialogue is triggered automatically
	public bool autoAdvance; //! if this dialogue advances automatically or the player needs to press an interation button, dialogues with options should not be auto advance!
	public bool areaTrigger; //If this is entering a Mesh Trigger Zone
	public float displayDelayed;
	public List<DialogueStruct> texts; // the text to be shown
	public Sprite image; // the image to be shown
	private GameObject player;
	private Material defaultMat;
	public Material highLightMat;
	public bool restrictMovement; // does the player is prohibited from doing anything when reading
	public bool oneTimeDialogue; //! can this dialogue be triggered only once, dialogues with options should set this to true!
	private bool inspected;
	private MeshRenderer mr;
	public bool isSwitch;
	public GameObject[] interactiveSwitch;
	public int logX;
	public int logY;



	[Header("Custimizable End Action")] public GameObject actor;
	public string funcToCall;

	private void Start()
	{
		if (GetComponent<MeshRenderer>() != null)
		{
			mr = GetComponent<MeshRenderer>();
			defaultMat = mr.material;
		}

		inspected = false;
		player = GameObject.FindGameObjectWithTag("Player");
		foreach (var line in texts)
		{
			if (line.clip != null)
			{
				line.time = line.clip.length;
			}
		}
	}

	private void Update()
	{
		if (player != null && Vector3.Distance(player.transform.position, transform.position) < triggerRange &&
		    (!inspected || !oneTimeDialogue))
		{
			if (!autoTrigger) // highlight item, show text after pressing E
			{
				//mr.material = highLightMat;
				if ((Input.GetKeyUp(KeyCode.E) || Input.GetAxis("HorizontalArrow") > 0) &&
				    (player.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("testIdle") ||
				     player.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("testWalk") ||
				     player.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0)
					     .IsName("Player_Walking_Right") ||
				     player.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0)
					     .IsName("Player_Walking_Left") ||
				     player.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0)
					     .IsName("Player_Walking_Backwards")))
				{
					if (displayDelayed <= 0f)
					{
						SoundMan.SoundManager.ItemInspection();
						inspected = true;
						ObjectInspectorManagerScript.me.ShowText(this);
						foreach (GameObject interactable in interactiveSwitch)
						{
							interactable.SetActive(true);
						}

						//if (LogManager.LOGManager != null)
						//{
						//	LogManager.LOGManager.CoverSetActive(logX, logY);
						//}
					}
				}
			}
			else // auto show text
			{
				if (displayDelayed <= 0f)
				{
					inspected = true;
					ObjectInspectorManagerScript.me.ShowText(this);
					foreach (GameObject interactable in interactiveSwitch)
					{
						interactable.SetActive(true);
					}

					//if (LogManager.LOGManager != null)
					//{
					//	LogManager.LOGManager.CoverSetActive(logX, logY);
					//}
				}
			}
		}
		else
		{
			if (mr != null)
			{
				//mr.material = defaultMat;
			}
		}
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && areaTrigger)
		{
			StartCoroutine(Dialogue()); 
			Debug.Log("Player in Range");
		}
	}

	IEnumerator Dialogue()
	{
		yield return new WaitForSeconds (displayDelayed);
		inspected = true;
		ObjectInspectorManagerScript.me.ShowText(this);
		foreach (GameObject interactable in interactiveSwitch)
		{
			interactable.SetActive(true);
		}

		//LogManager.LOGManager.CoverSetActive(logX, logY);
		//Destroy(this.gameObject);
	}
}

	
