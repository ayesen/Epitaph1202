using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCtrlerScript : MonoBehaviour
{
    public GameObject player;
    public GameObject goal;
    public GameObject enemy;
    public bool on;

    void Update()
    {
        if (on)
		{
            goal.transform.position = player.transform.position;
        }
		else
		{
            goal.transform.position = enemy.transform.position;
		}
    }
}
