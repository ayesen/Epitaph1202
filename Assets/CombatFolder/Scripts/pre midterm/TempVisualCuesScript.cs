using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempVisualCuesScript : MonoBehaviour
{
    // temp visual cues
    [Header("Temp Visual Cues")]
    public Material backswing_mat;
    private Material default_mat;
	private MeshRenderer mr;

	private void Start()
	{
		mr = GetComponent<MeshRenderer>();
		default_mat = mr.material;
	}

	private void Update()
	{
		if (PlayerScript.me.anim.GetCurrentAnimatorStateInfo(0).IsName("testBackswing") ||
			PlayerScript.me.anim.GetCurrentAnimatorStateInfo(0).IsName("testBackswing_pie") ||
			PlayerScript.me.anim.GetCurrentAnimatorStateInfo(0).IsName("testBackswing_aoe"))
		{
			if (backswing_mat != null)
			{
				mr.material = backswing_mat;
			}
			else
			{
				Debug.LogError("Drag in a backswing_mat");
			}
		}
		else
		{
			mr.material = default_mat;
		}
	}
}
