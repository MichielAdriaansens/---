using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	PlayerStats pStats;

	public Transform target;
	GameObject Shadow;

	public bool isMoving;

	public float highTime;

	bool goingUp;
	bool goingDown;
	bool goingLeft;
	bool goingRight;


	// Use this for initialization
	void Start () 
	{
		pStats = GetComponent<PlayerStats> ();

		Shadow = transform.Find("FakeShadow").gameObject;
	}

	#region Movement
	void Move()
	{
		if (target != null)
		{
			Vector3 direction = target.position - this.transform.position;

			if (direction.magnitude > pStats.stoppingDist)
			{
				this.transform.Translate (direction.normalized * pStats.playaSpeed * Time.deltaTime, Space.World);

				transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), Time.deltaTime * pStats.rotSpeed);
				isMoving = true;
			}
			else
			{
				isMoving = false;
			}
		}
	}

	void DetectWP(Vector3 dir)
	{

		RaycastHit hit;
		Ray front = new Ray (this.transform.position, dir);
		if (Physics.Raycast (front, out hit, 1.5f, 1 << 8))
		{
			target = hit.transform;
		}

	//	Debug.DrawRay (this.transform.position, dir * 1.5f, Color.red,8f);

	} 

	void AutoMove()
	{
		if (goingUp)
		{
			DetectWP (Vector3.forward);
		} else if (goingDown)
		{
			DetectWP (Vector3.back);
		} else if (goingLeft)
		{
			DetectWP (Vector3.left);
		} else if (goingRight)
		{
			DetectWP (Vector3.right);
		}
	}
	#endregion

	#region Items
	//PickUp Items
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
				highTime = item.buffduration;
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

	public void Dead(bool Bingo)
	{
		DestroyObject (Shadow);
		Destroy (GetComponent<PlayerController> ());
		Level_Manager.instance.playerDied = true;
	}

	void Update ()
	{
		PlayerInput ();
		//find better way then Update
		AutoMove();

	}
	void LateUpdate()
	{
		Move();
	}

	void PlayerInput()
	{
		//keyInputs
		if (Input.GetKeyDown("w"))
		{
						goingUp = true;
						goingDown = false;
						goingLeft = false;
						goingRight = false;


		}
		if (Input.GetKeyDown("s"))
		{
						goingDown = true;
						goingUp = false;
						goingLeft = false;
						goingRight = false;

		}
		if (Input.GetKeyDown("a"))
		{
						goingLeft = true;
						goingUp = false;
						goingDown = false;
						goingRight = false;

		}
		if (Input.GetKeyDown("d"))
		{
						goingUp = false;
						goingDown = false;
						goingLeft = false;
						goingRight = true;
		}
	}
}
