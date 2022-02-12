using UnityEngine;
using System.Collections;
using Assets.Scripts;
using UnityEngine.UI;

//namespace Completed
//{

public class GameGUI : MonoBehaviour
{
	public static GameGUI instance = null;
	public GameObject pausedDialog;
	public GameObject wonDialog;
	public GameObject lostDialog;
	//	public GameObject selectObjectsMenu;
	public GameObject playGameButton;
	private bool isPlaying = false;
	private bool lockPauseBtn = false;
	private GameManager	mGameManager = null;
	private float startTime;
	private Text score;
	private Text helthtext;
	private Text Titile;
	private Text cointext;
	GameObject Joystick_right;
	GameObject TopScoreLayer;
	GameObject[] lostText;
	GameObject[] winText;
	public void HideAll ()
	{
		lockPauseBtn = false;
		//mGameManager.SCLock = false;
		pausedDialog.SetActive (false);
		wonDialog.SetActive (false);
		lostDialog.SetActive (false);
	}

	public void ShowPausedMenu ()
	{

		if (lockPauseBtn)
			return;
		mGameManager.SCLock = true;
		pausedDialog.SetActive (true);
		hide ();
	}


	public void hide(){
		Time.timeScale = 0;	
		Joystick_right.SetActive (false);
		TopScoreLayer.SetActive (false);
	}

	public void StartPlayGame ()
	{
		//selectObjectsMenu.SetActive (false);
		playGameButton.SetActive (false);
		Time.timeScale = 1;
		isPlaying = true;
		    
		//mGameManager.PlayGameMode ();
	}


	public void HidePausedMenu ()
	{

		mGameManager.SCLock = false; 
		pausedDialog.SetActive (false);
		Joystick_right.SetActive (true);
		TopScoreLayer.SetActive (true);
		Time.timeScale = 1;	
		
	}

	public void PlayerWon ()
	{
		print ("PlayerWon");
		lockPauseBtn = true;
		wonDialog.SetActive (true);
	}

	public void PlayerLost ()
	{
		print ("PlayerLost");
		lockPauseBtn = true;
		//wonDialog.SetActive(true);
		int lost_text_count = PlayerPrefs.GetInt ("lost_text_count",0);
		int win_text_count = PlayerPrefs.GetInt ("win_text_count",0);

		for (int i = 0; i < winText.Length; i++) {
			winText[i].SetActive (false);
		}
		for (int i = 0; i < lostText.Length; i++) {
			lostText[i].SetActive (false);
		}


		int maxTime = PlayerPrefs.GetInt ("maxtime",10);
		int currentTime = (int)(Time.time - startTime);
		string text = "";
		if (currentTime > maxTime) {
			
			winText[win_text_count].SetActive (true);
			win_text_count++;
			if (win_text_count == winText.Length)
				win_text_count = 0;
			PlayerPrefs.SetInt ("win_text_count",win_text_count);
			lostDialog.SetActive (true);
			Titile.text = "Победа";
			PlayerPrefs.SetInt ("maxtime", currentTime);
			text = currentTime+" сек\n Отличное время!";
		} else {
			
			lostText[lost_text_count].SetActive (true);
			lost_text_count++;
			if (lost_text_count == lostText.Length)
				lost_text_count = 0;
			PlayerPrefs.SetInt ("lost_text_count",lost_text_count);
			lostDialog.SetActive (true);
			Titile.text = "Фиаско";
			text = currentTime+" сек\n Ваш лучший результат:"+maxTime+" сек";
		}
		Text Scoretext = GameObject.Find ("scoretext").GetComponent<Text>();
		Scoretext.text =text;
		hide ();
	}




	// Use this for initialization
	void Start ()
	{
		print ("GUI Start");
		 Joystick_right = GameObject.Find ("Joystick_left");
		 TopScoreLayer = GameObject.Find ("TopScoreLayer");
		lostText = GameObject.FindGameObjectsWithTag("lost_text");
		winText = GameObject.FindGameObjectsWithTag("win_text");
		Titile = GameObject.Find ("Titile").GetComponent<Text>();
		print (lostText.Length);
		HideAll ();
		lockPauseBtn = false;
		mGameManager = GameManager.instance;
		startTime = Time.time;
		score = GameObject.Find ("score").GetComponent<Text>();
		helthtext = GameObject.Find ("helthtext").GetComponent<Text>();
		cointext = GameObject.Find ("cointext").GetComponent<Text>();

	}
	void Update ()
	{
		score.text = ""+(Time.time - startTime).ToString("F1");

	}

	/*
	 * 
	 *Перезагрузка уровня! 
	 */
	public void RealoadGame ()
	{
		this.HideAll ();
		HidePausedMenu ();
		lockPauseBtn = false;
		print ("RealoadGame");
		Application.LoadLevel (Application.loadedLevel);
			
	}










	public void NextLevel ()
	{
		this.HideAll ();
		lockPauseBtn = false;
		print ("NextLevel");
		GameManager	mGameManager = GameObject.FindGameObjectWithTag ("gm").GetComponent<GameManager> ();
		//Application.LoadLevel (mGameManager.nextleve);
		
	}
	//В ГЛАВНОЕ МЕНЮ
	public void GoToMainMenu ()
	{
		this.gameObject.SetActive (false);
		Application.LoadLevel ("StartScene");
	}
	// Update is called once per frame


	public void SetHelthText(int t){
		if(helthtext!=null)
		helthtext.text = "x" + t.ToString ();


	}
	public void SetCoinText(int t){
		if(cointext!=null)
			cointext.text =t.ToString ();


	}


}
//}
