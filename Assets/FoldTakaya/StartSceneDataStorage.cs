using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneDataStorage : MonoBehaviour
{
    public static StartSceneDataStorage me;
    public float rotateSpd_startscene = 4f;
    
    private void Awake()
    {
        if(me != null && me != this)
        {
            Destroy(gameObject);
        }

        me = this;

        DontDestroyOnLoad(this.gameObject);
    }
}
