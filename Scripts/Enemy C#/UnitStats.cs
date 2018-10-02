using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour 
{
	//points given to player on Death
	public int pointValue = 100;

	//How accurate unit behaves
	public int Performance = 10;

	//Basic movement speed for patrolling
	public float BaseSpeed = 1.2f;

	//Add when chasing or fleeing
	public float BonusSpeed = 0.3f; 

	public float RotSpeed = 10f;

	//how close unit must be to destination to register ..when destination is reached
	public float stoppingDist = 0.5f;

	//Ramge for Attacking Player
	public float AttackRange = 1f;

	public int patrolArea = 0;

	public void setStats (int _performance)
	{
		Performance = _performance;
		if (Performance > 8)
		{
		//	pointValue = 150;
			BaseSpeed = 2f;
		}
		else if (Performance <= 8 && Performance > 5)
		{
			BaseSpeed = 1.8f;
		}
		else if (Performance < 5 && Performance > 2)
		{
			BaseSpeed = 1.5f;
		}
		else if(Performance <= 2)
		{
			//pointValue = 75;

			BaseSpeed = 1.2f;
		}
	}

}
