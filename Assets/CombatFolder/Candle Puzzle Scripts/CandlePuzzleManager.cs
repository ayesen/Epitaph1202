using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandlePuzzleManager : MonoBehaviour
{
	public List<GameObject> candles; // put in order
	public List<GameObject> lightedCandles; // candles that the player lighted

	public string methodToCall;
	public GameObject methodsOwner;

	private bool matched = false;

	private void Start()
	{
		lightedCandles = new List<GameObject>();
	}

	private void Update()
	{
		if (DoListsMatch(candles, lightedCandles) && !matched)
		{
			print("matched");
			matched = true;
		}
	}

	private void CallFunction()
	{
		if (methodToCall.Length > 0)
		{
			methodsOwner.SendMessage(methodToCall);
		}
	}

	private bool DoListsMatch(List<GameObject> list1, List<GameObject> list2)
	{
		var areListsEqual = true;

		if (list1.Count != list2.Count)
			return false;

		for (var i = 0; i < list1.Count; i++)
		{
			if (list2[i] != list1[i])
			{
				areListsEqual = false;
			}
		}

		return areListsEqual;
	}
}
