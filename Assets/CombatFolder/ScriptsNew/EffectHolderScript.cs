using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script controls a gameobject for holding an effect, it is stored in an effect holders holder, which is attached to a game object that can be attacked
public class EffectHolderScript : MonoBehaviour
{
	public GameObject myOwner;
    public EffectStructNew myEffect;
	public Vector3 myCreatorPos; // the object that created this holder
	[HideInInspector]
	public bool destroy = false;
	private float timer;

	private void Start()
	{
		timer = myEffect.forHowLong;
		//print("when "+myEffect.when + " triggered by " + myEffect.triggeredBy + ", do " + myEffect.doThis + " for " + myEffect.forHowMuch + ",\nthis effect lasts for " + myEffect.forHowLong + "s");
	}

	private void Update()
	{
		if (destroy)
		{
			myOwner.GetComponent<EffectHoldersHolderScript>().effectHolders.Remove(gameObject);
			Destroy(gameObject);
		}
		else if (myEffect.forHowLong > 0)
		{
			if (timer > 0)
			{
				timer -= Time.deltaTime;
			}
			else
			{
				myOwner.GetComponent<EffectHoldersHolderScript>().effectHolders.Remove(gameObject);
				Destroy(gameObject);
			}
		}
	}
}
