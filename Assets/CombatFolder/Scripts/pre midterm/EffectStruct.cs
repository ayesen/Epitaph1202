using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // structs are not serializable so we need to add this in order to see it in the inspector
public struct EffectStruct
{
    [Header("DMG")]
    public int damageAmount; // if it's not DOT, one time dmg amount; if it's DOT, dmg amount for each tick
    public bool DOT;
    public float DOT_interval; // interval between each dmg
    public float DOT_duration; // total duration of the DOT
    public float freezeFrameTime;

    [Header("CTRL")]
    public EffectManager.CtrlTypes myCtrlType;
    public float ctrl_duration;
    public float knockback_amount;

    [Header("RECOV")]
    public float healAmount; // if it's not HOT, one time heal amount; if it's hot, heal amount for each tick
    public bool HOT;
    public float HOT_interval; // interval between each heal
    public float HOT_duration; // total duration of the hot

    [Header("SPLY")]
    public List<GameObject> matProduce; // for testing only
    public float dropMatAmount;

    
}
