using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMatManager : MonoBehaviour
{
    public GameObject myMat; //the mat ur giving to player, drag from scene
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    
    public void GainMat()
    {
        
        PlayerScriptNew.me.matSlots[3] = myMat;
        PlayerScriptNew.me.matSlots[3].GetComponent<MatScriptNew>().amount = 1;
        UIManager.Me.UI_ChangeIcon();
        Destroy(gameObject);
        
    }
}
