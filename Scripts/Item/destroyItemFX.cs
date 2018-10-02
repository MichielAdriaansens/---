using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyItemFX : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		StartCoroutine (HoldUp ());	
	}
	IEnumerator HoldUp()
	{
		yield return new WaitForSeconds (2);
		Destroy (this.gameObject);
	}
	// Update is called once per frame
	void Update () {
		
	}
}
