using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatInventoryManager : MonoBehaviour
{
    public Image leftIcon;
    public Image rightIcon;
    public Image upIcon;
    public Image downIcon;
    private List<InventoryDict> playerInventory;

    void Start()
    {
        //playerInventory = PlayerScript.me.tempInventory;
        //ChangeMatIcon();
    }

    void Update()
    {
        
    }

    public void ChangeMatIcon()
    {
        print("Change Icon");
        for (int i = 0; i < 4; i++)
        {
            if (playerInventory[i].Mats == null)
            {
                if (i == 0)
                {
                    leftIcon.color = new Color32(255, 255, 255, 0);
                }
                else if (i == 1)
                {
                    upIcon.color = new Color32(255, 255, 255, 0);
                }
                else if (i == 2)
                {
                    rightIcon.color = new Color32(255, 255, 255, 0);
                }
                else if (i == 3)
                {
                    downIcon.color = new Color32(255, 255, 255, 0);
                }
            }

            if (playerInventory[i].Mats != null)
            {
                if (i == 0)
                {
                    leftIcon.color = new Color32(255, 255, 255, 255);
                    leftIcon.sprite = playerInventory[i].Mats.GetComponent<MatScript>().Icon;
                }
                else if (i == 1)
                {
                    upIcon.color = new Color32(255, 255, 255, 255);
                    upIcon.sprite = playerInventory[i].Mats.GetComponent<MatScript>().Icon;
                }
                else if (i == 2)
                {
                    rightIcon.color = new Color32(255, 255, 255, 255);
                    rightIcon.sprite = playerInventory[i].Mats.GetComponent<MatScript>().Icon;
                }
                else if (i == 3)
                {
                    downIcon.color = new Color32(255, 255, 255, 255);
                    downIcon.sprite = playerInventory[i].Mats.GetComponent<MatScript>().Icon;
                }
            }
            
        }
    }
}
