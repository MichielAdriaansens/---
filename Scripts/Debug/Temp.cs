using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Temp : MonoBehaviour 
{

	List<GameObject> points = new List<GameObject>();

	// Use this for initialization
	void Start () 
	{

		int anus = transform.childCount;

		for (int i = 0; i < anus; i++)
		{
			points.Add (transform.GetChild (i).gameObject);
		}

		for(int i =0; i < points.Count;i++)
		{
			if (!points [i].GetComponent<WP> ())
			{
				
				points [i].AddComponent<WP>();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
