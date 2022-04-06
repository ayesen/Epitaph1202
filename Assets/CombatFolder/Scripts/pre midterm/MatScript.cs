using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatScript : MonoBehaviour
{
    public string namae;
    public Sprite Icon;
    public SpellCtrlScript.CastType matCastType;
    public bool compound; // true - product of combination
    public Material myMaterial;
    public List<EffectStruct> myEffects;

    [Header("Projectile")]
    public float mass;
    public float spd;

    [Header("AOE")]
    public float aoe_distance;
    public float aoe_range;

    [Header("Pie")]
    public float angle;
    public float radius;

    [Header("Target")]
    public float range;

    void Update()
    {
        //Easy to test
        if (Input.GetKeyDown(KeyCode.Alpha1))
            matCastType = SpellCtrlScript.CastType.projectile;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            matCastType = SpellCtrlScript.CastType.aoe;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            matCastType = SpellCtrlScript.CastType.pie;
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            matCastType = SpellCtrlScript.CastType.target;
        else if (Input.GetKeyDown(KeyCode.Alpha5))
		{
            matCastType = SpellCtrlScript.CastType.self;
		}
    }
}
