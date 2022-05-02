using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXLoadScript : MonoBehaviour
{
    public List<GameObject> vfxs;

    private void Awake()
    {
        foreach (var vfx in vfxs)
        {
            GameObject _vfx = Instantiate(vfx);
            _vfx.transform.position = new Vector3(999, 999, 999);
        }
    }
}
