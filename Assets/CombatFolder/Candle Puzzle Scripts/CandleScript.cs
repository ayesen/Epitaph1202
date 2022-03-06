using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleScript : MonoBehaviour
{
    public GameObject reactionMat;
    public List<GameObject> objects_toShow;
    public List<GameObject> objects_toHide;

	public CandlePuzzleManager my_candlePuzzleManager;

	public void Reaction()
	{
		if (!my_candlePuzzleManager.lightedCandles.Contains(gameObject))
		{
			my_candlePuzzleManager.lightedCandles.Add(gameObject);
		}

		ShowStuffs();
		HideStuffs(objects_toHide);
	}

	public void ShowStuffs()
	{
		if (objects_toShow.Count > 0) // show things
		{
			foreach (var thing in objects_toShow)
			{
				thing.SetActive(true);
			}
		}
	}

	public void HideStuffs(List<GameObject> stuffsToHide)
	{
		if (stuffsToHide.Count > 0) // hide things
		{
			foreach (var thing in stuffsToHide)
			{
				thing.SetActive(false);
			}
		}
	}
}
