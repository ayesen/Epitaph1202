using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeInventory : MonoBehaviour
{
    [Header("Prefab")]
    public Image choosenSquare;
    public Image choosenCircle;
    private Image selectedPrefab;
    [Header("UI Stuff")]
    public int choosenMatIndex;
    public DisplayInventory DI;
    public int choosenMat;
    public TextMeshProUGUI description;
    public bool isChanging;
    private bool doOnce;
    private bool switching;
    [Header("Colors")]
    public Color squareChoCol;
    [Header("Positions")]
    public int X_Start;
    public int Y_Start;
    public int X_Space_Between_Items;
    public int Number_Of_Column;
    public int Y_Space_Between_Items;
    public Vector3 positionOffset_0;
    public Vector3 positionOffset_1;
    public Vector3 positionOffset_2;

    void Start()
    {
        doOnce = switching;
        choosenMatIndex = 4;
        DI = gameObject.GetComponent<DisplayInventory>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("HorizontalArrow") > 0 
            || Input.GetAxis("LeftJoystickHorizontal") > PlayerScriptNew.me.joystickSensitivity)
        {
            switching = true;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("HorizontalArrow") < 0
                || Input.GetAxis("LeftJoystickHorizontal") < -PlayerScriptNew.me.joystickSensitivity)
        {
            switching = true;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Input.GetAxis("VerticalArrow") < 0
                || Input.GetAxis("LeftJoystickVertical") < -PlayerScriptNew.me.joystickSensitivity)
        {
            switching = true;
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("VerticalArrow") > 0
                || Input.GetAxis("LeftJoystickVertical") > PlayerScriptNew.me.joystickSensitivity)
        {
            switching = true;
        }
        else
        {
            switching = false;
        }
        if (SafehouseManager.Me.isSafehouse)
        {
            //Move the square, choose inventory
            if (doOnce != switching)
            {
                doOnce = switching;
                if (switching && !SafehouseManager.Me.isFading)
                {
                    if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("HorizontalArrow") > 0 
                        || Input.GetAxis("LeftJoystickHorizontal") > PlayerScriptNew.me.joystickSensitivity)
                    {
                        choosenMatIndex += 1;
                        SoundMan.SoundManager.SafehouseMaterialSelect();
                    }
                    else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("HorizontalArrow") < 0 
                            || Input.GetAxis("LeftJoystickHorizontal") < -PlayerScriptNew.me.joystickSensitivity)
                    {
                        choosenMatIndex -= 1;
                        SoundMan.SoundManager.SafehouseMaterialSelect();
                    }
                    else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Input.GetAxis("VerticalArrow") < 0 
                            || Input.GetAxis("LeftJoystickVertical") < -PlayerScriptNew.me.joystickSensitivity)
                    {
                        if (choosenMatIndex > 4)
                        {
                            if (choosenMatIndex <= DI.Amount_Of_Inventory - 1)
                            {
                                choosenMatIndex += 4;
                                SoundMan.SoundManager.SafehouseMaterialSelect();
                            }
                            else
                            {
                                choosenMatIndex = DI.Amount_Of_Inventory + 3;
                                SoundMan.SoundManager.SafehouseMaterialSelect();
                            }
                        }
                        else
                        {
                            choosenMatIndex += 1;
                            SoundMan.SoundManager.SafehouseMaterialSelect();
                        }
                    }
                    else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("VerticalArrow") > 0 
                            || Input.GetAxis("LeftJoystickVertical") > PlayerScriptNew.me.joystickSensitivity)
                    {
                        if (choosenMatIndex > 4)
                        {
                            if (choosenMatIndex - 4 >= 4)
                            {
                                choosenMatIndex -= 4;
                                SoundMan.SoundManager.SafehouseMaterialSelect();
                            }
                            else
                            {
                                choosenMatIndex = 4;
                                SoundMan.SoundManager.SafehouseMaterialSelect();
                            }
                        }
                        else
                        {
                            choosenMatIndex -= 1;
                            SoundMan.SoundManager.SafehouseMaterialSelect();
                        }
                    }
                }
                else
                {
                    
                }
            }

            /*
            if(choosenMatIndex == 3)
            {
                if (!isChanging)
                    choosenMatIndex = 4;
                else
                    choosenMatIndex = 2;
            }
            */
            if(choosenMatIndex < 0)
            {
                choosenMatIndex = 0;
            }

            //Draw the square
            if (choosenSquare.color == Color.white)
                choosenSquare.GetComponent<RectTransform>().localPosition = GetPosition(choosenMatIndex - 4);
            //Draw Circle
            if (choosenCircle.color == Color.white)
            {
                float offset_X = 1;
                float offset_Y = 1;
                if (choosenMatIndex == 0)
                {
                    offset_X = positionOffset_0.x;
                    offset_Y = positionOffset_0.y;
                }
                else if (choosenMatIndex == 1)
                {
                    offset_X = positionOffset_1.x;
                    offset_Y = positionOffset_1.y;
                }
                else if (choosenMatIndex == 2)
                {
                    offset_X = positionOffset_2.x;
                    offset_Y = positionOffset_2.y;
                }
                else if (choosenMatIndex == 3)
                {
                    offset_X = 0;
                    offset_Y = 0;
                }
                choosenCircle.GetComponent<RectTransform>().localPosition = DisplayInventory.Me.GetPosition(choosenMatIndex - 4) + Vector3.up * offset_Y + Vector3.right * offset_X;
            }
            if (choosenMatIndex <= 3)
            {
                choosenSquare.enabled = false;
                choosenCircle.enabled = true;
            }
            else if (choosenMatIndex > 3)
            {
                choosenCircle.enabled = false;
                choosenSquare.enabled = true;
            }

            if (!isChanging)
            {
                choosenSquare.color = Color.white;
                choosenCircle.color = Color.white;
                
                //Limit range
                if (choosenMatIndex - 4 > DI.Amount_Of_Inventory - 1)
                {
                    choosenMatIndex = DI.Amount_Of_Inventory + 3;
                }
                
                //choosen mat
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("AButton"))
                {
                    if(choosenMatIndex != 3)
                    {
                        if (choosenMatIndex < 3)
                        {
                            selectedPrefab = Instantiate(choosenCircle, GameObject.Find(("Canvas-Safehouse")).transform);
                        }
                        else if (choosenMatIndex > 3)
                        {
                            selectedPrefab = Instantiate(choosenSquare, GameObject.Find(("Canvas-Safehouse")).transform);
                        }
                        selectedPrefab.color = new Color(squareChoCol.r, squareChoCol.g, squareChoCol.b, 0.8f);
                        SafehouseManager.Me.cannotExit = true;
                        choosenMat = choosenMatIndex;
                        choosenMatIndex = 0;
                        isChanging = true;
                        SoundMan.SoundManager.SafehouseMaterialSelect();
                    }
                    else
                    {
                        SoundMan.SoundManager.CannotAccess();
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("AButton"))
                {
                    if(choosenMatIndex != 3)
                    {
                        SafehouseManager.Me.cannotExit = false;
                        ChangeMat(choosenMatIndex, choosenMat);
                        DI.CreateDisplay();
                        UIManager.Me.UI_ChangeIcon();
                        isChanging = false;
                        choosenMatIndex = 4;
                        SoundMan.SoundManager.SafehouseMaterialSwap();
                    }
                    else
                    {
                        SoundMan.SoundManager.CannotAccess();
                    }
                }

                if (Input.GetButtonUp("BButton"))
                {
                    SafehouseManager.Me.cannotExit = false;
                    isChanging = false;
                    choosenMatIndex = choosenMat;
                    SoundMan.SoundManager.SafehouseMaterialSelect();
                }

                if(choosenMatIndex < 0)
                {
                    choosenMatIndex = 0;
                }
            }

            //Show description
            if (PlayerScriptNew.me.matSlots[choosenMatIndex] != null)
                description.text = PlayerScriptNew.me.matSlots[choosenMatIndex].GetComponent<MatScriptNew>().Description;
            else
                description.text = "";
        }
    }

    public void ChangeMat(int choosenMat, int targetMat)
    {
        Destroy(selectedPrefab);
        GameObject temp = PlayerScriptNew.me.matSlots[targetMat];
        PlayerScriptNew.me.matSlots[targetMat] = PlayerScriptNew.me.matSlots[choosenMat];
        PlayerScriptNew.me.matSlots[choosenMat] = temp;
        if (PlayerScriptNew.me.matSlots[targetMat] == null)
        {
            //PlayerScriptNew.me.matSlots.RemoveAt(targetMat);
        }
        PlayerScriptNew.me.MatSlotUpdate();
    }
    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_Start + (X_Space_Between_Items * (i % Number_Of_Column)), Y_Start + (-Y_Space_Between_Items * (i / Number_Of_Column)), 0f);
    }
}
