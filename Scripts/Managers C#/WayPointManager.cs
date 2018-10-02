using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WayPointManager : MonoBehaviour 
{
	 

	//singleton
	public static WayPointManager instance;


	public Dictionary<Vector2,EnemyWP> grid = new Dictionary<Vector2, EnemyWP>();
	public Dictionary<Vector2,WP> tile = new Dictionary<Vector2, WP> ();

	void Awake()
	{
		instance = this;

		var waypoints = FindObjectsOfType<EnemyWP> ();
		foreach(EnemyWP wp in waypoints)
		{
			if (!grid.ContainsKey (wp.EwpID))
			{
				grid.Add (wp.EwpID, wp);
			}
		}

		var tiles = FindObjectsOfType<WP> ();
		foreach (WP _tile in tiles)
		{
			if (!tile.ContainsKey (_tile.Pos))
			{
				tile.Add (_tile.Pos, _tile);
			}
			else
			{
				print ("DoubleTile : " + _tile); //Debug
			}
		}

	}

}
