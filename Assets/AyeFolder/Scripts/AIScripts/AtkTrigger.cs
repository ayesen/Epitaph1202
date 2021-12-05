using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkTrigger : MonoBehaviour
{
    public bool onAtkTrigger = false;
    public MeshRenderer myMR;
    public GameObject MyEnemy;

	private void Start()
	{
        MyEnemy = transform.parent.gameObject;
	}

	private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == MyEnemy.GetComponent<Enemy>().target)
        {
            onAtkTrigger = true;
            //myMR.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == MyEnemy.GetComponent<Enemy>().target)
        {
            
            onAtkTrigger = false;
            //myMR.enabled = false;
        }
    }

}
