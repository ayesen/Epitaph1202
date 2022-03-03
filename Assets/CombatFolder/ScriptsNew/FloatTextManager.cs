using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatTextManager : MonoBehaviour
{
    private static FloatTextManager me = null;
    public static FloatTextManager Me
    {
        get
        {
            return me;
        }
    }
    public GameObject floatText;
    [Header("Float Text Switch")]
    public bool damageText;
    public bool CDText;
    public bool poiseDamageText;
    [Header("Float Text Color")]
    public Color damageColor;
    public Color CDColor;
    public Color poiseDamageColor;

    public enum TypeOfText {Damage, CD, poiseDamage};
    
    [Header("Offsets")]
    public Vector3 damage_Offset = new Vector3(0.5f, 2.1f, -0.5f);
    public Vector3 CD_Offset = new Vector3(0, 2, 0);
    public Vector3 poiseDamage_Offset = new Vector3(-5, 2, 0);
    public float random_horizontal;
    public float random_vertical;
    public float random_anim;


    private void Awake()
    {
        me = this;
    }

    public void SpawnFloatText(GameObject target, string content, TypeOfText type)
    {
        var FT = Instantiate(floatText, target.transform.position, floatText.transform.rotation, target.transform);
        FT.GetComponent<TextMesh>().text = content;
        float randomV = Random.Range(-random_vertical, random_vertical);
        Vector3 intensity = new Vector3(randomV, Random.Range(-random_horizontal, random_horizontal), -randomV);
        FT.GetComponent<Animator>().speed += Random.Range(-random_anim, random_anim);
        if (type == TypeOfText.Damage)
        {
            FT.GetComponent<TextMesh>().color = damageColor;
            FT.transform.localPosition += damage_Offset + intensity;
        }
        else if(type == TypeOfText.CD)
        {
            FT.GetComponent<TextMesh>().color = CDColor;
            FT.transform.localPosition += CD_Offset + intensity;
        }
        else if (type == TypeOfText.poiseDamage)
        {
            FT.GetComponent<TextMesh>().color = poiseDamageColor;
            FT.transform.localPosition += poiseDamage_Offset + intensity;
        }
    }
}
