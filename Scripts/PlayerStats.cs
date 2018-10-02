using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

	public int localScore = 0;
	public float playaSpeed = 1f;
	public float rotSpeed = 5f;
	public float stoppingDist = 0.1f;

	public bool NRGized;

	public Vector2 wpID()
	{
		Vector2 myWpID;
		myWpID.x = Mathf.Floor (transform.position.x) + 0.5f;
		myWpID.y = Mathf.Floor (transform.position.z) + 0.5f;
		return myWpID;
	}


}
