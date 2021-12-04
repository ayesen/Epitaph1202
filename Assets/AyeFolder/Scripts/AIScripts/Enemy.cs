using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

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
    public float knockbackAmount;
    public Vector3 ResetPos;
    

    public AIController myAC;
    public enum AIPhase { NotInBattle, InBattle1, InBattle2 };
    public AIPhase phase;

    [Header("NAV MESH")]
    public NavMeshAgent ghostRider;
    public GameObject target;

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

    [Header("Supply")]
    public bool breakable;
    public float breakMeter;
    public float breakMeterMax;
    public float recovery_wait;
    private float recovery_timer;
    public float recovery_spd;
    public TextMeshProUGUI breakMeter_ui;

    [Header("SCRIPTED EVENTS")]
    public Transform eventTarget;
    public float stopDis;
    public GameObject dialogueTrigger;

    //private
    private int healthRecord;
    private int shieldRecord;


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
        HittedStatesIndication();
        AIDead();
        PhaseSetting();
        BreakMeter_recovery();
        BreakMeter_show();
        if (knockedBack)
		{
            ReactivateNavMesh();
        }
    }

    public void ChangePhase(AIPhase phaseName, int time)
    {
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
                ChangePhase(AIPhase.InBattle2, 10);
            }
        }
        else if (phase == AIPhase.InBattle2)
        {
            atkSpd = 5;
            preAtkSpd = 7;
            atkTime = 1;
            postAtkSpd = 3;
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

            myAC.ChangeState(myAC.dieState);
            return true;
        }
        else
            return false;

    }

    public void ResetEnemy()
    {
        health = this.healthRecord;
        maxHealth = this.healthRecord;
        shield = this.shieldRecord - 200;
        maxShield = this.shieldRecord - 200;
        changeLimit = 2;
        Mother.BackKids();
        this.GetComponent<NavMeshAgent>().enabled = false;
        ChangePhase(AIPhase.NotInBattle, 1);
        myAC.ChangeState(myAC.idleState);
        this.transform.position = ResetPos;
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<CapsuleCollider>().enabled = false;
        breakMeter_ui.enabled = false;
        hittedStates.enabled = false;
        myTrigger.GetComponent<AtkTrigger>().onAtkTrigger = false;
        myTrigger.myMR.enabled = false;

    }

    public void DealtDmg(int dmgAmt)
    {
        if (target.gameObject.tag == "Player")
        {
            target.GetComponent<PlayerScriptNew>().LoseHealth_player(dmgAmt);
        }
        if (target.gameObject.tag == "Enemy")
        {
            target.GetComponent<Enemy>().LoseHealth(dmgAmt);
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
                if (health - hurtAmt >= 0)
                {
                    health -= hurtAmt;
                }
                else
                {
                    health = 0;
                }
            }
            else
                shield -= hurtAmt;
        }
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
            DealtDmg(attackamt);
        }

    }

    public void SoundWaveAtk()
    {
        myTrigger.myMR.material.color = new Color(0, 0.5f, 1, 1);
        float dmgRange = 12;
        float soundWaveDmg = dmgRange - AIToPlayerDist(); //can change later
        Debug.Log(soundWaveDmg);
        if (AIToPlayerDist() <= dmgRange)
        {
            
            DealtDmg(attackamt * (int)soundWaveDmg);
            /*apply DOT to player here*/
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
        this.GetComponent<MeshRenderer>().enabled = true;
        this.GetComponent<CapsuleCollider>().enabled = true;
        this.GetComponent<NavMeshAgent>().enabled = true;
        breakMeter_ui.enabled = true;
        hittedStates.enabled = true;
        myTrigger.myMR.enabled = true;
        ChangePhase(AIPhase.InBattle1, 1);
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

    public void ReactivateNavMesh()
    {
        if (GetComponent<Rigidbody>().velocity.magnitude <= 1f)
        {
            ghostRider.enabled = true;
            GetComponent<Rigidbody>().isKinematic = true;
            knockedBack = false;
        }
    }
}
