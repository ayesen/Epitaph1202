using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueScript : MonoBehaviour
{
    public float triggerRange; // the distance for player to trigger the dialogue
    public bool autoTrigger; // if this dialogue is triggered automatically
	public bool autoAdvance; //! if this dialogue advances automatically or the player needs to press an interation button, dialogues with options should not be auto advance!
	public List<DialogueStruct> texts; // the text to be shown
	public Sprite image; // the image to be shown
	private GameObject player;
	private Material defaultMat;
	public Material highLightMat;
	public bool restrictMovement; // does the player is prohibited from doing anything when reading
	public bool oneTimeDialogue; //! is this dialogue can only be triggered once, dialogues with options should only be one time!
	private bool inspected;
	private MeshRenderer mr;
	public bool isSwitch;
	public GameObject[] interactiveSwitch;

	[Header("Custimizable End Action")]
	public GameObject actor;
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
		if (player != null && Vector3.Distance(player.transform.position, transform.position) < triggerRange && !inspected && oneTimeDialogue)
		{
			if (!autoTrigger) // highlight item, show text after pressing E
			{
				mr.material = highLightMat;
				if (Input.GetKeyUp(KeyCode.E) &&
					player.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("testIdle"))
				{
					inspected = true;
					ObjectInspectorManagerScript.me.ShowText(this);
					foreach (GameObject interactable in interactiveSwitch)
					{
						interactable.SetActive(true);
					}
				}
			}
			else // auto show text
			{
				inspected = true;
				ObjectInspectorManagerScript.me.ShowText(this);
				foreach (GameObject interactable in interactiveSwitch)
				{
					interactable.SetActive(true);
				}
			}
		}
		else
		{
			if (mr != null)
			{
				mr.material = defaultMat;
			}
		}
	}
}
