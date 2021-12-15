using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeInventory : MonoBehaviour
{
    public Image choosenSquare;
    public Image choosenCircle;
    public int choosenMatIndex;
    public DisplayInventory DI;
    public int choosenMat;
    public TextMeshProUGUI description;
    public bool isChanging;

    public int X_Start;
    public int Y_Start;
    public int X_Space_Between_Items;
    public int Number_Of_Column;
    public int Y_Space_Between_Items;

    void Start()
    {
        choosenMatIndex = 4;
        DI = gameObject.GetComponent<DisplayInventory>();
    }

    void Update()
    {
        if (SafehouseManager.Me.isSafehouse)
        {
                    //Move the square, choose inventory
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            choosenMatIndex += 1;
            SoundMan.SoundManager.SafehouseMaterialSelect();
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            choosenMatIndex -= 1;
            SoundMan.SoundManager.SafehouseMaterialSelect();
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!isChanging)
            {
                if (choosenMatIndex <= DI.Amount_Of_Inventory - 1)
                {
                    choosenMatIndex += 4;
                    SoundMan.SoundManager.SafehouseMaterialSelect();
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!isChanging)
            {
                if (choosenMatIndex - 4 >= 4)
                {
                    choosenMatIndex -= 4;
                    SoundMan.SoundManager.SafehouseMaterialSelect();
                }
            }
        }

        if (!isChanging)
        {
            //choosen mat
            if (Input.GetKeyDown(KeyCode.Space))
            {
                choosenMat = choosenMatIndex;
                choosenMatIndex = 0;
                isChanging = true;
                SoundMan.SoundManager.SafehouseMaterialSelect();
            }
            //Limit range
            if (choosenMatIndex - 4 > DI.Amount_Of_Inventory - 1)
            {
                choosenMatIndex = DI.Amount_Of_Inventory + 3;
            }
            else if (choosenMatIndex < 4)
            {
                choosenMatIndex = 4;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ChangeMat(choosenMatIndex, choosenMat);
                DI.CreateDisplay();
                UIManager.Me.UI_ChangeIcon();
                isChanging = false;
                choosenMatIndex = 4;
                SoundMan.SoundManager.SafehouseMaterialSwap();
            }

            if(choosenMatIndex < 0)
            {
                choosenMatIndex = 0;
            }
            else if(choosenMatIndex > 2)
            {
                choosenMatIndex = 2;
            }
        }

        //Draw the square
        if (!isChanging)
        {
            choosenSquare.color = Color.white;
            choosenSquare.GetComponent<RectTransform>().localPosition = GetPosition(choosenMatIndex - 4);
            choosenCircle.enabled = false;
        }
        else
        {
            choosenSquare.color = new Color32(227, 103, 31, 255);
            choosenCircle.enabled = true;
            choosenCircle.GetComponent<RectTransform>().localPosition = DisplayInventory.Me.GetPosition(choosenMatIndex - 4) + Vector3.up * 190f;
        }
        //Show description
        if(PlayerScriptNew.me.matSlots[choosenMatIndex] != null)
            description.text = PlayerScriptNew.me.matSlots[choosenMatIndex].name;
        }
    }

    public void ChangeMat(int choosenMat, int targetMat)
    {
        GameObject temp = PlayerScriptNew.me.matSlots[targetMat];
        PlayerScriptNew.me.matSlots[targetMat] = PlayerScriptNew.me.matSlots[choosenMat];
        PlayerScriptNew.me.matSlots[choosenMat] = temp;
        if (PlayerScriptNew.me.matSlots[targetMat] == null)
        {
            Debug.Log("choosenMat null");
            PlayerScriptNew.me.matSlots.RemoveAt(targetMat);
        }
    }
    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_Start + (X_Space_Between_Items * (i % Number_Of_Column)), Y_Start + (-Y_Space_Between_Items * (i / Number_Of_Column)), 0f);
    }
}
