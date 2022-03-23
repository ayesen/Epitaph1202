using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactBallScript : MonoBehaviour
{
    public List<GameObject> mats;

    private float _timer = .5f;

    private void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
