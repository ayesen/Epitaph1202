using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this is the container of effect holders
public class EffectHoldersHolderScript : MonoBehaviour
{
    public List<GameObject> effectHolders;

	private void Update()
	{
		if (effectHolders.Count > 0)
		{
			if (!EffectManagerNew.me.enemiesEffected.Contains(gameObject) && 
				gameObject.CompareTag("Enemy"))
			{
				EffectManagerNew.me.enemiesEffected.Add(gameObject);
			}
		}
		else
		{
			if (EffectManagerNew.me.enemiesEffected.Contains(gameObject))
			{
				EffectManagerNew.me.enemiesEffected.Remove(gameObject);
			}
		}
	}
}
