using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move02 : MonoBehaviour 
{
	UnitStats uStats;

	public Vector2 unitPos;

	public GameObject player;

	Vector2 direction = Vector2.zero, newDirection = Vector2.zero;

	EnemyWP currentWp,targetWp,previousWp;
	WP currentTile, targetTile;

	[HideInInspector]
	public bool isMoving = false;

	// Use this for initialization
	void Start () 
	{
		uStats = GetComponent<UnitStats> ();
		UpdateUnitPos ();
	//	currentWp = GetWpFromPos (unitPos);
		currentTile = GetTileFromPos (unitPos);

		direction = Vector2.right;	//Debug

	}
	Vector2 UpdateUnitPos()
	{
		unitPos.x = Mathf.RoundToInt (transform.position.x);
		unitPos.y = Mathf.RoundToInt (transform.position.z);
		return unitPos;
	}

	EnemyWP GetWpFromPos(Vector2 wpId)
	{

		if (WayPointManager.instance.grid.ContainsKey (wpId))
		{
			return WayPointManager.instance.grid[wpId].transform.GetComponent<EnemyWP>();
		}
		else
		return null;

	}

	WP GetTileFromPos(Vector2 pos)	//use for target,nexttile,previous
	{
		var wpm = WayPointManager.instance;

		if (wpm.tile.ContainsKey (pos))
		{
			return wpm.tile [pos];
		}
		else if(targetTile!= null)
		{
			return targetTile;
		}
		return null;
	}
		
	//Debug	Edit Pos for individual unit type here
	Vector2 getTargetPos ()
	{
		Vector2 playerPos;
		playerPos.x = Mathf.RoundToInt (player.transform.position.x);
		playerPos.y = Mathf.RoundToInt (player.transform.position.z);

		return playerPos;
	}

	float GetDistanceDirection(Vector2 target)
	{
		WP targetObj = WayPointManager.instance.tile [target].GetComponent<WP> (); 

		float distX = (targetObj.transform.position.x - transform.position.x);
		float distY = (targetObj.transform.position.z - transform.position.z);


		if (direction == Vector2.left || direction == Vector2.right)
		{
			float distance = distX * distX;

			return distance;
		}
		else if (direction == Vector2.up || direction == Vector2.down)
		{
			float distance = distY * distY;
			return distance;
		}
		return 666f;
	}

	float GetDistanceTotal(Vector2 start,Vector2 end)
	{
		float _x = start.x - end.x;
		float _y = start.y - end.y;

		float distance = Mathf.Sqrt((_x * _x) +(_y * _y));

		return distance;
	}

	void Move()
	{
		if (WayPointManager.instance.tile.ContainsKey (currentTile.Pos + direction))
		{
			if (direction != Vector2.zero) //fix if pdist < tdist 
			{
				isMoving = true;

				WP nextTile = GetTileFromPos (currentTile.Pos + direction);

				RotateUnit (direction);

				if (GetDistanceDirection (nextTile.Pos) < 0.01f) //Debug direction should only be changed at this point
				{
					currentTile = nextTile;

					if (currentTile.GetComponent<EnemyWP> () != null) //has EWP
					{
						currentWp = currentTile.GetComponent<EnemyWP>();


						//Begin to look for new direction
						if (WayPointManager.instance.tile.ContainsKey (currentTile.Pos + newDirection))
							Checkdirections (currentWp,getTargetPos());
					}
				}

				float newX = direction.x * uStats.BaseSpeed;
				float newZ = direction.y * uStats.BaseSpeed;

				transform.position += new Vector3 (newX, 0, newZ) * Time.deltaTime;
			}
		}
		else
		{
			isMoving = false;
			Checkdirections (currentWp,getTargetPos());
		}
	}

	void Checkdirections(EnemyWP ewp, Vector2 _target)
	{

		targetTile = GetTileFromPos (_target);

		//make new List copy validDirections -direction you came from
		List<EnemyWP> NeighbourWp = new List<EnemyWP>();
		List<WP> _neighbourTile = new List<WP> ();

		foreach (WP tile in ewp.neighbourTile)
		{
			_neighbourTile.Add (tile);
		}

		foreach (EnemyWP enemyWP in ewp.neighbours)
		{
			NeighbourWp.Add (enemyWP);
		}
		List<Vector2> ApprovedDirection = new List<Vector2>();
		foreach(Vector2 dir in ewp.validDirection)
		{
			ApprovedDirection.Add (dir);
		}


		for (int i = 0; i < NeighbourWp.Count; i++)
		{
			if (ApprovedDirection[i] == direction * -1)
			{
				ApprovedDirection.Remove (ApprovedDirection[i]);
				NeighbourWp.Remove (NeighbourWp [i]);
				_neighbourTile.Remove (_neighbourTile [i]);
			}
		}

		float lowestDist = 1000000f;

		for(int i = 0; i < ApprovedDirection.Count; i++)
		{

			if(ApprovedDirection[i] != Vector2.zero)
			{
				float distance = GetDistanceTotal(_neighbourTile[i].Pos, targetTile.Pos);

				if (distance < lowestDist)
				{
					lowestDist = distance;
					newDirection = ApprovedDirection [i];
				//	print (targetTile + " N:" + NeighbourWp [i].EwpID + "direction" + ApprovedDirection[i]);
				}
			}
		}

		ChangeDirection ();
		//remove 
	}
		
	void ChangeDirection()
	{
		if (newDirection != Vector2.zero || direction != newDirection)
		{
			direction = newDirection;		
		}
	}

	void RotateUnit(Vector2 _dir)
	{
		if (_dir != Vector2.zero)
		{
			if (_dir == Vector2.up)
			{
				Quaternion up = Quaternion.Euler (0,0,0);

				transform.rotation = Quaternion.Slerp (transform.rotation, up, uStats.RotSpeed * Time.deltaTime);
			}
			else if (_dir == Vector2.down)
			{
				Quaternion down = Quaternion.Euler (0,180,0);

				transform.rotation = Quaternion.Slerp (transform.rotation, down, uStats.RotSpeed * Time.deltaTime);
			}
			else if (_dir == Vector2.right)
			{
				Quaternion right = Quaternion.Euler (0, 90, 0);

				transform.rotation = Quaternion.Slerp (transform.rotation, right, uStats.RotSpeed * Time.deltaTime);
			}
			else if (_dir == Vector2.left)
			{
				Quaternion left = Quaternion.Euler (0, -90, 0);

				transform.rotation = Quaternion.Slerp (transform.rotation, left, uStats.RotSpeed * Time.deltaTime);
			}
		}
	}
	// Update is called once per frame
	void Update () 
	{
		UpdateUnitPos ();
	
		if (direction != Vector2.zero)
		{
			Move ();
		} 
	}
}
