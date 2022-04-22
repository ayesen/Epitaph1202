using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnIndicatorScript : MonoBehaviour
{
    private PlayerScriptNew ps;
    private Transform lockedOnto;
    public float indicator_offset;

    private void Start()
    {
        ps = PlayerScriptNew.me;
    }

    void Update()
    {
        if (ps.lockedOnto != null)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            lockedOnto = ps.lockedOnto.transform;
            transform.LookAt(Camera.main.transform);
            transform.position = new Vector3(lockedOnto.position.x, lockedOnto.position.y + indicator_offset, lockedOnto.position.z);
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
