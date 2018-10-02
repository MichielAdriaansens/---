using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WayPoint : MonoBehaviour {

//	[HideInInspector]
	public bool explored = false;
//	[HideInInspector]
	public WayPoint foundBy;
	public bool passable = true;

//	[HideInInspector]
	public bool hasEwp;
	public EnemyWP eWP;

	public float Size = 0.5f;

	public Vector2 wpID;

	// Use this for initialization
	void Start () 
	{
		wpID.x = this.transform.position.x;
		wpID.y = this.transform.position.z;

	}
		

	public void SetColor (Color _color) 
	{
		MeshRenderer rend = this.GetComponent<MeshRenderer>();
		if (rend == null)
		{
			print ("WTF");
		}
		rend.material.color = _color;
	}
}
