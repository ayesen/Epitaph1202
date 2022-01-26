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
		enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

	private void Update()
	{
        if (Input.GetKeyDown(KeyCode.K) && SafehouseManager.Me.isCheatOn)
        {
			hp = 0;
        }
		print("selected Mats count: " + selectedMats.Count);
		print("walking: " + walking);
		print("current clip: " + anim.GetCurrentAnimatorStateInfo(0).fullPathHash);
		print("atkButtonPressed: " + atkButtonPressed);
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
                !anim.GetCurrentAnimatorStateInfo(0).IsName("testBackswing"))
            {
				if (Input.GetKeyUp(KeyCode.Alpha1) && matSlots[0] != null)
				{
					SoundMan.SoundManager.MaterialSelect();
					if (selectedMats.Contains(matSlots[0]))
					{
						selectedMats.Remove(matSlots[0]);
					}
					else
					{
						if (matSlots[0].GetComponent<MatScriptNew>().amount > 0 && selectedMats.Count < 2)
							selectedMats.Add(matSlots[0]);
					}
					EffectManagerNew.me.RefreshCurrentMats();
				}
				else if (Input.GetKeyUp(KeyCode.Alpha2) && matSlots[1] != null)
				{
					SoundMan.SoundManager.MaterialSelect();
					if (selectedMats.Contains(matSlots[1]))
					{
						selectedMats.Remove(matSlots[1]);
					}
					else
					{
						if (matSlots[1].GetComponent<MatScriptNew>().amount > 0 && selectedMats.Count < 2)
							selectedMats.Add(matSlots[1]);
					}
					EffectManagerNew.me.RefreshCurrentMats();
				}
				else if (Input.GetKeyUp(KeyCode.Alpha3) && matSlots[2] != null)
				{
					SoundMan.SoundManager.MaterialSelect();
					if (selectedMats.Contains(matSlots[2]))
					{
						selectedMats.Remove(matSlots[2]);
					}
					else
					{
						if (matSlots[2].GetComponent<MatScriptNew>().amount > 0 && selectedMats.Count < 2)
							selectedMats.Add(matSlots[2]);
					}
					EffectManagerNew.me.RefreshCurrentMats();
				}
				else if (Input.GetKeyUp(KeyCode.Alpha4) && matSlots[3] != null)
				{
					SoundMan.SoundManager.MaterialSelect();
					if (selectedMats.Contains(matSlots[3]))
					{
						selectedMats.Remove(matSlots[3]);
					}
					else
					{
						if (matSlots[3].GetComponent<MatScriptNew>().amount > 0 && selectedMats.Count < 2)
							selectedMats.Add(matSlots[3]);
					}
					EffectManagerNew.me.RefreshCurrentMats();
				}
			}
			
			#endregion
			#region movement
			if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) &&
				!anim.GetCurrentAnimatorStateInfo(0).IsName("testWindup") &&
				!anim.GetCurrentAnimatorStateInfo(0).IsName("testATK") &&
				!anim.GetCurrentAnimatorStateInfo(0).IsName("testBackswing") &&
				!anim.GetCurrentAnimatorStateInfo(0).IsName("readingText"))
				//anim.GetCurrentAnimatorStateInfo(0).IsName("testIdle")) // if in walk state, walk
			{
				walking = true;
				atkButtonPressed = false;
			}
			if (walking)
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
				else
				{
					walkingDir = new Vector3(0, 0, 0);
					forwarding = false;
					backwarding = false;
					lefting = false;
					righting = false;
					anim.CrossFade("testIdle", .3f);
					walking = false;
				}

				if (walkingDir.magnitude > 0)
				{
					if (Vector3.Angle(walkingDir, transform.forward) < 45 && Vector3.Angle(walkingDir, transform.forward) > -45)
					{
						//anim.Play("testWalk");
						if (!forwarding)
						{
							anim.CrossFade("testWalk", .3f);
							forwarding = true;
							backwarding = false;
							lefting = false;
							righting = false;
						}
					}
					else if (Vector3.Angle(walkingDir, transform.forward) > 135 && Vector3.Angle(walkingDir, transform.forward) < 225)
					{
						if (!backwarding)
						{
							anim.CrossFade("Player_Walking_Backwards", .3f);
							forwarding = false;
							backwarding = true;
							lefting = false;
							righting = false;
						}
					}
					else if (Vector3.Angle(walkingDir, transform.forward) > 45 && Vector3.Angle(walkingDir, transform.forward) < 135)
					{
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

			// look at mouse pos(not changing y-axis)
			//! if this doesn't work properly, check game objects' layers, and make sure the mouse manager ignores the proper layers
			if (Input.GetMouseButton(1))
			{
				var target = new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z);
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target - transform.position), rot_spd * Time.deltaTime);
			}
			else if (!anim.GetCurrentAnimatorStateInfo(0).IsName("readingText"))
			{
				var target = new Vector3(MouseManager.me.mousePos.x, transform.position.y, MouseManager.me.mousePos.z);
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target - transform.position), rot_spd * Time.deltaTime);
			}
            #endregion
            #region Detect Boss Slot
			if(matSlots[3] != null && matSlots[3].GetComponent<MatScriptNew>().amount <= 0)
            {
				if(selectedMats.Contains(matSlots[3]))
					selectedMats.Remove(matSlots[3]);
				matSlots[3] = null;
				UIManager.Me.UI_ChangeIcon();
            }
			for(int i = 0; i<matSlots.Count; i++)
            {
				if(matSlots[i] != null)
                {
					if(matSlots[i].GetComponent<MatScriptNew>().amount <= 0)
                    {
						if (selectedMats.Contains(matSlots[i]))
							selectedMats.Remove(matSlots[i]);
						if (matSlots[i].CompareTag("BossMat"))
						{
							matSlots[i] = null;
							UIManager.Me.UI_ChangeIcon();
						}
                    }
                }
            }
			#endregion
			// check for attack button press
			if (selectedMats.Count > 0 &&  // check if player has mat activated
				(anim.GetCurrentAnimatorStateInfo(0).IsName("testIdle") || // if player in idle state
				walking ||
				anim.GetCurrentAnimatorStateInfo(0).IsName("testWalk") ||
				anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Walking_Backwards") ||
				anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Walking_Left") ||
				anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Walking_Right")))  // if player in walk state
			{
				if (!atkButtonPressed && Input.GetMouseButtonUp(0)) // if left click
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
						//anim.Play("testWindup"); // player anticipation clip and call effect manager's casting event in clip
						anim.CrossFade("testWindup", 0.1f);
					}
					else
					{
						print("YOU DON'T HAVE ENOUGH MATERIALS!!!");
					}
				}
			}
			if (Input.GetMouseButtonUp(0))
			{
				if (selectedMats.Count > 0 &&  // check if player has mat activated
				(anim.GetCurrentAnimatorStateInfo(0).IsName("testIdle") || // if player in idle state
				walking))
				{

				}
				else
				{
					print(walking);
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
}
