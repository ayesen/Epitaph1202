using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedMatScript : MonoBehaviour
{
    public GameObject myMat;
    public int amount;
	public float attractDis;
	public float killDis;
    public float attractSpd;

	private void Update()
	{
		if (Vector3.Distance(PlayerScriptNew.me.transform.position, transform.position) < attractDis)
		{
			if(myMat.CompareTag("BossMat") && PlayerScriptNew.me.matSlots[3] == null)
				FlyTowardsPlayer();
			if (myMat.tag != "BossMat")
				FlyTowardsPlayer();
		}
		if (Vector3.Distance(PlayerScriptNew.me.transform.position, transform.position) < killDis)
		{
			if (myMat.CompareTag("BossMat"))
			{
				if (PlayerScriptNew.me.matSlots[3] == null)
				{
					PlayerScriptNew.me.matSlots[3] = myMat;
					PlayerScriptNew.me.matSlots[3].GetComponent<MatScriptNew>().amount = 1;
					Destroy(gameObject);
				}
			}
			else
			{
				foreach (var mat in PlayerScriptNew.me.matSlots)
				{
					if (mat.name == myMat.name)
					{
						MatScriptNew ms = mat.GetComponent<MatScriptNew>();
						ms.amount += amount;
						ms.amount = Mathf.Clamp(ms.amount, 0, ms.amount_max);
					}
				}
				Destroy(gameObject);
			}
		}
		float min_y = Mathf.Clamp(transform.position.y, 0, float.MaxValue);
		transform.position = new Vector3(transform.position.x, min_y, transform.position.z);
	}

	private void FlyTowardsPlayer()
	{
		GetComponent<Rigidbody>().useGravity = false;
		GetComponent<BoxCollider>().isTrigger = true;
        Vector3 targetPos = PlayerScriptNew.me.transform.position;
        transform.position = Vector3.Lerp(transform.position, targetPos, attractSpd * Time.deltaTime);
	}
}
