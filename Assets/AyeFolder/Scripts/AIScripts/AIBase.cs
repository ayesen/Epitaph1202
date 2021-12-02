using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public abstract class AIBase : MonoBehaviour
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

    [Header("Supply")]
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

    public abstract void ChangePhase(AIPhase phaseName, int time);
    public abstract void PhaseSetting();


    public abstract void AIDead();



}
