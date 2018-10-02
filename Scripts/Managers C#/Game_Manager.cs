using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour {

	public static Game_Manager instance;

	public int GameStateID = 0; //0 = main menu, 1 = ingame
	Scene curScene;


	void Awake()
	{
		if (instance == null)
			instance = this;

		curScene = SceneManager.GetActiveScene ();

		if (curScene.name == "CC_MainMenu")
		{
			GameStateID = 0;
		} 
		else if (curScene.name == "CC_Lvl1")
		{
			GameStateID = 1;
		}
	}
		
	// Update is called once per frame
	void Update () 
	{
		
	}
}
