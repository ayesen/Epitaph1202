using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager me;
    [HideInInspector]
    public int stateOfLevel = 0; // 0 = phase 1; 1 = phase 2

	private void Awake()
	{
		me = this;
	}
}
