using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Level_Manager : MonoBehaviour
{
	public static Level_Manager instance;
	int curLevel = 1;
	public bool playerDied;
	public bool levelWon; 

	public GameObject Enemy;

	GameObject[] SpawnPoint;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}

		SpawnPoint = GameObject.FindGameObjectsWithTag ("EnemySp");
	}

	GameObject[] Items;
	public int EggCounter = 0;

	// Use this for initialization
	void Start () 
	{
		//Count Eggs
		Items = GameObject.FindGameObjectsWithTag ("Item");
		foreach(GameObject item in Items)
		{
			if (item.GetComponent<Item> ().ItemId == 0)
			{
				EggCounter ++;
			}
		}

		GenerateEnemies ();

		if(Enemy == null)
		{
			print ("Assign Enemy to Enemy Gameobject in levelManage");
		}
		
	}

	void GenerateEnemies()
	{
		if (curLevel < 2)
		{
	//		Instantiate (Enemy, SpawnPoint [0].transform.position, Quaternion.identity);
	//		Instantiate (Enemy, SpawnPoint [1].transform.position, Quaternion.identity);
	//		Instantiate (Enemy, SpawnPoint [2].transform.position, Quaternion.identity);
		}

//		SetEnemies ();
	}

	void SetEnemies()
	{
		GameObject[] _enemies = GameObject.FindGameObjectsWithTag ("Enemy");

		foreach (GameObject unit in _enemies)
		{
			UnitStats stats = unit.GetComponent<UnitStats> ();

			int _rng = Random.Range (1, 10);
			stats.setStats (_rng);
		}

		for(int i = 0; i < _enemies.Length; i++)
		{
			_enemies [i].GetComponent<UnitStats> ().patrolArea = i + 1;

			if (_enemies.Length > 4)
			{
				_enemies [i].GetComponent<UnitStats> ().patrolArea = 0;
			}
		}
	}


	// Update is called once per frame
	void Update () 
	{
		//Track win lose condition
		if (EggCounter == 0)
		{
			levelWon = true;
			UiManager.instance.ifWin (levelWon);
		}
		if(playerDied)
		{
			UiManager.instance.ifLose (playerDied);
		}
	}
}
