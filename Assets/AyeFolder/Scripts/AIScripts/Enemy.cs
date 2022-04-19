using System.Collections;
using System.Collections.Generic;
//using AmplifyShaderEditor;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [Header("BASICS")]
    public float def;
    public float def_normal;
    public float def_weak;
    public int health;
    public int maxHealth;
    public int moveSpeed;
    public int shield;
    public int maxShield;
    public int atkSpd;
    public int attackamt;
    public int preAtkSpd;
    public int atkTime;
    public int postAtkSpd;
    public int changePhaseTime;
    public int healthLimit;
    public int changeLimit;
    public float hittedTime;
    public float knockbackAmount;
    public float dot_interval;
    public Vector3 ResetPos;
    public GameObject BearMesh;
    public AIController myAC;
    public enum AIPhase { NotInBattle, InBattle1, InBattle2 };
    public AIPhase phase;
    public bool isPhaseTwo = false;
    public bool doorTrigger = false;

    [Header("Stun")]
    public float edr; // endurance: 0 ~ 1
    public float edr_atk;
    public float edr_normal;
    public float down_time; // how long the enemy stay downed when downed
    public float downPoise;
    public float downPoise_max;
    public float stunPoise;
    public float stunPoise_max;
    private float spRegenTimer;
    public float spRegenTime;
    public Material ogMat;

    [Header("NAV MESH")]
    public NavMeshAgent ghostRider;
    public GameObject target;

    [Header("ANIMATION")]
    public Animator AIAnimator;

    [Header("ATTACK")]
    public AtkTrigger myTrigger;
    public GameObject myTriggerObj;
    public Color Origin = new Color(1, 0.5f, 0.5f, 0.3f);
    public Color TempAtkColor = new Color(1, 0, 0, 0.3f);
    public MotherController Mother;

    [Header("HITTED CTRL")]
    public bool attackable;
    public bool walkable;
    public TextMeshProUGUI hittedStates;
    public bool knockedBack;
    public AIStateBase interruptedState;
    public GameObject EnemyCanvas;
    public Material flashWhite;

    [Header("Supply")]
    public bool breakable;
    public float breakMeter;
    public float breakMeterMax;
    public float recovery_wait;
    private float recovery_timer;
    public float recovery_spd;
    public TextMeshProUGUI breakMeter_ui;
    public List<GameObject> myMats;

    [Header("SCRIPTED EVENTS")]
    public GameObject eventTarget;

    //private
    private int healthRecord;
    private int shieldRecord;
    //private bool MusicIsStopped = false;

    [Header("PHASE 2 TIMER")]
    public float duration_phase2;
    private float timer_phase2;

    // for save point
    [HideInInspector]
    public Transform ogTransform;
    //[HideInInspector]
    public List<DoorScript> myEntrances;
    

    private void Awake()
    {
        this.healthRecord = maxHealth;
        this.shieldRecord = maxShield;
        ghostRider = GetComponent<NavMeshAgent>();
        myAC = GetComponent<AIController>();
        health = maxHealth;
        PhaseSetting();
        Mother = GetComponent<MotherController>();
        downPoise_max = downPoise;
        stunPoise_max = stunPoise;
        ogMat = GetComponentInChildren<SkinnedMeshRenderer>().material;
        breakMeterMax = breakMeter;
        ogTransform = transform;
    }

    private void Update()
    {
        if (!MenuManager.GameIsPaused)
        {
            changeLimit = Mathf.Clamp(changeLimit, 0, int.MaxValue);
            //HittedStatesIndication();
            AIDead();
            PhaseSetting();
            BreakMeter_recovery();
            BreakMeter_show();
            if (knockedBack)
            {
                ReactivateNavMesh();
            }
            ChangeEdrBasedOnStates();
            RegenerateStunPoise();
            Phase2Duration();
            //print("current state: "+myAC.currentState);
        }
    }

    private void ChangeEdrBasedOnStates()
	{
        // low edr
		if (myAC.currentState == myAC.preAttackState || myAC.currentState == myAC.attackState)
		{
            edr = edr_atk;
		}
		else // high edr
		{
            edr = edr_normal;
		}
	}

    public void EnterHittedState(float hitTimer)
    {
        //print("enter hitted state");
        hittedTime = hitTimer;
        //interruptedState = myAC.currentState;
        if (myAC.currentState != myAC.changePhaseState || myAC.currentState!= myAC.dieState)
        {
            myAC.ChangeState(myAC.hittedState);
        }
    }

    public void EnterDownedState()
	{
        hittedTime = down_time;
        if (myAC.currentState != myAC.changePhaseState || myAC.currentState != myAC.dieState)
		{
            myAC.ChangeState(myAC.downedState);
		}
	}

    public void ChangePhase(AIPhase phaseName, int time)
    {
        interruptedState = myAC.currentState;
        myTrigger.myMR.enabled = false;
        phase = phaseName;
        changePhaseTime = time;
        myAC.ChangeState(myAC.changePhaseState);
    }


    public void PhaseSetting()
    {
        if (phase == AIPhase.InBattle1)
        {
            atkSpd = 5;
            preAtkSpd = 5;
            atkTime = 1;
            postAtkSpd = 2;
            attackamt = 5;
            
            myTriggerObj = GameObject.Find("Atk1Trigger");
            if (shield <= 0)
            {
                timer_phase2 = duration_phase2;
                ChangePhase(AIPhase.InBattle2, 1);
            }
        }
        else if (phase == AIPhase.InBattle2)
        {
            atkSpd = 5;
            preAtkSpd = 5;
            atkTime = 1;
            postAtkSpd = 2;
            attackamt = 25;
            myTriggerObj = GameObject.Find("Atk2Trigger");
            if ((health < healthLimit || timer_phase2 <= 0) && changeLimit > 0)
            {
                shield = maxShield;
                ChangePhase(AIPhase.InBattle1, 1);
            }
        }
        myTrigger = myTriggerObj.GetComponent<AtkTrigger>();
    }

    public bool AIDead()
    {
       
        if (health <= 0 && myAC.currentState != myAC.dieState)
        {
            /*if (gameObject == EnemyDialogueManagerScript.me.enemy)
            {
                EnemyDialogueManagerScript.me.SpawnDialogueTrigger(0);
            }*/
            myAC.ChangeState(myAC.dieState);
            //EnemyCanvas.SetActive(false);
            //FadeInManager.Me.StartCoroutine(UIManager.Me.FadeCanvas(FadeInManager.Me.GetComponent<CanvasGroup>(), 1, 3));
            //StartCoroutine(EndGame(3));
            //if (MusicIsStopped == false)
            //{
            //    BGMMan.bGMManger.EndBattleMusic();
            //    MusicIsStopped = true;
            //}

            BGMMan.bGMManger.EndMusic();

            return true;
        }
        else
            return false;
    }

    public IEnumerator EndGame()
    {
        yield return new WaitForSeconds(3f);
        GameObject.Find("SceneLoader").GetComponent<SceneLoader>().LoadLevel(2);
        //SceneManager.LoadScene(2);
    }

    public void ResetEnemy()
    {
        health = this.healthRecord;
        maxHealth = this.healthRecord;
        maxShield = 30;
        shield = maxShield;
        //maxShield = 200;
        changeLimit = 3;
        //Mother.BackKids();
        var item = GameObject.Find("GirlJournal");
        if (item != null)
        {
            this.GetComponent<NavMeshAgent>().enabled = false;
            BearMesh.SetActive(false);
            this.GetComponent<MeshRenderer>().enabled = false;
            this.GetComponent<CapsuleCollider>().enabled = false;
            myTrigger.GetComponent<AtkTrigger>().onAtkTrigger = false;
            myTrigger.myMR.enabled = false;
        }
        else
            isPhaseTwo = true;
        //ChangePhase(AIPhase.NotInBattle, 1); // [Safehouse update] currently commented by Takaya
        gameObject.SetActive(false);
        myAC.ChangeState(myAC.idleState);
        this.transform.position = ResetPos;
        //breakMeter_ui.enabled = false;
        //hittedStates.enabled = false;
        //EnemyCanvas.SetActive(false);
    }

    public void DealDmg(int dmgAmt, GameObject dmgDealer)
    {
        if (target.gameObject.CompareTag("Player"))
        {
            if (target.GetComponent<PlayerScriptNew>().hp > 0)
			{
                target.transform.LookAt(dmgDealer.transform.position);
                target.transform.localEulerAngles = new Vector3(0, target.transform.localEulerAngles.y, 0);
            }
            target.GetComponent<PlayerScriptNew>().LoseHealth_player(dmgAmt);
            //Debug.Log(dmgAmt);
        }
        if (target.gameObject.CompareTag("Enemy"))
        {
            if (CompareTag("PlayerSpawnedBear"))
			{
                target.GetComponent<Enemy>().LoseHealth(dmgAmt);
                GetComponent<CollisionDetectorScript>().InflictEffects(target);
            }
        }
    }

    public void LoseHealth(int hurtAmt)
    {
        // for effect manager new
        ConditionStruct cs = new ConditionStruct
        {
            condition = EffectStructNew.Condition.dealtDmg,
            conditionTrigger = gameObject,
            dmgAmount = hurtAmt
        };
        if (EffectManagerNew.me.gameObject != null)
        {
            EffectManagerNew.me.conditionProcessList.Add(cs);
        }
        //print("dealt " + hurtAmt + " damage to " + gameObject.name);

        // og code
        if (myAC.currentState != myAC.changePhaseState)
        {
            if (shield <= 0)
            {
                if (this.health - hurtAmt >= 0)
                {
                    this.health -= hurtAmt;
                }
                else
                {
                    this.health = 0;
                }
            }
            else
                this.shield -= hurtAmt;
        }
        SoundMan.SoundManager.EnemyHitten();
    }

    public void ChangeSpd(int ChangeAmt)
    {
        moveSpeed += ChangeAmt; // get navmesh move spd
    }

    public void ChaseTarget()
    {
        ghostRider.SetDestination(target.transform.position);
    }

    public void Idleing()
    {
        if (InRange())
        {
            myTrigger.myMR.enabled = true;
            myTrigger.myMR.material.color = Origin;
        }
        if (!InRange())
        {
            myTrigger.myMR.enabled = false;
        }

    }
    public void HittedStatesIndication()
    {
        if (AIDead() == false)
        {
            if (myAC.currentState != myAC.dieState)
            {
                if (walkable && attackable == false)
                {
                    hittedStates.text = "cant attack";
                }
                else if (attackable && walkable == false)
                {
                    hittedStates.text = "cant walk";
                }
                else if (!walkable && !attackable)
                {
                    hittedStates.text = "cant anything";
                }

                else if (walkable && attackable)
                {
                    if (hittedStates != null)
                    {
                        hittedStates.text = "";
                    }
                }
            }
        }
        if(AIDead() == true)
        {
            hittedStates.text = "DEAD";
        }
    }
    public void TempPre(float time)
    {
        myTrigger.myMR.material.color = Color.Lerp(Origin, TempAtkColor, time);
    }

    public void TempPost(float time)
    {
        myTrigger.myMR.material.color = Color.Lerp(TempAtkColor, Origin, time);
    }
    public void KnockBackAtk(float KnockBackAmt, Vector3 AttackerPos, GameObject Receiver)
    {
        myTrigger.myMR.material.color = new Color(1, 1, 1, 1);

        if (InRange())
        {
            //EffectManager.me.KnockBack(knockbackAmount, gameObject, GameObject.FindGameObjectWithTag("Player"));
            Vector3 dir = Receiver.transform.position - AttackerPos;
            Receiver.GetComponent<Rigidbody>().AddForce(dir.normalized * KnockBackAmt, ForceMode.Impulse);
            DealDmg(attackamt, gameObject);
            if (GetComponent<CollisionDetectorScript>())
			{
                GetComponent<CollisionDetectorScript>().InflictEffects(Receiver);
			}
        }
    }

    public void SoundWaveAtk()
    {
        myTrigger.myMR.material.color = new Color(0, 0.5f, 1, 1);
        float dmgRange = 12;
        float soundWaveDmg_decay = AIToPlayerDist()*AIToPlayerDist(); //can change later
        StartCoroutine(GetComponent<AIEffectManager>().StartSoundWave());
        SoundMan.SoundManager.BearRoar();
        if (AIToPlayerDist() <= dmgRange)
        {
            DealDmg((int)(attackamt / soundWaveDmg_decay) * 5, gameObject);
            //print("bear roar dmg: "+(int)(attackamt / soundWaveDmg_decay) * 5);
            //StartCoroutine(EnemyDotDmg(5f, 1));
        }
    }

    public float AIToPlayerDist()
    {
        return Vector3.Distance(transform.position, target.transform.position);
    }

    public bool InRange()
    {
        if (myTrigger.onAtkTrigger)
        {
            return true;
        }
        else
            return false;
    }

    public void BackToFighting()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        phase = AIPhase.InBattle1;
    }

    private void BreakMeter_recovery()
    {
        if (breakMeter < breakMeterMax)
        {
            if (recovery_timer >= 0)
            {
                recovery_timer -= Time.deltaTime;
            }
            else
            {
                breakMeter += recovery_spd * Time.deltaTime;
            }
        }
        else
        {
            recovery_timer = recovery_wait;
        }
    }

    private void BreakMeter_show()
    {
        if (breakMeter_ui != null)
        {
            breakMeter_ui.text = breakMeter.ToString("F2");
        }
    }

    public IEnumerator EnemyDotDmg(float dotTimer, int dotDmg)
    {
        yield return new WaitForSeconds(dot_interval);
        float timer = dotTimer;
        while (timer > 0)
        {
            timer--;
            DealDmg(dotDmg, gameObject);
            yield return new WaitForSeconds(dot_interval);
        }
    }
    public void ReactivateNavMesh()
    {
        if (interruptedState != myAC.dieState || interruptedState != myAC.changePhaseState)
        {
            if (GetComponent<Rigidbody>().velocity.magnitude <= 1f)
            {
                GetComponent<Rigidbody>().isKinematic = true;
                knockedBack = false;

                myAC.ChangeState(myAC.idleState);

            }
        }
        else if (interruptedState == myAC.dieState || interruptedState == myAC.changePhaseState)
        {
            if (GetComponent<Rigidbody>().velocity.magnitude <= 1f)
            {
                GetComponent<Rigidbody>().isKinematic = true;
                knockedBack = false;

                myAC.ChangeState(interruptedState);
            }
        }
    }

    private void RegenerateStunPoise()
	{
        if (stunPoise < stunPoise_max)
		{
            spRegenTimer += Time.deltaTime;
            if (spRegenTimer >= spRegenTime)
			{
                stunPoise = stunPoise_max;
			}
		}
	}

    private void Phase2Duration()
	{
        if (phase == AIPhase.InBattle2)
		{
            if (timer_phase2 > 0)
            {
                timer_phase2 -= Time.deltaTime;
            }
        }
	}

    public void GoToLoc()
	{
        target = eventTarget;
	}
}
