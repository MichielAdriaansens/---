using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour 
{
	public static UiManager instance;

	public bool playTriggered;

	public GameObject inGameHUD;
	public GameObject MainMenuHUD;
	public GameObject winHUD;
	public GameObject loseHUD;

	public Text HighScoreCounterHUD;
	public Text ScoreCounterHUD;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	void Start()
	{
		#region CheckManualObjPlacement
		if (winHUD == null)
		{
			print ("assign obj to winHUD");
		}
		if (loseHUD == null)
		{
			print ("assign obj to loseHUD");
		}
		if (loseHUD == null)
		{
			print ("assign obj to inGameHUD");
		}
		if (ScoreCounterHUD == null)
		{
			print ("assign obj to scoreCounterHUD");
		}
		if (MainMenuHUD == null)
		{
			print ("assign obj to MainMenuHUD");
		}
		if (HighScoreCounterHUD == null)
		{
			print ("assign obj to HighScoreCounterHUD");
		}
		#endregion

		if (Game_Manager.instance.GameStateID == 0)
		{
			inGameHUD.SetActive (false);
			MainMenuHUD.SetActive (true);
		} 
		else if (Game_Manager.instance.GameStateID == 1)
		{
			inGameHUD.SetActive (true);
			MainMenuHUD.SetActive (false);
		}

		//highscoreDisplay
		HiScoreCount (PlayerPrefs.GetInt("highscore"));
	}
	#region Buttons

	#region StartGameButton
	//Called by Button
	public void StartGame () 
	{
		Play (1); //also plays animation and loads level
		//sound
	}
	//start from menu. Input required for multiple/random level generation //to be implemented
	void Play(int curLvl)
	{
		playTriggered = true;
		StartCoroutine (IntroAnim (curLvl));
	}
	IEnumerator IntroAnim(int _curLvl)
	{
		yield return new WaitForSeconds (0.7f);
		SceneManager.LoadScene (_curLvl);
	}
	#endregion

	#region InGameButton
	//Button Back To Start, InGame
	public void BackToStart()
	{
		SceneManager.LoadScene (0);
	}

	//Button Next level
	public void NextLevel()
	{
		SceneManager.LoadScene (1);
	}
	#endregion

	#endregion

	//DisplayWinUI
	public void ifWin(bool Bingo)
	{
		winHUD.SetActive (true);
		ScoreManager.instance.SaveScore ();

	}

	//DisplayLoseUI
	public void ifLose(bool Bingo)
	{
		StartCoroutine (WaitLose());
		ScoreManager.instance.resetScore ();
	}
	IEnumerator WaitLose()
	{
		yield return new WaitForSecondsRealtime (3f);
		loseHUD.SetActive (true);
	}

	public void ScoreCount(int newScore)
	{

		ScoreCounterHUD.text = newScore.ToString();
	} 
	void HiScoreCount(int newHigh)
	{
		HighScoreCounterHUD.text = newHigh.ToString();
	}


}
