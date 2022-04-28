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
        StartCoroutine(ps.LookTowardsItemOnce(lookTarget));
    }
}
