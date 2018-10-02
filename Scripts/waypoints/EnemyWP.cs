using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyWP : MonoBehaviour 
{
	public Vector2 EwpID;
	GridManager gridManager;

	public List <EnemyWP> neighbours = new List<EnemyWP>();
	public List <WP> neighbourTile = new List<WP> (); 
	public List <Vector2> validDirection = new List<Vector2>();

	#region DebugVars
	bool gotX;
	bool gotMinX;
	bool gotY;
	bool gotMinY;

	#endregion //Debug for execute in edit mode


	void Awake ()
	{
		gridManager = FindObjectOfType<GridManager> ();
		EwpID.x = Mathf.RoundToInt (transform.position.x); //+ 0.5f;
		EwpID.y = Mathf.RoundToInt (transform.position.z); // + 0.5f;


	}
	void Start()
	{

	//	GetBaseWP ();							//debug 
	//	StartCoroutine (FindNeighbours());		//debug
	}


	#region DebugFindNeighbours 
	IEnumerator FindNeighbours ()
	{
		yield return new WaitForSecondsRealtime (2f);
		GetNeighbourY ();
		GetNeighbourX ();
		GetNeighbourYmin ();
		GetNeighbourXmin ();
		yield return null;
		GetValidDir ();
	}

	void GetNeighbourY ()
	{
		for (int i = 1; i < gridManager.grid.Count; i++)
		{
			Vector2 newID = EwpID;
			newID.y = EwpID.y + i;
			if (gridManager.grid.ContainsKey (newID))
			{
				if (gridManager.grid [newID].hasEwp && !gotY)
				{
					neighbours.Add (gridManager.grid [newID].eWP);
					gotY = true;
				}
			}
			else
			{
				break;
			}
		}
	}
	void GetNeighbourX ()
	{
		for (int i = 1; i < gridManager.grid.Count; i++)
		{
			Vector2 newID = EwpID;
			newID.x = EwpID.x + i;
			if (gridManager.grid.ContainsKey (newID))
			{
				if (gridManager.grid [newID].hasEwp && !gotX)
				{
					neighbours.Add (gridManager.grid [newID].eWP);
					gotX = true;
				}
			}
			else
			{
				break;
			}
		}
	}
	void GetNeighbourYmin ()
	{
		for (int i = 1; i < gridManager.grid.Count; i++)
		{
			Vector2 newID = EwpID;
			newID.y = EwpID.y - i;
			if (gridManager.grid.ContainsKey (newID))
			{
				if (gridManager.grid [newID].hasEwp && !gotMinY)
				{
					neighbours.Add (gridManager.grid [newID].eWP);
					gotMinY = true;
				}
			}
			else
			{
				break;
			}
		}
	}
	void GetNeighbourXmin ()
	{
		for (int i = 1; i < gridManager.grid.Count; i++)
		{
			Vector2 newID = EwpID;
			newID.x = EwpID.x - i;
			if (gridManager.grid.ContainsKey (newID))
			{
				if (gridManager.grid [newID].hasEwp && !gotMinX)
				{
					neighbours.Add (gridManager.grid [newID].eWP);
					gotMinX = true;
				}
			}
			else
			{
				break;
			}
		}
	}
	void GetValidDir()
	{
		if (gotY)
		{
			
			validDirection.Add (Vector2.up);
		}
		if (gotX)
		{

			validDirection.Add (Vector2.right);
		}
		if (gotMinY)
		{

			validDirection.Add (Vector2.down);
		}
		if (gotMinX)
		{

			validDirection.Add (Vector2.left);
		}
	}
		
	void GetBaseWP ()
	{
		if (gridManager.grid.ContainsKey (EwpID))
		{
			gridManager.grid [EwpID].hasEwp = true;
			gridManager.grid [EwpID].eWP = this.GetComponent<EnemyWP> ();
		}
	}	

//	void LateUpdate()
//	{
//		if (Input.GetKeyDown ("a"))
//		{
//			GetNeighbourY ();
//			GetNeighbourX ();
//			GetNeighbourYmin ();
//			GetNeighbourXmin ();
//			GetValidDir ();
//		}
//	}
	#endregion	//Debug for execute in Edit mode
}
