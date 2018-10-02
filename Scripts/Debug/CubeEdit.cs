using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
[RequireComponent(typeof(WayPoint))]
public class CubeEdit : MonoBehaviour {

	WayPoint waypoint;
//	TextMesh tmesh;
	Renderer mat;

	// Use this for initialization
	void Start () 
	{
		waypoint = GetComponent<WayPoint> ();	
	//	tmesh = GetComponentInChildren<TextMesh> ();
		mat = GetComponent<Renderer> ();

		//check if node is passable Color 
		if (!waypoint.passable)
		{
			mat.sharedMaterial.color = Color.black;
	//		mat.enabled = true;
		} else
		{
			mat.sharedMaterial.color = Color.gray;
		}

		if (waypoint.hasEwp)
		{
			mat.sharedMaterial.color = Color.red;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		DebugShit ();
	}

	void DebugShit ()
	{
//		Vector3 snapPos;
//		snapPos.x = Mathf.RoundToInt (transform.position.x / waypoint.Size) * waypoint.Size;
//		snapPos.z = Mathf.RoundToInt (transform.position.z / waypoint.Size) * waypoint.Size;
//		transform.position = new Vector3 (snapPos.x, 0f, snapPos.z);
//		tmesh.text = snapPos.x + "," + snapPos.z;
//		gameObject.name = "Node " + tmesh.text;

	}
}
