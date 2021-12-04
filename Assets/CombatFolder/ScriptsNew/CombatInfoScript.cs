using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class CombatInfoScript : MonoBehaviour
{
    public TextMeshProUGUI myInfoUI;
    public List<string> infoToDisplay;

	private void Update()
	{
		foreach (var info in infoToDisplay.ToList())
		{
			myInfoUI.text += "\n" + info;
			infoToDisplay.Remove(info);
		}
	}
}
