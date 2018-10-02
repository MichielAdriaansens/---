using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WP : MonoBehaviour 
{
	public Vector2 Pos;

	// Use this for initialization
	void Start () 
	{
		Pos.x = Mathf.RoundToInt (transform.position.x);
		Pos.y = Mathf.RoundToInt (transform.position.z);
	}
		
}
