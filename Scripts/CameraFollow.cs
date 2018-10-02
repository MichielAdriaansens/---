using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{
	public Transform pureya;
	Vector3 offset;
	public float lerpSpeed = 2;
	bool gameOver;

	// Use this for initialization
	void Start () 
	{
	//	pureya = GameObject.FindGameObjectWithTag ("PlayerCC").transform;	

		offset = pureya.position - this.transform.position;
		gameOver = false;
	}

	void Follow()
	{
		Vector3 pos = this.transform.position;
		Vector3 targetPos = pureya.position - offset;
		pos = Vector3.Lerp (pos, targetPos, lerpSpeed * Time.deltaTime);
		transform.position = pos;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!gameOver)
		{
			Follow ();
		}
		
	}
}
