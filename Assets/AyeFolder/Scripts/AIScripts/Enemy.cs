using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [Header("BASIC")]
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
    public int changeLimit = 2;
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
    public Transform eventTarget;
    public float stopDis;
    public GameObject dialogueTrigger;

    //private
    private int healthRecord;
    private int shieldRecord;
    private bool MusicIsStopped = false;

    private void Awake()
    {
        this.healthRecord = maxHealth;
        this.shieldRecord = maxShield;
        ghostRider = GetComponent<NavMeshAgent>();
        myAC = GetComponent<AIController>();
        health = maxHealth;
        PhaseSetting();
        Mother = GetComponent<MotherController>();
    }

    private void Update()
    {
        
        //HittedStatesIndication();
        AIDead();
        PhaseSetting();
        BreakMeter_recovery();
        BreakMeter_show();
        if (knockedBack)
		{
            ReactivateNavMesh();
        }
        
    }

    public void EnterHittedState(float hitTimer)
    {
        hittedTime = hitTimer;
        interruptedState = myAC.currentState;
        if (myAC.currentState != myAC.changePhaseState || myAC.currentState!= myAC.dieState)
        {
            myAC.ChangeState(myAC.hittedState);
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
            atkSpd = 2;
            preAtkSpd = 2;
            atkTime = 1;
            postAtkSpd = 2;
            attackamt = 5;

            myTriggerObj = GameObject.Find("Atk1Trigger");
            if (shield <= 0)
            {
                ChangePhase(AIPhase.InBattle2, 20);
            }
        }
        else if (phase == AIPhase.InBattle2)
        {
            atkSpd = 5;
            preAtkSpd = 5;
            atkTime = 1;
            postAtkSpd = 2;
            attackamt = 2;
            myTriggerObj = GameObject.Find("Atk2Trigger");
            if (health < healthLimit && changeLimit > 0)
            {
                shield = maxShield;
                ChangePhase(AIPhase.InBattle1, 10);
            }
        }
        myTrigger = myTriggerObj.GetComponent<AtkTrigger>();
    }

    public bool AIDead()
    {
       
        if (health <= 0)
        {
            /*if (gameObject == EnemyDialogueManagerScript.me.enemy)
            {
                EnemyDialogueManagerScript.me.SpawnDialogueTrigger(0);
            }*/
            myAC.ChangeState(myAC.dieState);
            //FadeInManager.Me.StartCoroutine(UIManager.Me.FadeCanvas(FadeInManager.Me.GetComponent<CanvasGroup>(), 1, 3));
            //StartCoroutine(EndGame(3));
            if (MusicIsStopped == false)
            {
                BGMMan.bGMManger.EndBattleMusic();
                MusicIsStopped = true;
            }
            return true;
        }
        else
            return false;

    }

    public IEnumerator EndGame()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(2);
    }

    public void ResetEnemy()
    {
        health = this.healthRecord;
        maxHealth = this.healthRecord;
        shield = maxShield;
        //maxShield = 200;
        changeLimit = 2;
        Mother.BackKids();
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
        ChangePhase(AIPhase.NotInBattle, 1);
        myAC.ChangeState(myAC.idleState);
        this.transform.position = ResetPos;
        breakMeter_ui.enabled = false;
        //hittedStates.enabled = false;
        EnemyCanvas.SetActive(false);
    }

    public void DealDmg(int dmgAmt)
    {
        if (target.gameObject.CompareTag("Player"))
        {
            target.GetComponent<PlayerScriptNew>().LoseHealth_player(dmgAmt);
            Debug.Log(dmgAmt);
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
        print("dealt " + hurtAmt + " damage to " + gameObject.name);

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
            DealDmg(attackamt);
        }
    }

    public void SoundWaveAtk()
    {
        myTrigger.myMR.material.color = new Color(0, 0.5f, 1, 1);
        float dmgRange = 12;
        float soundWaveDmg = dmgRange - AIToPlayerDist(); //can change later
        StartCoroutine(this.GetComponent<AIEffectManager>().StartSoundWave());
        SoundMan.SoundManager.BearRoar();
        if (AIToPlayerDist() <= dmgRange)
        {
            
            DealDmg(attackamt * (int)soundWaveDmg);
            StartCoroutine(EnemyDotDmg(5f, 1));
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

    public void GotoLoc()
    {
        // go to specific location and stand still for dialogue
        BearMesh.SetActive(true);
        this.GetComponent<CapsuleCollider>().enabled = true;
        this.GetComponent<NavMeshAgent>().enabled = true;
        //EnemyCanvas.SetActive(true);
        breakMeter_ui.enabled = true;
        //hittedStates.enabled = true;
        myTrigger.myMR.enabled = true;
        ChangePhase(AIPhase.InBattle1, 1);
        SafehouseManager.Me.canSafehouse = true;
        BGMMan.bGMManger.StartBattleMusic();
        var item = GameObject.Find("GirlJournal");
        if (item == null)
        {
            isPhaseTwo = true;
        }
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
            DealDmg(dotDmg);
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
}
