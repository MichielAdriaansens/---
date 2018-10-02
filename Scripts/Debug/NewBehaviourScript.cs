using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class NewBehaviourScript : MonoBehaviour {
	//public EnemyWP ewp;


	// Use this for initialization
	void Start () {

		GameObject[] _wp;
		_wp = GameObject.FindGameObjectsWithTag ("enemyWP");



		foreach (GameObject wp in _wp)
		{
			wp.AddComponent<EnemyWP> ();
		}

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
