using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCtrl : MonoBehaviour 
{
	PlayerStats pStats;

	public Vector2 unitPos;

	public GameObject player;

	public Vector2 direction = Vector2.zero, newDirection = Vector2.zero;

	EnemyWP currentWp,targetWp,previousWp;
	public WP currentTile;

	GameObject Shadow;
	public bool isMoving;


	// Use this for initialization
	void Start () 
	{
		pStats = GetComponent<PlayerStats> ();
		UpdateUnitPos ();
	//	currentWp = GetWpFromPos (unitPos);
		currentTile = GetTileFromPos (unitPos);

		Shadow = transform.Find("FakeShadow").gameObject;

	}

	#region Movement

	void RotatePlayer(Vector2 targetDir)
	{
	//	EnemyWP _target = targetDir;

		if (targetDir == Vector2.right)
		{
			Quaternion right = Quaternion.Euler (0, 90, 0);
			//speed
			transform.rotation = Quaternion.Slerp (transform.rotation, right, pStats.rotSpeed * Time.deltaTime);
		}
		else if (targetDir == Vector2.left)
		{
			Quaternion left = Quaternion.Euler (0, -90, 0);
			//speed
			transform.rotation = Quaternion.Slerp (transform.rotation, left, pStats.rotSpeed * Time.deltaTime);
		}
		else if (targetDir == Vector2.up)
		{
			Quaternion up = Quaternion.Euler (0, 0, 0);
			//speed
			transform.rotation = Quaternion.Slerp (transform.rotation, up, pStats.rotSpeed * Time.deltaTime);
		}
		else if (targetDir == Vector2.down)
		{
			Quaternion down = Quaternion.Euler (0, 180, 0);
			//speed
			transform.rotation = Quaternion.Slerp (transform.rotation, down, pStats.rotSpeed * Time.deltaTime);
		}
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
		return null;

	}

	WP GetTileFromPos(Vector2 pos)	//use for target,nexttile,previous
	{
		var wpm = WayPointManager.instance;

		if (wpm.tile.ContainsKey (pos))
		{
			return wpm.tile [unitPos].GetComponent<WP> ();
		}
		return null;
	}

	float GetDistance(Vector2 target)
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

	void Move()
	{
		//only change direction if input matches grid possibility's
		if (WayPointManager.instance.tile.ContainsKey (currentTile.Pos + direction))
		{
			isMoving = true;
			if (direction != Vector2.zero)
			{
				WP nextTile = GetTileFromPos (currentTile.Pos + direction);

				RotatePlayer (direction);

				// direction should only be changed at this point
				if (GetDistance (nextTile.Pos) < 0.01f) 
				{
					currentTile = nextTile;

					if (currentTile.GetComponent<EnemyWP> () != null)
					{
				//		currentWp = GetWpFromPos (currentTile.Pos);

						if (WayPointManager.instance.tile.ContainsKey (currentTile.Pos + newDirection))
							ChangeDirection ();
					}
				}

				//enable going in reverse direction
				if(newDirection == direction * -1)
				{
				//	currentWp = null;
					ChangeDirection ();
				}

				float newX = direction.x * pStats.playaSpeed;
				float newZ = direction.y * pStats.playaSpeed;

				transform.position += new Vector3 (newX, 0, newZ) * Time.deltaTime;
			}
		}
		else
		{
			isMoving = false;
			ChangeDirection ();
		}
	}

	void ChangeDirection()
	{
		if (newDirection != Vector2.zero || direction != newDirection)
		{
			direction = newDirection;		
		}
	}
	void PlayerInput()
	{
		if (Input.GetKeyDown ("a"))
		{
			newDirection = Vector2.left;
		}
		if (Input.GetKeyDown ("d"))
		{
			newDirection = Vector2.right;
		}
		if (Input.GetKeyDown ("s"))
		{
			newDirection = Vector2.down;
		}
		if (Input.GetKeyDown ("w"))
		{
			newDirection = Vector2.up;
		}

		//kickstart
		if(direction == Vector2.zero)
		{
			direction = newDirection;
		}
	}

	#endregion

	#region ItemInteraction
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Item")
		{
			//	print (col.transform.name);
			Item item = col.GetComponent<Item> ();
			pStats.localScore = ScoreManager.instance.score + item.pointValue;
			ScoreManager.instance.CalculateNewScore ();
			if (item.ItemId == 1) //NRG
			{
				//Maak IE numerator timer SpeedBuff
				StartCoroutine(OnDrugs(item.buffduration, item.speedBuff));
				item.NRGParticle ();
			}
			else if(item.ItemId == 0) //Egg
			{
				item.EggParticle ();
			}
			item.DestroyObj ();
		}
	}

	//Buff Timer
	IEnumerator OnDrugs(float duration, float spBuff)
	{
		pStats.NRGized = true;
		pStats.playaSpeed = pStats.playaSpeed + spBuff;

		yield return new WaitForSeconds (duration);

		pStats.playaSpeed = pStats.playaSpeed - spBuff;
		pStats.NRGized = false;
	}
	#endregion

	#region Death
	public void Dead(bool Bingo)
	{
		DestroyObject (Shadow);
		Destroy (GetComponent<PlayerController> ());
		Level_Manager.instance.playerDied = true;
	}
	#endregion

	// Update is called once per frame
	void Update () 
	{
		UpdateUnitPos ();

		PlayerInput ();
	
		if (direction != Vector2.zero)
		{
			Move ();
		} 
	}
}
