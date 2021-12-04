using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class CombatInfoScript : MonoBehaviour
{
	public static CombatInfoScript me;
    public TextMeshProUGUI infoPanel;
    public List<string> infoToDisplay;
	public float duration;
	private float timer;

	private void Awake()
	{
		me = this;
	}

	private void Start()
	{
		timer = duration;
	}

	private void Update()
	{
		foreach (var info in infoToDisplay.ToList())
		{
			infoPanel.text += "\n" + info;
			infoToDisplay.Remove(info);
		}
		if (infoPanel.text.Length > 0 && timer > 0)
		{
			timer -= Time.deltaTime;
		}
		else
		{
			timer = duration;
			//infoPanel.text = "";
		}
	}
}
