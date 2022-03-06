using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManagerScript : MonoBehaviour
{
    public GameObject tutorBear;
    private int tut_state = 0;
    private int state_poise = 1;
    private int state_cd = 2;
    private int state_dmg = 3;

	private void OnTriggerEnter(Collider other)
	{
		// 激活小熊，削韧教学
		if (other.gameObject.CompareTag("Player") && tut_state == 0)
		{
			tut_state = state_poise;
			tutorBear.GetComponent<SmallBear>().enabled = true;
			tutorBear.GetComponent<AIController>().enabled = true;
		}
	}
}
