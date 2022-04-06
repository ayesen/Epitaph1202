using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    public List<InventoryDict> inventory;

    public Image imagePrefab;

    public int Amount_Of_Inventory;

    public int X_Start;
    public int Y_Start;
    public int X_Space_Between_Items;
    public int Number_Of_Column;
    public int Y_Space_Between_Items;

    public RectTransform positionHolder_0;
    public RectTransform positionHolder_1;
    public RectTransform positionHolder_2;
    public RectTransform positionHolder_3;

    public GameObject[] images;

    private static DisplayInventory me = null;

    public static DisplayInventory Me
    {
        get
        {
            return me;
        }
    }

    private void Awake()
    {
        if(me != null && me != this)
        {
            Destroy(this.gameObject);
        }

        me = this;
    }
    void Start()
    {
        CreateDisplay();
    }

    void Update()
    {
        
    }
    
    public void CreateDisplay()
    {
        Amount_Of_Inventory = 0;
        images = GameObject.FindGameObjectsWithTag("Inventory");
        foreach(GameObject image in images)
        {
            Destroy(image);
        }

        for (int i = 0; i <= 3; i++)
        {
            if (PlayerScriptNew.me.matSlots[i] != null)
            {
                Image obj = Instantiate(imagePrefab, Vector3.zero, Quaternion.identity, transform);
                obj.sprite = PlayerScriptNew.me.matSlots[i].GetComponent<MatScriptNew>().matIcon;
                obj.GetComponent<RectTransform>().localPosition = GetPosition((i - 4));
                obj.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 200);
            }
        }

        for (int i = 4; i < PlayerScriptNew.me.matSlots.Count; i++)
        {
            if(PlayerScriptNew.me.matSlots[i] != null)
            {
                Image obj = Instantiate(imagePrefab, Vector3.zero, Quaternion.identity, transform);
                obj.sprite = PlayerScriptNew.me.matSlots[i].GetComponent<MatScriptNew>().matIcon;
                obj.GetComponent<RectTransform>().localPosition = GetPosition((i - 4));
            }
            Amount_Of_Inventory += 1;
        }
    }

    public Vector3 GetPosition(int i)
    {
        if (i == -4)
            return positionHolder_0.localPosition;
        else if (i == -3)
            return positionHolder_1.localPosition;
        else if (i == -2)
            return positionHolder_2.localPosition;
        else if (i == -1)
            return positionHolder_3.localPosition;
        else
            return new Vector3(X_Start + (X_Space_Between_Items * (i % Number_Of_Column)), Y_Start + (-Y_Space_Between_Items * (i / Number_Of_Column)), 0f);
    }

    private void CopyList()
    {
        foreach(var obj in GameObject.Find("Player").GetComponent<PlayerScript>().tempInventory)
        {
            inventory.Add(obj);
        }
    }
}
