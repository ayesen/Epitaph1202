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
	public int maxHP;
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
	[Header("Animation Related")]
	private Vector3 walkingDir;
	public bool walking;
	public float flyAmount_death;
	//private bool forwarding;
	//private bool backwarding;
	//private bool lefting;
	//private bool righting;
	[Header("Joystick Control")]
	public float joystickSensitivity;//0~1
	[Header("VFX")]
	public Transform hand;
	// when select
	public GameObject selectVFX_blue; // blue for amp
	public GameObject selectLight_blue;
	public GameObject selectVFX_white; // white for functional
	public GameObject selectLight_white;
	public GameObject selectVFX_yellow; // yellow for atk
	public GameObject selectLight_yellow;
	public GameObject selectVFX_purple; // purple for boss
	public GameObject selectLight_purple;
	// when mat activated
	public GameObject amp_activated_vfx;
	public GameObject atk_activated_vfx;
	public GameObject func_activated_vfx;
	public GameObject boss_activated_vfx;
	[Header("For Lock On")]
	public Vector3 targetPos;
	public GameObject lockedOnto;
	private bool lockOnPressed = false;
	private bool rightAnalogePushed = false;
	[Header("Police sense")]
	public bool isPoliceSense;

	//Do once after death
	private bool checkBoolChange;

	private void Awake()
	{
		me = this;
		walkingDir = new Vector3();
		maxHP = hp;
		if(StartSceneDataStorage.me != null)
			rot_spd = StartSceneDataStorage.me.rotateSpd_startscene;
	}

	private void Start()
    {
		checkBoolChange = dead;
        anim = playerModel.GetComponent<Animator>();
    }

    private void Update()
	{
		transform.localPosition = new Vector3(transform.localPosition.x, -8.5f, transform.localPosition.z);
		if (!MenuManager.GameIsPaused)
		{
			if (Input.GetKeyDown(KeyCode.M))
			{
				LoseHealth_player(1000);
			}
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
				ShowActivateVFX();
				#region activate and deactivate mats
				if (selectedMats.Count == 0) // put down hand
				{
					anim.SetBool("selected", false);
				}
				// activate mats
				if (!anim.GetCurrentAnimatorStateInfo(1).IsName("testWindup") &&
					!anim.GetCurrentAnimatorStateInfo(1).IsName("testBackswing") &&
					!anim.GetCurrentAnimatorStateInfo(1).IsName("readingText") &&
					!SafehouseManager.Me.isSafehouse &&
					!SafehouseManager.Me.isFading)
				{
					if (Input.GetKeyUp(KeyCode.Alpha1) || Input.GetButtonUp("XButton"))
					{
						if(matSlots[0] != null)
                        {
							
							if (selectedMats.Contains(matSlots[0]))
							{
								selectedMats.Remove(matSlots[0]);
								//UIManager.Me.ComboInstruct();
								SoundMan.SoundManager.MaterialSelect();
							}
							else
							{
								if (matSlots[0].GetComponent<MatScriptNew>().amount > 0 && selectedMats.Count < 2)
								{
									selectedMats.Add(matSlots[0]);
									// vfx
									PlaySelectVFX(matSlots[0]);
									anim.SetBool("selected", true);
									//UIManager.Me.ComboInstruct();
									SoundMan.SoundManager.MaterialSelect();
								}
                                else
                                {
									SoundMan.SoundManager.CannotAccess();
								}
							}
							EffectManagerNew.me.RefreshCurrentMats();
						}
						else
                        {
							SoundMan.SoundManager.CannotAccess();
                        }
					}
					if (Input.GetKeyUp(KeyCode.Alpha2) || Input.GetButtonUp("YButton"))
					{
						if(matSlots[1] != null)
                        {
							if (selectedMats.Contains(matSlots[1]))
							{
								selectedMats.Remove(matSlots[1]);
								//UIManager.Me.ComboInstruct();
								SoundMan.SoundManager.MaterialSelect();
							}
							else
							{
								if (matSlots[1].GetComponent<MatScriptNew>().amount > 0 && selectedMats.Count < 2)
								{
									selectedMats.Add(matSlots[1]);
									// vfx
									PlaySelectVFX(matSlots[1]);
									anim.SetBool("selected", true);
									//UIManager.Me.ComboInstruct();
									SoundMan.SoundManager.MaterialSelect();
                                }
                                else
                                {
									SoundMan.SoundManager.CannotAccess();
								}
							}
							EffectManagerNew.me.RefreshCurrentMats();
						}
                        else
                        {
							SoundMan.SoundManager.CannotAccess();
                        }
					}
					if (Input.GetKeyUp(KeyCode.Alpha3) || Input.GetButtonUp("BButton"))
					{
						if(matSlots[2] != null)
                        {
							if (selectedMats.Contains(matSlots[2]))
							{
								selectedMats.Remove(matSlots[2]);
								//UIManager.Me.ComboInstruct();
								SoundMan.SoundManager.MaterialSelect();
							}
							else
							{
								if (matSlots[2].GetComponent<MatScriptNew>().amount > 0 && selectedMats.Count < 2)
								{
									anim.SetBool("selected", true);
									// vfx
									PlaySelectVFX(matSlots[2]);
									selectedMats.Add(matSlots[2]);
									//UIManager.Me.ComboInstruct();
									SoundMan.SoundManager.MaterialSelect();
                                }
                                else
                                {
									SoundMan.SoundManager.CannotAccess();
								}
							}
							EffectManagerNew.me.RefreshCurrentMats();
						}
                        else
                        {
							SoundMan.SoundManager.CannotAccess();
                        }
					}
					if (Input.GetKeyUp(KeyCode.Alpha4) || Input.GetButtonUp("AButton"))
					{
						if(matSlots[3] != null)
                        {
							if (selectedMats.Contains(matSlots[3]))
							{
								selectedMats.Remove(matSlots[3]);
								//UIManager.Me.ComboInstruct();
								SoundMan.SoundManager.MaterialSelect();
							}
							else
							{
								if (matSlots[3].GetComponent<MatScriptNew>().amount > 0 && selectedMats.Count < 2)
								{
									anim.SetBool("selected", true);
									// vfx
									PlaySelectVFX(matSlots[3]);
									selectedMats.Add(matSlots[3]);
									//UIManager.Me.ComboInstruct();
									SoundMan.SoundManager.MaterialSelect();
                                }
                                else
                                {
									SoundMan.SoundManager.CannotAccess();
								}
							}
							EffectManagerNew.me.RefreshCurrentMats();
						}
                        else
                        {
							SoundMan.SoundManager.CannotAccess();
                        }
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
				// stop walking animation
				if (anim.GetCurrentAnimatorStateInfo(1).IsName("testBackswing") ||
					anim.GetCurrentAnimatorStateInfo(1).IsName("readingText"))
				{
					anim.SetFloat("velocity x", 0);
					anim.SetFloat("velocity z", 0);
				}
				if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) ||
					Mathf.Abs(Input.GetAxis("LeftJoystickHorizontal")) >= joystickSensitivity ||
					Mathf.Abs(Input.GetAxis("LeftJoystickVertical")) >= joystickSensitivity ||
					Mathf.Sqrt(Mathf.Pow(Input.GetAxis("LeftJoystickHorizontal"), 2) + Mathf.Pow(Input.GetAxis("LeftJoystickVertical"), 2)) >= joystickSensitivity) &&
					!anim.GetCurrentAnimatorStateInfo(1).IsName("testWindup") &&
					!anim.GetCurrentAnimatorStateInfo(1).IsName("testATK") &&
					!anim.GetCurrentAnimatorStateInfo(1).IsName("testBackswing") &&
					!anim.GetCurrentAnimatorStateInfo(1).IsName("readingText") &&
					!anim.GetCurrentAnimatorStateInfo(0).IsName("Hitted") &&
					!anim.GetCurrentAnimatorStateInfo(1).IsName("Hitted") &&
					!isPoliceSense)
				{
					walking = true;
					atkButtonPressed = false;
				}

				if (walking && 
					!anim.GetCurrentAnimatorStateInfo(1).IsName("readingText") &&
					!anim.GetCurrentAnimatorStateInfo(0).IsName("Hitted") &&
					!anim.GetCurrentAnimatorStateInfo(1).IsName("Hitted")
					)
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
					else if (Mathf.Abs(Input.GetAxis("LeftJoystickHorizontal")) >= joystickSensitivity ||
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
						//anim.CrossFade("testIdle", .3f);
						anim.Play("Idle", 1);
						//anim.Play("Idle", 0);
						walking = false;
					}
					walkingDir = transform.InverseTransformDirection(walkingDir); // convert walkingDir to local relative to player's direction
					anim.SetFloat("velocity x", Vector3.Normalize(walkingDir).x); // convert magnitude to 1, since the animation parameter is 0 to 1
					anim.SetFloat("velocity z", Vector3.Normalize(walkingDir).z); // same
					// decide walk animation
					if (walkingDir.magnitude > 0)
					{
						if (!anim.GetCurrentAnimatorStateInfo(1).IsName("select") &&
							!anim.GetCurrentAnimatorStateInfo(1).IsName("PostWind") &&
							!anim.GetBool("selected"))
                        {
							//anim.Play("Walking", 1);
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
				//Fuck I didn't want to touch this shit
				#region police sense
				if ((Input.GetButtonDown("LB") || Input.GetKeyDown(KeyCode.P)) &&
					 PostProcessingManager.Me.coroutinesQueue.Count <= 0 &&
					 !anim.GetCurrentAnimatorStateInfo(1).IsName("readingText")) // A lot of shits has been moved by a hero
				{
					PostProcessingManager.Me.PS_Running = true;
					isPoliceSense = true;
					StopPlayer();
					PostProcessingManager.Me.coroutinesQueue.Enqueue(PostProcessingManager.Me.DistorsionFilter());
				}
				if ((Input.GetButtonUp("LB") || Input.GetKeyUp(KeyCode.P)) && PostProcessingManager.Me.PS_Running)
				{
					PostProcessingManager.Me.coroutinesQueue.Enqueue(PostProcessingManager.Me.ResetPolice());
					PostProcessingManager.Me.PS_Running = false;
					isPoliceSense = false;
				}
				#endregion

				// check for attack button press
				#region attack
				if (selectedMats.Count > 0 &&  // check if player has mat activated
					(anim.GetCurrentAnimatorStateInfo(1).IsName("Idle") || // if player in idle state
					walking ||
					//anim.GetCurrentAnimatorStateInfo(1).IsName("Walking") ||
					//anim.GetCurrentAnimatorStateInfo(1).IsName("BackWalking") ||
					//anim.GetCurrentAnimatorStateInfo(1).IsName("WalkingLeft") ||
					//anim.GetCurrentAnimatorStateInfo(1).IsName("WalkingRight") ||  // if player in walk state
					anim.GetCurrentAnimatorStateInfo(1).IsName("select") || // if player in select mat state
					anim.GetCurrentAnimatorStateInfo(1).IsName("PostWind") ||
					anim.GetCurrentAnimatorStateInfo(1).IsName("Protected"))) 
				{
					if ((Input.GetMouseButtonUp(0) || Input.GetAxis("RT") > 0) && !atkButtonPressed) // if left click
					{
						//print(anim.GetCurrentAnimatorStateInfo(1).shortNameHash);
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
							if (anim.GetCurrentAnimatorStateInfo(1).IsName("select") || 
								anim.GetCurrentAnimatorStateInfo(1).IsName("PostWind"))
							{
								anim.Play("testBackswing", 1);
							}
							else
							{
								anim.Play("testWindup", 1); // player anticipation clip and call effect manager's casting event in clip
							}
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
					(anim.GetCurrentAnimatorStateInfo(1).IsName("testIdle") || // if player in idle state
					walking))
					{

					}
					else
					{
						//print(walking);
					}
				}
				#endregion

				if (!anim.GetCurrentAnimatorStateInfo(1).IsName("readingText"))
                {
					StopAllCoroutines();
                }
			}
		}

		if(Input.GetKeyDown(KeyCode.N))
        {
			LoseHealth_player(5);

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
		if (hp > 0 && !anim.GetCurrentAnimatorStateInfo(0).IsName("Protected"))
		{
			anim.SetTrigger("hit");
		}
		SoundMan.SoundManager.PlayerHitten();
		if(hp >= 0)
		{
			PostProcessingManager.Me.GradualDeath(maxHP, hp);
			SoundMan.SoundManager.PlayerLowHealthFilter(); //if player lose health, sound will get blurry
		}
		/*if (hp < 25)
		{
			PostProcessingManager.Me.ChangeFilter();
		}*/
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
			Rigidbody rb = GetComponent<Rigidbody>();
			rb.AddForce(-transform.forward * flyAmount_death, ForceMode.Impulse);
			checkBoolChange = dead;
			if (!anim.GetBool("died"))
			{
				anim.SetBool("died", true);
			}
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
		if (!anim.GetCurrentAnimatorStateInfo(1).IsName("readingText") && !isPoliceSense)
		{
			// lock on
			if ((Input.GetMouseButton(1) || Input.GetAxis("LT") > 0) && LockOnManager.me.bears_canBeLockedOn.Count > 0)
			{
				if (!lockOnPressed) // if haven't pressed it yet, assign lock on target
				{
					lockOnPressed = true;
					lockedOnto = LockOnManager.me.bears_canBeLockedOn[0];
				}
				// change target
				if (Input.GetAxis("RightJoystickHorizontal") >= joystickSensitivity && !rightAnalogePushed)
				{
					rightAnalogePushed = true;
					GameObject newTarget = LockOnManager.me.GetClosest_right();
					lockedOnto = newTarget;
					//print("right: "+newTarget.gameObject.name);
				}
				else if (Input.GetAxis("RightJoystickHorizontal") <= -joystickSensitivity && !rightAnalogePushed)
				{
					rightAnalogePushed = true;
					GameObject newTarget = LockOnManager.me.GetClosest_left();
					lockedOnto = newTarget;
					//print("left: "+newTarget.gameObject.name);
				}
				else if(Input.GetAxis("RightJoystickHorizontal") < joystickSensitivity || Input.GetAxis("RightJoystickHorizontal") > -joystickSensitivity)
				{
					rightAnalogePushed = false;
				}
				targetPos = new Vector3(lockedOnto.transform.position.x, transform.position.y, lockedOnto.transform.position.z);
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPos - transform.position), rot_spd * Time.deltaTime);
			}
			else if (Input.GetAxis("LT") == 0)
			{
				lockOnPressed = false;
				lockedOnto = null;
				// aim with analog stick
				if (Mathf.Abs(Input.GetAxis("RightJoystickHorizontal")) >= joystickSensitivity ||
					 Mathf.Abs(Input.GetAxis("RightJoystickVertical")) >= joystickSensitivity ||
					 Mathf.Sqrt(Mathf.Pow(Input.GetAxis("RightJoystickHorizontal"), 2) + Mathf.Pow(Input.GetAxis("RightJoystickHorizontal"), 2)) >= joystickSensitivity)
				{
					var target = new Vector3(Input.GetAxis("RightJoystickHorizontal"), 0, Input.GetAxis("RightJoystickVertical") * -1);
					transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target), rot_spd * Time.deltaTime);
				}
			}
		}
	}
	public IEnumerator LookTowardsItem(GameObject item)
	{
		var target = new Vector3(item.transform.position.x, transform.position.y, item.transform.position.z);
		while (true)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target - transform.position), rot_spd * Time.deltaTime);
			yield return null;
		}
	}

	public IEnumerator LookTowardsItemOnce(GameObject item)
	{
		var target = new Vector3(item.transform.position.x, transform.position.y, item.transform.position.z);
		var dir = item.transform.position - transform.position;
		bool looked = false;
        while (Vector3.Angle(transform.forward, dir) > 5 && 
			//Vector3.Angle(transform.forward, dir) < 175 &&
			!looked)
        {
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target - transform.position), rot_spd * Time.deltaTime);
			if (Vector3.Angle(transform.forward, dir) <= 10 
				//|| Vector3.Angle(transform.forward, dir) >= 174
				)
            {
				print("don't look");
				looked = true;
			}
			yield return null;
		}
	}

	public void MatSlotUpdate()
    {
		if(matSlots.Count > 5)
        {
			for(int i = 4; i < matSlots.Count; i++)
            {
				if (matSlots[i] == null)
					matSlots.RemoveAt(i);
            }
        }
		if(matSlots.Count < 5)
        {
			int time = 5 - matSlots.Count;
			for(int i = 0; i < time; i++)
            {
				matSlots.Add(null);
            }
        }
    }
	private void PlaySelectVFX(GameObject mat)
	{
		switch (mat.GetComponent<MatScriptNew>().myType)
		{
			case MatScriptNew.MatType.amp:
				GameObject vfx_amp = Instantiate(selectVFX_blue);
				GameObject light_amp = Instantiate(selectLight_blue);
				vfx_amp.transform.position = hand.position;
				light_amp.transform.position = hand.position;
				break;
			case MatScriptNew.MatType.atk:
				GameObject vfx_atk = Instantiate(selectVFX_yellow);
				GameObject light_atk = Instantiate(selectLight_yellow);
				vfx_atk.transform.position = hand.position;
				light_atk.transform.position = hand.position;
				break;
			case MatScriptNew.MatType.functional:
				GameObject vfx_func = Instantiate(selectVFX_white);
				GameObject light_func = Instantiate(selectLight_white);
				vfx_func.transform.position = hand.position;
				light_func.transform.position = hand.position;
				break;
			case MatScriptNew.MatType.boss:
				GameObject vfx_boss = Instantiate(selectVFX_purple);
				GameObject light_boss = Instantiate(selectLight_purple);
				vfx_boss.transform.position = hand.position;
				light_boss.transform.position = hand.position;
				break;
		}
	}
	private void ShowActivateVFX()
	{
		foreach (var mat in selectedMats)
		{
			amp_activated_vfx.SetActive(mat.GetComponent<MatScriptNew>().myType == MatScriptNew.MatType.amp);
			amp_activated_vfx.transform.position = hand.transform.position;
			atk_activated_vfx.SetActive(mat.GetComponent<MatScriptNew>().myType == MatScriptNew.MatType.atk);
			atk_activated_vfx.transform.position = hand.transform.position;
			func_activated_vfx.SetActive(mat.GetComponent<MatScriptNew>().myType == MatScriptNew.MatType.functional);
			func_activated_vfx.transform.position = hand.transform.position;
			boss_activated_vfx.SetActive(mat.GetComponent<MatScriptNew>().myType == MatScriptNew.MatType.boss);
			boss_activated_vfx.transform.position = hand.transform.position;
		}
		if (selectedMats.Count <= 0)
		{
			amp_activated_vfx.SetActive(false);
			atk_activated_vfx.SetActive(false);
			func_activated_vfx.SetActive(false);
			boss_activated_vfx.SetActive(false);
		}
	}

	public void StopPlayer()
    {
		walkingDir = new Vector3(0, 0, 0);
		//anim.CrossFade("testIdle", .3f);
		anim.Play("Idle", 1);
		//anim.Play("Idle", 0);
		walking = false;
		anim.SetFloat("velocity x", 0);
		anim.SetFloat("velocity z", 0);
	}
}
