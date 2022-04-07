using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandlePuzzleManager : MonoBehaviour
{
	[Header("Candle Manager")]
	public List<GameObject> candles; // put in order
	public List<GameObject> lightedCandles; // candles that the player lighted
	private bool matched = false;

	[Header("Method Caller")]
	public string methodToCall;
	public GameObject methodsOwner;

	[Header("Door Opener")]
	public List<DoorScript> doors_iCtrl;

	private void Start()
	{
		lightedCandles = new List<GameObject>();
	}

	private void Update()
	{
		if (DoListsMatch(candles, lightedCandles) && !matched)
		{
			CallFunction();
			matched = true;
			if (doors_iCtrl.Count > 0)
			{
				foreach (var door in doors_iCtrl)
				{
					if (!door.isOpen)
					{
						door.ControllDoor();
					}
				}
			}
			lightedCandles.Clear();
		}
		else if (lightedCandles.Count == candles.Count)
		{
			foreach (var candle in lightedCandles)
			{
				candle.GetComponent<CandleScript>().HideStuffs(candle.GetComponent<CandleScript>().objects_toShow); // hide flames
			}
			lightedCandles.Clear();
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
