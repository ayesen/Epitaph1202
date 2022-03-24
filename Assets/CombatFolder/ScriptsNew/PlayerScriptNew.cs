using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Animations;

public class PlayerScriptNew : MonoBehaviour
{
	public static PlayerScriptNew me;
	[Header("Basics")]
	public int hp;
	public float spd;
	public float rot_spd;
	private GameObject enemy;
	[HideInInspector]
    public Animator anim;
    public GameObject playerModel;
    public float deathTime;
	public bool dead = false;
	[Header("Mat")]
	public List<GameObject> selectedMats; // mats activated
	public List<GameObject> matSlots; // inventory
	public bool atkButtonPressed = false; // for checking if player has already pressed the button
	[Header("Temp UI")]
	public TextMeshProUGUI mat1;
	public TextMeshProUGUI mat2;
	public TextMeshProUGUI mat3;
	public TextMeshProUGUI mat4;
	[Header("Walking Animation")]
	private Vector3 walkingDir;
	public bool walking;
	private bool forwarding;
	private bool backwarding;
	private bool lefting;
	private bool righting;
	[Header("Joystick Controll")]
	public float joystickSensitivity;//0~1

	// backswing cancel
	private GameObject lastMat;

	//Do once after death
	private bool checkBoolChange;

	private void Awake()
	{
		me = this;
		walkingDir = new Vector3();
	}

	private void Start()
    {
		checkBoolChange = dead;
        anim = playerModel.GetComponent<Animator>();
		//enemy = GameObject.FindGameObjectWithTag("Enemy");
		enemy = EffectStorage.me.mainEnemyOfThisLevel;
    }

	private void Update()
	{
        if (Input.GetKeyDown(KeyCode.K) && SafehouseManager.Me.isCheatOn)
        {
			hp = 0;
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
			RecovMatCD(2);
        }
		//print("selected Mats count: " + selectedMats.Count);
		//print("walking: " + walking);
		//print("current clip: " + anim.GetCurrentAnimatorStateInfo(0).fullPathHash);
		//print("atkButtonPressed: " + atkButtonPressed);
		Death();
		if (!dead && !SafehouseManager.Me.isSafehouse)
		{
			
			#region Temp UI
			//if (selectedMats.Contains(matSlots[0]))
			//{
			//	mat1.text = matSlots[0].name + ": " + matSlots[0].GetComponent<MatScriptNew>().amount;
			//}
			//else
			//{
			//	mat1.text = "mat 1 not selected";
			//}
			//if (selectedMats.Contains(matSlots[1]))
			//{
			//	mat2.text = matSlots[1].name + ": " + matSlots[1].GetComponent<MatScriptNew>().amount;
			//}
			//else
			//{
			//	mat2.text = "mat 2 not selected";
			//}
			//if (selectedMats.Contains(matSlots[2]))
			//{
			//	mat3.text = matSlots[2].name + ": " + matSlots[2].GetComponent<MatScriptNew>().amount;
			//}
			//else
			//{
			//	mat3.text = "mat 3 not selected";
			//}
			//if (selectedMats.Contains(matSlots[3]))
			//{
			//	mat4.text = matSlots[3].name + ": " + matSlots[3].GetComponent<MatScriptNew>().amount;
			//}
			//else
			//{
			//	mat4.text = "mat 4 not selected";
			//}
			#endregion
			#region activate and deactivate mats
			// activate mats
			if (!anim.GetCurrentAnimatorStateInfo(0).IsName("testWindup") &&
				!anim.GetCurrentAnimatorStateInfo(0).IsName("testATK") &&
                !anim.GetCurrentAnimatorStateInfo(0).IsName("testBackswing") &&
				!anim.GetCurrentAnimatorStateInfo(0).IsName("readingText"))
            {
				if (Input.GetKeyUp(KeyCode.Alpha1) || Input.GetButtonUp("XButton") && matSlots[0] != null)
				{
					SoundMan.SoundManager.MaterialSelect();
					if (selectedMats.Contains(matSlots[0]))
					{
						selectedMats.Remove(matSlots[0]);
					}
					else
					{
						if (matSlots[0].GetComponent<MatScriptNew>().amount > 0 && selectedMats.Count < 2)
						{
							selectedMats.Add(matSlots[0]);
							anim.SetTrigger("selected");
						}
					}
					EffectManagerNew.me.RefreshCurrentMats();
				}
				if (Input.GetKeyUp(KeyCode.Alpha2) || Input.GetButtonUp("YButton") && matSlots[1] != null)
				{
					SoundMan.SoundManager.MaterialSelect();
					if (selectedMats.Contains(matSlots[1]))
					{
						selectedMats.Remove(matSlots[1]);
					}
					else
					{
						if (matSlots[1].GetComponent<MatScriptNew>().amount > 0 && selectedMats.Count < 2)
						{
							selectedMats.Add(matSlots[1]);
							anim.SetTrigger("selected");
						}
					}
					EffectManagerNew.me.RefreshCurrentMats();
				}
				if (Input.GetKeyUp(KeyCode.Alpha3) || Input.GetButtonUp("BButton") && matSlots[2] != null)
				{
					SoundMan.SoundManager.MaterialSelect();
					if (selectedMats.Contains(matSlots[2]))
					{
						selectedMats.Remove(matSlots[2]);
					}
					else
					{
						if (matSlots[2].GetComponent<MatScriptNew>().amount > 0 && selectedMats.Count < 2)
						{
							anim.SetTrigger("selected");
							selectedMats.Add(matSlots[2]);
						}
					}
					EffectManagerNew.me.RefreshCurrentMats();
				}
				if (Input.GetKeyUp(KeyCode.Alpha4) || Input.GetButtonUp("AButton") && matSlots[3] != null)
				{
					SoundMan.SoundManager.MaterialSelect();
					if (selectedMats.Contains(matSlots[3]))
					{
						selectedMats.Remove(matSlots[3]);
					}
					else
					{
						if (matSlots[3].GetComponent<MatScriptNew>().amount > 0 && selectedMats.Count < 2)
						{
							selectedMats.Add(matSlots[3]);
						}
					}
					EffectManagerNew.me.RefreshCurrentMats();
				}
			}

			#endregion
			#region controller movement
			/*
			//Move
			if(Mathf.Abs(Input.GetAxis("LeftJoystickHorizontal")) >= joystickSensitivity ||
				Mathf.Abs(Input.GetAxis("LeftJoystickVertical")) >= joystickSensitivity ||
				Mathf.Sqrt(Mathf.Pow(Input.GetAxis("LeftJoystickHorizontal"), 2) + Mathf.Pow(Input.GetAxis("LeftJoystickVertical"), 2)) >= joystickSensitivity)
            {
				if(!anim.GetCurrentAnimatorStateInfo(0).IsName("testWindup") &&
				!anim.GetCurrentAnimatorStateInfo(0).IsName("testATK") &&
				!anim.GetCurrentAnimatorStateInfo(0).IsName("testBackswing") &&
				!anim.GetCurrentAnimatorStateInfo(0).IsName("readingText") &&
				atkButtonPressed == false)
                {
					walking = true;
                }
            }
            if (walking)
            {
				if (Mathf.Abs(Input.GetAxis("LeftJoystickHorizontal")) >= joystickSensitivity ||
					Mathf.Abs(Input.GetAxis("LeftJoystickVertical")) >= joystickSensitivity ||
					Mathf.Sqrt(Mathf.Pow(Input.GetAxis("LeftJoystickHorizontal"), 2) + Mathf.Pow(Input.GetAxis("LeftJoystickVertical"), 2)) >= joystickSensitivity)
				{
					transform.position = new Vector3(transform.position.x + Input.GetAxis("LeftJoystickHorizontal") * spd * Time.deltaTime, transform.position.y,
					transform.position.z + Input.GetAxis("LeftJoystickVertical") * -1 * spd * Time.deltaTime);
					walkingDir = new Vector3(Input.GetAxis("LeftJoystickHorizontal"), 0, Input.GetAxis("LeftJoystickVertical") * -1);
				}
                else
                {
					walking = false;
					forwarding = false;
					backwarding = false;
					lefting = false;
					righting = false;
					anim.CrossFade("testIdle", .3f);
					walkingDir = Vector3.zero;
				}
            }
			//Rotate
			if(Input.GetAxis("LT") > 0)
            {
				var target = new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z);
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target - transform.position), rot_spd * Time.deltaTime);
			}
			else if(Mathf.Abs(Input.GetAxis("RightJoystickHorizontal")) >= joystickSensitivity ||
				Mathf.Abs(Input.GetAxis("RightJoystickVertical")) >= joystickSensitivity ||
				Mathf.Sqrt(Mathf.Pow(Input.GetAxis("RightJoystickHorizontal"), 2) + Mathf.Pow(Input.GetAxis("RightJoystickHorizontal"), 2)) >= joystickSensitivity &&
				Input.GetAxis("LT") == 0)
            {
				var target = new Vector3(Input.GetAxis("RightJoystickHorizontal"), 0, Input.GetAxis("RightJoystickVertical") * -1);
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target), rot_spd * Time.deltaTime);
			}
			*/
            #endregion
            #region movement
            if (((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) ||
				Mathf.Abs(Input.GetAxis("LeftJoystickHorizontal")) >= joystickSensitivity ||
				Mathf.Abs(Input.GetAxis("LeftJoystickVertical")) >= joystickSensitivity ||
				Mathf.Sqrt(Mathf.Pow(Input.GetAxis("LeftJoystickHorizontal"), 2) + Mathf.Pow(Input.GetAxis("LeftJoystickVertical"), 2)) >= joystickSensitivity) &&
				!anim.GetCurrentAnimatorStateInfo(0).IsName("testWindup") &&
				!anim.GetCurrentAnimatorStateInfo(0).IsName("testATK") &&
				!anim.GetCurrentAnimatorStateInfo(0).IsName("testBackswing") &&
				!anim.GetCurrentAnimatorStateInfo(0).IsName("readingText"))
				//anim.GetCurrentAnimatorStateInfo(0).IsName("testIdle")) // if in walk state, walk
			{
				walking = true;
				atkButtonPressed = false;
			}

			if (walking && !anim.GetCurrentAnimatorStateInfo(0).IsName("readingText"))
			{
				// walking diagonally
				if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
				{
					transform.position = new Vector3(transform.position.x - Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime, transform.position.y, transform.position.z + Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime);
					walkingDir = new Vector3(-Mathf.Sqrt(Mathf.Pow(spd, 2) / 2), 0, Mathf.Sqrt(Mathf.Pow(spd, 2) / 2));
				}
				else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
				{
					transform.position = new Vector3(transform.position.x + Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime, transform.position.y, transform.position.z + Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime);
					walkingDir = new Vector3(Mathf.Sqrt(Mathf.Pow(spd, 2) / 2), 0, Mathf.Sqrt(Mathf.Pow(spd, 2) / 2));
				}
				else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
				{
					transform.position = new Vector3(transform.position.x - Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime, transform.position.y, transform.position.z - Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime);
					walkingDir = new Vector3(-Mathf.Sqrt(Mathf.Pow(spd, 2) / 2), 0, -Mathf.Sqrt(Mathf.Pow(spd, 2) / 2));
				}
				else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
				{
					transform.position = new Vector3(transform.position.x + Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime, transform.position.y, transform.position.z - Mathf.Sqrt(Mathf.Pow(spd, 2) / 2) * Time.deltaTime);
					walkingDir = new Vector3(Mathf.Sqrt(Mathf.Pow(spd, 2) / 2), 0, -Mathf.Sqrt(Mathf.Pow(spd, 2) / 2));
				}
				// walking in one axis
				else if (Input.GetKey(KeyCode.W))
				{
					transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + spd * Time.deltaTime);
					walkingDir = new Vector3(0, 0, spd);
				}
				else if (Input.GetKey(KeyCode.S))
				{
					transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - spd * Time.deltaTime);
					walkingDir = new Vector3(0, 0, -spd);
				}
				else if (Input.GetKey(KeyCode.A))
				{
					transform.position = new Vector3(transform.position.x - spd * Time.deltaTime, transform.position.y, transform.position.z);
					walkingDir = new Vector3(-spd, 0, 0);
				}
				else if (Input.GetKey(KeyCode.D))
				{
					transform.position = new Vector3(transform.position.x + spd * Time.deltaTime, transform.position.y, transform.position.z);
					walkingDir = new Vector3(spd, 0, 0);
				}
				else if(Mathf.Abs(Input.GetAxis("LeftJoystickHorizontal")) >= joystickSensitivity ||
					Mathf.Abs(Input.GetAxis("LeftJoystickVertical")) >= joystickSensitivity ||
					Mathf.Sqrt(Mathf.Pow(Input.GetAxis("LeftJoystickHorizontal"), 2) + Mathf.Pow(Input.GetAxis("LeftJoystickVertical"), 2)) >= joystickSensitivity)
				{
					transform.position = new Vector3(transform.position.x + Input.GetAxis("LeftJoystickHorizontal") * spd * Time.deltaTime, transform.position.y,
					transform.position.z + Input.GetAxis("LeftJoystickVertical") * -1 * spd * Time.deltaTime);
					walkingDir = new Vector3(Input.GetAxis("LeftJoystickHorizontal"), 0, Input.GetAxis("LeftJoystickVertical") * -1);
				}
				else
				{
					walkingDir = new Vector3(0, 0, 0);
					forwarding = false;
					backwarding = false;
					lefting = false;
					righting = false;
					//anim.CrossFade("testIdle", .3f);
					anim.Play("testIdle");
					walking = false;
				}

				if (walkingDir.magnitude > 0)
				{
					if (Vector3.Angle(walkingDir, transform.forward) < 45)
					{
						//anim.Play("testWalk");
						if (!forwarding)
						{
							//anim.CrossFade("testWalk", .3f);
							anim.Play("testWalk");
							forwarding = true;
							backwarding = false;
							lefting = false;
							righting = false;
						}
					}
					else if (Vector3.Angle(walkingDir, transform.forward) > 135)
					{
						if (!backwarding)
						{
							//anim.CrossFade("Player_Walking_Backwards", .3f);
							anim.Play("Player_Walking_Backwards");
							forwarding = false;
							backwarding = true;
							lefting = false;
							righting = false;
						}
					}
					else if (Vector3.Angle(walkingDir, transform.forward) > 45 && Vector3.Angle(walkingDir, transform.forward) < 135)
					{
						Vector3 cross = Vector3.Cross(transform.forward, walkingDir);
						if(cross.y > 0)
                        {
							if (!righting)
							{
								//anim.CrossFade("Player_Walking_Right", .3f);
								anim.Play("Player_Walking_Right");
								forwarding = false;
								backwarding = false;
								lefting = false;
								righting = true;
							}
						}
						else if(cross.y < 0)
                        {
							if (!lefting)
							{
								//anim.CrossFade("Player_Walking_Left", .3f);
								anim.Play("Player_Walking_Left");
								forwarding = false;
								backwarding = false;
								lefting = true;
								righting = false;
							}
						}
						/*
						if (transform.forward.z < 0 || transform.forward.x < 0)
						{
							if (walkingDir.x > 0 || walkingDir.z < 0)
							{
								//anim.Play("Player_Walking_Left");
								if (!lefting)
								{
									anim.CrossFade("Player_Walking_Left", .3f);
									forwarding = false;
									backwarding = false;
									lefting = true;
									righting = false;
								}
							}
							else if (walkingDir.x < 0 || walkingDir.z > 0)
							{
								if (!righting)
								{
									anim.CrossFade("Player_Walking_Right", .3f);
									forwarding = false;
									backwarding = false;
									lefting = false;
									righting = true;
								}
							}
						}
						*/
						else if (transform.forward.z > 0 || transform.forward.x > 0)
						{
							if (walkingDir.x > 0 || walkingDir.z < 0)
							{
								if (!righting)
								{
									anim.CrossFade("Player_Walking_Right", .3f);
									forwarding = false;
									backwarding = false;
									lefting = false;
									righting = true;
								}
							}
							else if (walkingDir.x < 0 || walkingDir.z > 0)
							{
								if (!lefting)
								{
									anim.CrossFade("Player_Walking_Left", .3f);
									forwarding = false;
									backwarding = false;
									lefting = true;
									righting = false;
								}
							}
						}
					}
					
				}
			}
			Aim_and_LockOn();

			/* //mouse rotation
			else if (!anim.GetCurrentAnimatorStateInfo(0).IsName("readingText"))
			{
				var target = new Vector3(MouseManager.me.mousePos.x, transform.position.y, MouseManager.me.mousePos.z);
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target - transform.position), rot_spd * Time.deltaTime);
			}
			*/
			#endregion

			// check for attack button press
			if (selectedMats.Count > 0 &&  // check if player has mat activated
				(anim.GetCurrentAnimatorStateInfo(0).IsName("testIdle") || // if player in idle state
				walking ||
				anim.GetCurrentAnimatorStateInfo(0).IsName("testWalk") ||
				anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Walking_Backwards") ||
				anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Walking_Left") ||
				anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Walking_Right") ||  // if player in walk state
				anim.GetCurrentAnimatorStateInfo(0).IsName("selectMat"))) // if player in select mat state
			{
				if (Input.GetMouseButtonUp(0) || Input.GetAxis("RT") > 0 && !atkButtonPressed) // if left click
				{
					atkButtonPressed = true;
					walking = false;
					bool goodToGo = true;
					// check if player has any mat left
					foreach (var mat in selectedMats)
					{
						if (mat.GetComponent<MatScriptNew>().amount <= 0)
						{
							goodToGo = false;
						}
					}
					if (goodToGo) // there is enough mat
					{
						foreach (var mat in selectedMats)
						{
							mat.GetComponent<MatScriptNew>().amount--;
						}

						if (matSlots[3] != null && matSlots[3].GetComponent<MatScriptNew>().amount <= 0)//Detect boss mat and delete if used
						{
							if (selectedMats.Contains(matSlots[3]))
								selectedMats.Remove(matSlots[3]);
							matSlots[3] = null;
							UIManager.Me.UI_ChangeIcon();
						}
						anim.Play("testWindup"); // player anticipation clip and call effect manager's casting event in clip
						// anim.CrossFade("testWindup", 0.1f);
						
						selectedMats.Clear();
					}
					else
					{
						print("YOU DON'T HAVE ENOUGH MATERIALS!!!");
					}
				}
			}
			if (Input.GetMouseButtonUp(0) || Input.GetAxis("RT") > 0)
			{
				if (selectedMats.Count > 0 &&  // check if player has mat activated
				(anim.GetCurrentAnimatorStateInfo(0).IsName("testIdle") || // if player in idle state
				walking))
				{

				}
				else
				{
					//print(walking);
				}
			}
		}
    }
	public void RecovMatCD(int breakAmp)
    {
		for (int i = 0; i < 3; i++)
        {
			if (matSlots[i] != null)
			{
				if (matSlots[i].GetComponent<MatScriptNew>().amount < matSlots[i].GetComponent<MatScriptNew>().amount_max)
				{
					matSlots[i].GetComponent<MatScriptNew>().CD -= breakAmp;
					//print(matSlots[i].ToString()+": "+breakAmp);
					//Debug.Log("[PlayerScriptNew]RemoveMatCD: " + matSlots[i].ToString() + matSlots[i].GetComponent<MatScriptNew>().CD);
				}
			}
		}
	}

	public void LoseHealth_player(int amt)
	{
		hp -= amt;
		SoundMan.SoundManager.PlayerHitten();
	}
	public void Death()
	{
		if (hp <= 0)
		{
			dead = true;
		}
		else
		{
			dead = false;
		}
		if (dead != checkBoolChange && dead)
		{
			checkBoolChange = dead;
			anim.Play("Death");
			StartCoroutine(WaitSecondsAndDie(deathTime));
		}
		else if (dead != checkBoolChange && !dead)
		{
			checkBoolChange = dead;
		}
	}

	IEnumerator WaitSecondsAndDie(float deathTime)
	{
		yield return new WaitForSecondsRealtime(deathTime);
		PostProcessingManager.Me.StartCoroutine(PostProcessingManager.Me.DeadFilter());
	}

	private void Aim_and_LockOn()
	{
		// lock on
		if (!anim.GetCurrentAnimatorStateInfo(0).IsName(("readingText")))
		{
			if ((Input.GetMouseButton(1) || Input.GetAxis("LT") > 0) && LockOnManager.me.bears_canBeLockedOn.Count > 0)
			{
				var target = new Vector3(LockOnManager.me.bears_canBeLockedOn[0].transform.position.x, transform.position.y, LockOnManager.me.bears_canBeLockedOn[0].transform.position.z);
				print("currently locked onto: " + LockOnManager.me.bears_canBeLockedOn[0].name);
				
				
				// change target
				if (Input.GetAxis("RightJoystickHorizontal") >= joystickSensitivity)
				{
					GameObject newTarget = LockOnManager.me.GetClosest_right();
					target = new Vector3(newTarget.transform.position.x, transform.position.y, newTarget.transform.position.z);
				}
				else if (Input.GetAxis("RightJoystickHorizontal") <= -joystickSensitivity)
				{
					GameObject newTarget = LockOnManager.me.GetClosest_left();
					target = new Vector3(newTarget.transform.position.x, transform.position.y, newTarget.transform.position.z);
				}
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target - transform.position), rot_spd * Time.deltaTime);
			}
			// look at mouse pos(not changing y-axis)
			//! if this doesn't work properly, check game objects' layers, and make sure the mouse manager ignores the proper layers
			else if (Mathf.Abs(Input.GetAxis("RightJoystickHorizontal")) >= joystickSensitivity ||
					 Mathf.Abs(Input.GetAxis("RightJoystickVertical")) >= joystickSensitivity ||
					 Mathf.Sqrt(Mathf.Pow(Input.GetAxis("RightJoystickHorizontal"), 2) + Mathf.Pow(Input.GetAxis("RightJoystickHorizontal"), 2)) >= joystickSensitivity &&
					 Input.GetAxis("LT") == 0)
			{
				var target = new Vector3(Input.GetAxis("RightJoystickHorizontal"), 0, Input.GetAxis("RightJoystickVertical") * -1);
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target), rot_spd * Time.deltaTime);
			}
		}
	}
}
