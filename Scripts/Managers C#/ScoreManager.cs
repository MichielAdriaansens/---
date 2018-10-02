using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreManager : MonoBehaviour 
{

	public static ScoreManager instance;
	PlayerStats pStats;


	int highScore;
	public int score;

	void Awake()
	{	
		if(instance == null)
		instance = this;
	}

	// Use this for initialization
	void Start () 
	{
		
		if (GameObject.FindWithTag ("PlayerCC") != null)
		{
			pStats = GameObject.FindWithTag ("PlayerCC").GetComponent<PlayerStats> ();
		}
		//if the player hasn't quit or died.. don't reset the score
		if (Game_Manager.instance.GameStateID != 0)
		{
			score = PlayerPrefs.GetInt ("score");
			pStats.localScore = score;
			CalculateNewScore ();
		} 
		else
		{
			PlayerPrefs.SetInt ("score", score);
		}
	}

	void CheckNewhighScore()
	{
		if (PlayerPrefs.HasKey ("highscore"))
		{
			if (score > PlayerPrefs.GetInt("highscore"))
			{
				PlayerPrefs.SetInt ("highscore", score);
			}
		}
		else
		{
			PlayerPrefs.SetInt ("highscore", score);
		}
	}

	public void CalculateNewScore()
	{
		if (pStats != null)
		{
			score = pStats.localScore;
			UiManager.instance.ScoreCount (score);
		}
	}

	public void SaveScore()
	{
		ScoreManager.instance.CheckNewhighScore ();
		PlayerPrefs.SetInt ("score", score);
	}

	public void resetScore()
	{
		score = 0;
		PlayerPrefs.SetInt ("score", score);
	}
}
