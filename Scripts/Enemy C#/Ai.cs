using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai : MonoBehaviour 
{
//	Move _move;
	WayPointManager _wpManage;
	UnitStats _uStats;
	AnimCtrl_Enemy _anim;

	GameObject _player;
	Vector3 playerDist;
	public enum UnitState {Idle, Patrol, Chase, FLee, Dead};
	public UnitState unitState;

	public bool isTriggered = false;
	private bool isDead = false;
	public bool gotPlayer;
	private bool runTrigger = false;


	void Start()
	{
	//	_move = GetComponent<Move> ();
		_uStats = GetComponent<UnitStats> ();
		_wpManage = GameObject.Find ("_WayPoint_holder").GetComponent<WayPointManager> ();
		_player = GameObject.FindGameObjectWithTag ("PlayerCC");
		_anim = GetComponent<AnimCtrl_Enemy> ();

	}

	void Chase()
	{
//		_move.onRoute = false;
//		_move.SetDestination ();
//		_move.moveActive = true;
//		_move._CurDestination = _player.transform;
	}
	void Patrol()
	{
//		if (_move.plannedRoute.Count == 0)
//		{
//			if(_uStats.patrolArea == 0)
//			{
//			_move.SendMessage ("SetRoute", _wpManage.tempL);	//set wich route unit will follow 
//			}
//			if(_uStats.patrolArea == 1)
//			{
//				_move.SendMessage ("SetRoute", _wpManage.RouteUpperL);	//set wich route unit will follow 
//			}
//			if(_uStats.patrolArea == 2)
//			{
//				_move.SendMessage ("SetRoute", _wpManage.RouteBottomL);	//set wich route unit will follow 
//			}
//			if(_uStats.patrolArea == 3)
//			{
//				_move.SendMessage ("SetRoute", _wpManage.RouteUpperR);	//set wich route unit will follow 
//			}
//			if(_uStats.patrolArea == 4)
//			{
//				_move.SendMessage ("SetRoute", _wpManage.RouteBottomR);	//set wich route unit will follow 
//			}
//		}
//		_move.FollowRoute ();
	}


	void DistanceFromPlayer()
	{

		playerDist = transform.position - _player.transform.position;
		if (playerDist.magnitude < _uStats.AttackRange)
		{
			//LookatPlayer
			Vector3 newRot = _player.transform.position - this.transform.position;
			transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (newRot), Time.deltaTime * 10);

			if (!_player.GetComponent<PlayerStats> ().NRGized)
			{
				if (!gotPlayer)
				{
					_anim.Attack ();
					KillPlayer ();
						
					gotPlayer = true;
				}
			}
			else
			{
				unitState = UnitState.Dead;
			}

	 	}
	}

	void KillPlayer()
	{
		unitState = UnitState.Idle;

		if (_player.GetComponent<PlayerController> () != null)
		{
			_player.GetComponent<PlayerController> ().SendMessage ("Dead", true);
			_player.GetComponent<AnimCtrl_Playa> ().PlayDeath (transform.position);
		}

	}

	void Death()
	{
		//trigger animation
		_anim.DeathAnim();
		//give points to pstats.localscore
		int pScore = _player.GetComponent<PlayerStats>().localScore; 
		_player.GetComponent<PlayerStats>().localScore = pScore + _uStats.pointValue;
		ScoreManager.instance.CalculateNewScore ();

		//destroy this Object
		Destroy(this.gameObject);
	}
	void SwitchState () 
	{
		switch (unitState)
		{
		case UnitState.Idle:
			#region OnceInUpdate
			if (!isTriggered)
			{
	//			_move.onRoute = false;
				isTriggered = true;
			}
			#endregion
	//		_move.StopUnit ();
			break;
		case UnitState.Patrol:
			#region OnceInUpdate
			if (!isTriggered)
			{
		//		StartCoroutine(SwitchMode());
				isTriggered = true;
				print (unitState);
			}
			#endregion
		//	Patrol ();			
			break;
		case UnitState.FLee:
			#region OnceInUpdate
			if (!isTriggered)
			{
		//		_move.onRoute = false;
				isTriggered = true;
			}
			#endregion
			break;
		case UnitState.Chase:
			#region OnceInUpdate
			if (!isTriggered)
			{
	//			_move.onRoute = false;
			//	StartCoroutine(SwitchMode());
				isTriggered = true;
				print (unitState);
			}
			#endregion
			Chase ();
			break;
		case UnitState.Dead:
			if (!isDead)
			{
				Death ();
				isDead = true;
			}
			break;
		default:
			break;
		}	
	}

	IEnumerator SwitchMode()
	{
		if (unitState == UnitState.Patrol)
		{
			if (runTrigger == true)
			{
				yield return new WaitForSecondsRealtime (_player.GetComponent<PlayerController>().highTime);
				runTrigger = false;
			} else
			{
				yield return new WaitForSecondsRealtime (7f);
			}
			if(!Level_Manager.instance.playerDied)
			{
			isTriggered = false;
			unitState = UnitState.Chase;
			}
		}
		else if (unitState == UnitState.Chase)
		{
			
			yield return new WaitForSecondsRealtime (15f);
			isTriggered = false;
			unitState = UnitState.Patrol;
		} 
	}

	void CheckOnTarget(bool bingo)
	{
		unitState = UnitState.Idle;

		return;
	}
		
	void Update()
	{
		SwitchState ();

		if(!Level_Manager.instance.playerDied)
		{
			DistanceFromPlayer ();	
		}



//		if(!runTrigger && _player.GetComponent<PlayerStats>().NRGized)
//		{
//			unitState = UnitState.Patrol;
//			runTrigger = true;
//		}
	}
}
