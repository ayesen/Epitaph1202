using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ObjectInspectorManagerScript : MonoBehaviour
{
    static public ObjectInspectorManagerScript me;
    public TMP_Text objectDes_ui_cht;
    public TMP_Text objectDes_ui_eng;
	public GameObject imageDisplayer;
	public GameObject imageBG;
	public GameObject optionPrefab;
	public GameObject blurMask;
	public GameObject optionSelection;
	private bool textShowing = false;
	public GameObject canvasUI;
	public GameObject canvasDialogue;
	private int index = 0;
	public List<DialogueStruct> dialogueToShow;
	private bool restrictMovement; // does this dialogue make player stand still and do nothing?
	private bool burnAfterReading; // is this a one time only
	private DialogueScript dT; // the dialogue trigger currently being used

	// for options
	public List<GameObject> options;
	private bool optionsDisplaying = false;
	public float optionSpacing;
	private int optionIndex = 0;

	// for auto display text
	private bool autoAdvance;
	private float timer;

	// for customizable end action
	private GameObject doer;
	private string funcToCall;

	// for audio
	private AudioSource aS;
	public float advanceOffset;

	// for axis works only once
	private bool axisInUse;

	private void Awake()
	{
		me = this;
		objectDes_ui_cht.text = "";
		objectDes_ui_eng.text = "";
		axisInUse = false;
	}

	private void Start()
	{
		aS = GetComponent<AudioSource>();
	}

	public void ShowText(DialogueScript ds)
	{
		dT = ds;
		dialogueToShow = ds.texts;
		restrictMovement = ds.restrictMovement;
		autoAdvance = ds.autoAdvance;
		burnAfterReading = ds.oneTimeDialogue;
		doer = ds.actor;
		funcToCall = ds.funcToCall;
		canvasUI.SetActive(false);
		objectDes_ui_cht.gameObject.SetActive(true);
		objectDes_ui_eng.gameObject.SetActive(true);
		objectDes_ui_cht.text = dialogueToShow[index].description_cht;
		objectDes_ui_eng.text = dialogueToShow[index].description_eng;
		if (restrictMovement) // if this dialogue prohibit player from moving when reading
		{
			PlayerScript.me.GetComponentInChildren<Animator>().Play("readingText");
		}
		StartCoroutine(SetTextShowingToTrue());
		
		// show options
		if (dialogueToShow[index].options.Count > 0) // if there are options for this line
		{
			ShowOptions();
		}

		// show image
		if (ds.image != null)
		{
			imageBG.SetActive(true);
			blurMask.SetActive(true);
			imageDisplayer.SetActive(true);
			imageDisplayer.GetComponent<Image>().sprite = ds.image;
		}

		timer = dialogueToShow[index].time + advanceOffset;

		// play audio
		if (dialogueToShow[index].clip != null)
		{
			aS.clip = dialogueToShow[index].clip;
			aS.Play();
		}
	}

	private void Update()
	{
		if (textShowing)
		{
			if (autoAdvance) // if the dialogue is dispalyed automatically
			{
				if (timer > 0)
				{
					timer -= Time.deltaTime;
					objectDes_ui_cht.text = dialogueToShow[index].description_cht;
					objectDes_ui_eng.text = dialogueToShow[index].description_eng;
				}
				else
				{
					imageBG.SetActive(false);
					blurMask.SetActive(false);
					imageDisplayer.SetActive(false);
					//! LogManager.LOGManager.CoverSetActive(dialogueToShow[index].logX, dialogueToShow[index].logY); comment back when log is in
					if (index < dialogueToShow.Count - 1)
					{
						index++;
						objectDes_ui_cht.text = dialogueToShow[index].description_cht;
						objectDes_ui_eng.text = dialogueToShow[index].description_eng;
						timer = dialogueToShow[index].time + advanceOffset;
						// play audio
						if (dialogueToShow[index].clip != null)
						{
							aS.clip = dialogueToShow[index].clip;
							aS.Play();
						}
					}
					else // when the dialogue ends
					{
						index = 0;
						canvasUI.SetActive(true);
						objectDes_ui_cht.text = "";
						objectDes_ui_eng.text = "";
						textShowing = false;
						if (restrictMovement)
						{
							PlayerScriptNew.me.GetComponentInChildren<Animator>().Play("testIdle");
						}
						imageDisplayer.SetActive(false);
						imageBG.SetActive(false);
						blurMask.SetActive(false);
						if (doer != null)
						{
							doer.SendMessage(funcToCall);
						}
						if (burnAfterReading)
						{
							Destroy(dT.gameObject);
						}
					}
				}
			}
			else // if the dialogue requires player to press a button to proceed
			{
				if ((Input.GetKeyUp(KeyCode.E) || Input.GetButtonUp("AButton")) && !optionsDisplaying) // if no options being displayed, loop through text
				{
					if (index < dialogueToShow.Count - 1)
					{
						imageBG.SetActive(false);
						blurMask.SetActive(false);
						imageDisplayer.SetActive(false);
						LogManager.LOGManager.CoverSetActive(dialogueToShow[index].logX, dialogueToShow[index].logY);
						index++;
						objectDes_ui_cht.text = dialogueToShow[index].description_cht;
						objectDes_ui_eng.text = dialogueToShow[index].description_eng;
						if (dialogueToShow[index].options.Count > 0) // if there are options after this line
						{
							ShowOptions();
						}
						// play audio
						if (dialogueToShow[index].clip != null)
						{
							aS.clip = dialogueToShow[index].clip;
							aS.Play();
						}
						// roll back
						if (dialogueToShow[index].rollBack)
						{
							dialogueToShow.Insert(index + 1, dialogueToShow[dialogueToShow[index].rollBack_index]);
						}
					}
					else // when the dialogue ends
					{
						index = 0;
						canvasUI.SetActive(true);
						objectDes_ui_cht.text = "";
						objectDes_ui_eng.text = "";
						textShowing = false;
						if (restrictMovement)
						{
							PlayerScriptNew.me.GetComponentInChildren<Animator>().Play("testIdle");
						}
						imageDisplayer.SetActive(false);
						imageBG.SetActive(false);
						blurMask.SetActive(false);
						if (doer != null)
						{
							doer.SendMessage(funcToCall);
						}
						if (burnAfterReading)
						{
							Destroy(dT.gameObject);
						}
					}
				}
				if (optionsDisplaying) // let player choose
				{
					optionSelection.SetActive(true);
					RectTransform rt = optionSelection.GetComponent<RectTransform>();
					rt.position = new Vector3(options[optionIndex].GetComponent<RectTransform>().position.x, options[optionIndex].GetComponent<RectTransform>().position.y + 50f, rt.position.z);
					if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetAxis("VerticalArrow") < 0)
					{
                        if (!axisInUse)
                        {
							if (optionIndex < options.Count - 1)
							{
								optionIndex++;
							}
							else
							{
								optionIndex = 0;
							}
							axisInUse = true;
						}
					}
					else if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetAxis("VerticalArrow") > 0)
					{
                        if (!axisInUse)
                        {
							if (optionIndex > 0)
							{
								optionIndex--;
							}
							else
							{
								optionIndex = options.Count - 1;
							}
						}
						axisInUse = true;
					}
					else if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("AButton"))
					{
						for (int i = dialogueToShow[index].options[optionIndex].dialogues.Count - 1; i >= 0 ; i--)
						{
							dialogueToShow.Insert(index + 1, dialogueToShow[index].options[optionIndex].dialogues[i]);
						}
						// foreach (var dialogue in dialogueToShow[index].options[optionIndex].dialogues)
						// {
						// 	dialogueToShow.Insert(index+1, dialogue);               
						// }
						optionSelection.SetActive(false);
						foreach (var option in options)
						{
							Destroy(option.gameObject);
						}
						options.Clear();
						optionsDisplaying = false;
						// play audio
						if (dialogueToShow[index].clip != null)
						{
							aS.clip = dialogueToShow[index].clip;
							aS.Play();
						}
					}
					else if (Input.GetAxis("VerticalArrow") == 0)
                    {
						axisInUse = false;
                    }
				}
			}
		}
	}

	IEnumerator SetTextShowingToTrue()
	{
		yield return new WaitForSeconds(0.1f);
		textShowing = true;
	}

	private void ShowOptions()
	{
		DialogueStruct thisLine = dialogueToShow[index];
		foreach (var option in thisLine.options)
		{
			GameObject optn = Instantiate(optionPrefab, canvasDialogue.transform);
			//optn.transform.parent = optionSelection.transform;
			options.Add(optn);
			optn.GetComponentInChildren<TMP_Text>().text = option.optionContent;
			optionsDisplaying = true;
		}
		// space out options
		int amountOnOneSide = options.Count / 2;
		if (options.Count % 2 == 0) // if even number of options
		{
			for (int i = 0; i < options.Count; i++)
			{
				RectTransform rt = options[i].GetComponent<RectTransform>();
				if (i < amountOnOneSide) // place these half on top
				{
					float targetY = options[i].GetComponent<RectTransform>().position.y + ((amountOnOneSide - i) * optionSpacing - optionSpacing / 2);
					rt.position = new Vector3(rt.position.x, targetY, rt.position.z);
				}
				else // place these half below
				{
					float targetY = options[i].GetComponent<RectTransform>().position.y - ((i + 1 - amountOnOneSide) * optionSpacing - optionSpacing / 2);
					rt.position = new Vector3(rt.position.x, targetY, rt.position.z);
				}
			}
		}
		else // if uneven number of options
		{
			for (int i = 0; i < options.Count; i++)
			{
				RectTransform rt = options[i].GetComponent<RectTransform>();
				if (i < amountOnOneSide) // place these half on top
				{
					float targetY = options[i].GetComponent<RectTransform>().position.y + (amountOnOneSide - i) * optionSpacing;
					rt.position = new Vector3(rt.position.x, targetY, rt.position.z);
				}
				else if (i == amountOnOneSide) // doesn't move this one, it's in the middle
				{

				}
				else // place these half below
				{
					float targetY = options[i].GetComponent<RectTransform>().position.y - (i - amountOnOneSide) * optionSpacing;
					rt.position = new Vector3(rt.position.x, targetY, rt.position.z);
				}
			}
		}
	}
}
