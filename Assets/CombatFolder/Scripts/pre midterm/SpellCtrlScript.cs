using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellCtrlScript : MonoBehaviour
{
	static public SpellCtrlScript me;
	private PlayerScript ps;
	private Animator anim;
	public GameObject actualModel;

	[Header("PROJECTILE")]
	public Transform spellSpawnLoc;
	public GameObject spell_proj_prefab;
	private float spellSpd;
	private float spellMass;

	[Header("AOE")] // the ratio between range indicator's scale, aoe collider's scale, and aoe particle effect is rougly 1:15:20
	public GameObject spell_AOE_prefab;
	public GameObject aoeRangeIndicator;
	private float aoeDistance;
	private float aoeRadius; // this is the aoe collider's scale
	private Color aoeOgColor;

	[Header("PIE")]
	public GameObject pieRangeIndicator;
	private float pieRadius;
	private float pieAngle;
	private float defaultA;
	public float highlightA;

	[Header("TARGET")]
	public GameObject targetIndicator;

	[Header("Self")]
	public GameObject selfIndicator;

	[Header("Backswing Cancel")]
	public GameObject lastMat;

	public enum CastType
	{
		none,
		projectile,
		aoe,
		pie,
		target,
		self
	};
	public CastType currentCastType;

	private void Awake()
	{
		me = this;
	}

	private void Start()
	{
		ps = transform.GetComponentInParent<PlayerScript>();
		anim = actualModel.GetComponent<Animator>();
		defaultA = pieRangeIndicator.GetComponent<Image>().color.a;
	}

	private void Update()
	{
		if (ps.currentMat != null &&  // check if player has mat activated
			(anim.GetCurrentAnimatorStateInfo(0).IsName("testIdle") || // if player in idle state
			anim.GetCurrentAnimatorStateInfo(0).IsName("testWalk") || // if player in walk state
			(anim.GetCurrentAnimatorStateInfo(0).IsTag("backswing") && lastMat != ps.currentMat))) // if player not using the same mat again and in backswing state
		{
			currentCastType = ps.currentMat.GetComponent<MatScript>().matCastType; // get current cast type from player script
			//! if cast type projectile
			if (currentCastType == CastType.projectile)
			{
				// turn off other indicators
				aoeRangeIndicator.SetActive(false);
				pieRangeIndicator.SetActive(false);
				targetIndicator.SetActive(false);
				selfIndicator.SetActive(false);

				// cast the spell
				if (Input.GetMouseButtonDown(0) && ps.ConsumeMats())
				{
					lastMat = ps.currentMat;
					anim.Play("testWindup");
				}
			}
			//! if cast type aoe
			else if (currentCastType == CastType.aoe)
			{
				// show range
				aoeRangeIndicator.SetActive(true);
				pieRangeIndicator.SetActive(false);
				targetIndicator.SetActive(false);
				selfIndicator.SetActive(false);

				// get params from mat
				GetMatParams();

				// change indicator range according to spell range
				aoeRangeIndicator.transform.localScale = new Vector3(aoeRadius / 15, aoeRadius / 15, 1);

				// restrict distance
				float dist = Vector3.Distance(MouseManager.me.mousePos, transform.position);
				if (dist > aoeDistance)
				{
					Vector3 fromOriginToObject = MouseManager.me.mousePos - transform.position;
					fromOriginToObject *= aoeDistance;
					fromOriginToObject /= dist;
					aoeRangeIndicator.transform.position = transform.position + fromOriginToObject;
					aoeRangeIndicator.transform.position = new Vector3(aoeRangeIndicator.transform.position.x, .1f, aoeRangeIndicator.transform.position.z);
				}
				else
				{
					aoeRangeIndicator.transform.position = new Vector3(MouseManager.me.mousePos.x, .1f, MouseManager.me.mousePos.z);
				}

				// cast the spell
				if (Input.GetMouseButtonDown(0) && ps.ConsumeMats())
				{
					lastMat = ps.currentMat;
					anim.Play("testWindup_aoe");
				}
			}
			//! if cast type pie
			else if (currentCastType == CastType.pie)
			{
				// turn off other indicators
				aoeRangeIndicator.SetActive(false);
				pieRangeIndicator.SetActive(true);
				targetIndicator.SetActive(false);
				selfIndicator.SetActive(false);

				// get params from mat
				GetMatParams();

				// adjust image based on mat's params
				pieRangeIndicator.GetComponent<Image>().fillAmount = 1f / 360f * pieAngle;
				Quaternion targetAngle = Quaternion.Euler(0, 0, pieAngle / 2 - 180f);
				pieRangeIndicator.GetComponent<RectTransform>().localRotation = targetAngle;
				pieRangeIndicator.GetComponent<RectTransform>().localScale = new Vector3(pieRadius/3f, pieRadius/3f, 1); // ratio between image's scale and overlapsphere's radius is rougly 3

				// cast the spell
				if (Input.GetMouseButtonDown(0) && ps.ConsumeMats())
				{
					lastMat = ps.currentMat;
					anim.Play("testWindup_pie");
				}
			}
			//! if cast type target (we don't have this type yet)
			else if (currentCastType == CastType.target)
			{
				// turn off other indicators
				aoeRangeIndicator.SetActive(false);
				pieRangeIndicator.SetActive(false);
				selfIndicator.SetActive(false);

				//if selected enemy, then show the indicator
				if (MouseManager.me.enemySelected != null)
				{
					targetIndicator.SetActive(true);
					// cast the spell
					if (Input.GetMouseButtonDown(0) && ps.ConsumeMats())
					{
						targetIndicator.GetComponent<Light>().color = new Color(0, 159, 179, 1);
						//! insert effect codes here
						print("hit enemy with target");
						lastMat = ps.currentMat;
						EffectManager.me.ProcessEffects(ps.currentMat, MouseManager.me.enemySelected);
					}
					// change indicator color back to default
					if (Input.GetMouseButtonUp(0))
						targetIndicator.GetComponent<Light>().color = new Color(255, 255, 255, 1);
				}
				else
				{
					targetIndicator.SetActive(false);
				}
			}
			//! if cast type self
			else if (currentCastType == CastType.self)
			{
				// turn off other indicators
				aoeRangeIndicator.SetActive(false);
				pieRangeIndicator.SetActive(false);
				targetIndicator.SetActive(false);
				selfIndicator.SetActive(true);

				// cast the spell
				if (Input.GetMouseButtonDown(0) && ps.ConsumeMats())
				{
					selfIndicator.GetComponent<Light>().color = new Color(0, 159, 179, 1);
				}
				//! effect goes here
				if (Input.GetMouseButtonUp(0))
				{
					lastMat = ps.currentMat;
				}
			}
			else
			{
				// player has no mat
				// turn off all indicators
				aoeRangeIndicator.SetActive(false);
				pieRangeIndicator.SetActive(false);
				targetIndicator.SetActive(false);
			}
		}
	}

	private IEnumerator ChangeToDefaultColor_pie()
	{
		yield return new WaitForSeconds(0.05f);
		pieRangeIndicator.GetComponent<Image>().color = new Color(255,255,255,defaultA);
	}

	private void SpawnSpell_proj() // send out the spell
	{
		if (ps != null)
		{
			GetMatParams();
			GameObject spell = Instantiate(spell_proj_prefab, spellSpawnLoc.position, spellSpawnLoc.rotation);
			spell.GetComponent<MeshRenderer>().material = ps.currentMat.GetComponent<MatScript>().myMaterial;
			spell.GetComponent<Rigidbody>().mass = spellMass;
			spell.GetComponent<Rigidbody>().AddForce(spellSpawnLoc.transform.forward * spellSpd, ForceMode.Impulse);
			spell.GetComponent<SpellScript>().mats.Add(ps.currentMat);
			anim.Play("testATK");
		}
	}

	private void SpawnSpell_aoe() // spawn a cylinder collider
	{
		GetMatParams();
		GameObject aoeSpell = Instantiate(spell_AOE_prefab);
		aoeSpell.transform.position = aoeRangeIndicator.transform.position;
		aoeSpell.transform.localScale = new Vector3(aoeRadius, 2f, aoeRadius);
		aoeSpell.GetComponent<SpellAOEScript>().mat = ps.currentMat;
		anim.Play("testATK_aoe");
	}

	private void SpawnSpell_pie() // check if enemy in pie
	{
		GetMatParams();
		Collider[] targetsAround = Physics.OverlapSphere(transform.position, pieRadius);
		pieRangeIndicator.GetComponent<Image>().color = new Color(255, 255, 255, highlightA);
		foreach (var collider in targetsAround)
		{
			StartCoroutine(ChangeToDefaultColor_pie());
			if (collider.gameObject.CompareTag("Enemy"))
			{
				Vector3 tempV3 = new Vector3(collider.transform.position.x, 0, collider.transform.position.z);
				if (Vector3.Angle(transform.forward, tempV3 - transform.position) < pieAngle / 2)
				{
					//! insert effect here
					EffectManager.me.ProcessEffects(ps.currentMat, collider.gameObject);
				}
				else
				{
					print(collider.gameObject.name + " not in pie, its angle: " + Vector3.Angle(transform.forward, tempV3 - transform.position));
				}
			}
			else if (collider.gameObject.CompareTag("InteractableObject"))
			{
				if (collider.gameObject.GetComponent<InteractableObjectScript>().reactionMat == ps.currentMat)
				{
					collider.gameObject.SendMessage("Reaction");
				}
			}
		}
		anim.Play("testATK_pie");
	}

	private void SpawnSpell_self()
	{
		EffectManager.me.ProcessEffects(ps.currentMat, ps.gameObject);
		selfIndicator.GetComponent<Light>().color = new Color(59, 190, 55, 1);
		anim.Play("testATK_self");
	}

	private void GetMatParams()
	{
		if (ps != null && ps.currentMat != null)
		{
			MatScript ms = ps.currentMat.GetComponent<MatScript>();
			spellMass = ms.mass;
			spellSpd = ms.spd;
			aoeDistance = ms.aoe_distance;
			aoeRadius = ms.aoe_range;
			pieAngle = ms.angle;
			pieRadius = ms.radius;
		}
		else
		{
			//Debug.LogError("player doesn't have a mat yet");
		}
	}
}
