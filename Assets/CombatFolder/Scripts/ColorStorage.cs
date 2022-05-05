using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorStorage : MonoBehaviour
{
    public static ColorStorage me;

    public Color atkChoCol;
    public Color ampChoCol;
    public Color funChoCol;
    public Color bosChoCol;
    public Color safeColor;
    public Color noMat;

    private void Awake()
    {
        if (me != null && me != this)
            Destroy(gameObject);

        me = this;
    }

    public Color ChoColor(int i)
    {
        if (i > 3)
            return noMat;
        if (PlayerScriptNew.me.matSlots[i] != null)
        {
            if (PlayerScriptNew.me.matSlots[i].GetComponent<MatScriptNew>().amount > 0)
            {
                if (PlayerScriptNew.me.matSlots[i].GetComponent<MatScriptNew>().myType == MatScriptNew.MatType.atk)
                    return atkChoCol;
                else if (PlayerScriptNew.me.matSlots[i].GetComponent<MatScriptNew>().myType == MatScriptNew.MatType.amp)
                    return ampChoCol;
                else if (PlayerScriptNew.me.matSlots[i].GetComponent<MatScriptNew>().myType == MatScriptNew.MatType.functional)
                    return funChoCol;
                else if (PlayerScriptNew.me.matSlots[i].GetComponent<MatScriptNew>().myType == MatScriptNew.MatType.boss)
                    return bosChoCol;
                else
                    return noMat;
            }
            else
                return noMat;
        }
        else if (SafehouseManager.Me.isSafehouse)
        {
            return safeColor;
        }
        else
            return noMat;
    }

}
