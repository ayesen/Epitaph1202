using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDialogueManagerScript : MonoBehaviour
{
	public static EnemyDialogueManagerScript me;
    public List<GameObject> dialogueTriggers;
	public GameObject enemy;

	private void Awake()
	{
		me = this;
	}

	private void Start()
	{
		//enemy = GameObject.FindGameObjectWithTag("Enemy");
	}

	public void SpawnDialogueTrigger(int index)
	{
		if (enemy.GetComponent<Enemy>().dialogueTrigger == null)
		{
			GameObject newDialogueTrigger = Instantiate(dialogueTriggers[index], enemy.transform);
			newDialogueTrigger.transform.position = enemy.transform.position;
			enemy.GetComponent<Enemy>().dialogueTrigger = newDialogueTrigger;
		}
	}
}
