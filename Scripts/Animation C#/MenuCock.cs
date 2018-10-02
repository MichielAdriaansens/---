using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCock : MonoBehaviour {

	Animator anim;
	UiManager _ui;

	bool stop = false;

	void Start()
	{
		anim = GetComponentInChildren<Animator> ();
		_ui = GameObject.Find ("_UIManager").GetComponent<UiManager> ();
	}

	// Use this for initialization
	void GetCrazy () 
	{
		anim.SetBool ("CrazyMode", true);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (_ui.playTriggered && stop == false)
		{
			GetCrazy ();
			stop = true;
		}	
	}
}
