using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTowardsScript : MonoBehaviour
{
    public GameObject lookTarget;
    private PlayerScriptNew ps;

    private void Start()
    {
        ps = PlayerScriptNew.me;
    }

    public void LookTowards()
    {
        if (lookTarget != null)
        {
            StartCoroutine(ps.LookTowardsItem(lookTarget));
        }
        else
        {
            Debug.LogError("look target null!");
        }
    }
}
