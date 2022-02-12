using UnityEngine;
using System.Collections;
using Assets.Scripts;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour {
	public static GameState CurrentGameState = GameState.Playing;
	public GameObject ball;
	[HideInInspector]
	public bool isEnemy = false;
	private GameObject MainCamera;
	[HideInInspector]
	public bool SCLock = false;
	[HideInInspector]
	public float playerCatPositionX;
	private Text moneytext;
	private Text enemytext;
	private int colEnemy = 0;
	private int money = 0;
	private float startTime;
	private int ballcount = 0;
	public static GameManager instance;
	public bool showDirection = false;
	public bool bonusSLow = false;
	public void StartGame(){
		print ("StartGame()");
		startTime = Time.time;
		startTimeGenBonus = Time.time;
		//Instantiate (ball,new Vector2(-2.19f,1.49f) , Quaternion.identity);

	}

	public float startTimeBonusLine;
	public float startTimeBonusSlow;
	public float startTimeGenBonus;
	public GameObject bonusLine;
	public GameObject bonusSlow;
	public GameObject bonusLife;
	public GameObject bonusCoin;
	public GameObject bonusBomb;
	void FixedUpdate () {
		//print ("StartGame()");
		if (ballcount < 9) {
			if ((Time.time - startTime) > 10) {
				startTime = Time.time;
				//ballcount++;
				Instantiate (ball, new Vector2 (-2.19f, 1.49f), Quaternion.identity);
				ballcount = GameObject.FindGameObjectsWithTag ("ball").Length;

			}
		}
		//Генерация бонусов
			if ((Time.time - startTimeGenBonus) > 9) {
			    startTimeGenBonus = Time.time;
			GameObject g = null;
			int rand = Random.Range (0,10);
			if (rand == 0||rand == 1) {
				g = bonusLine;
			}
			if (rand == 2||rand == 3) {
				g = bonusSlow;
			}
			if (rand == 4) {
				g = bonusLife;
			}
			if (rand == 5||rand==6) {
				g = bonusCoin;
			}
			if (rand == 7||rand == 8 ||rand == 9 ) {
				g = bonusBomb;
			}
			if(g)Instantiate (g, new Vector2 (Random.Range(-3f,3f), Random.Range(-1.4f,1.4f)), Quaternion.identity);

			}


		//Время действия Бонуса - показать направление движения
		if (this.showDirection) {
			if ((Time.time - startTimeBonusLine) > 15) {
				this.showDirection = false;
			}
		}
		//Время действия Бонуса - показать направление движения
		if (this.bonusSLow) {
			if ((Time.time - startTimeBonusSlow) > 12) {
				this.bonusSLow = false;
			}
		} 


	}

	public void plusLife(){
		mChicken.lifeN++;
		mChicken.GUI_updateLife ();
	}


	public void MoneyPlus(){
		mChicken.GUI_updateMoney ();
	}


















	void Awake (){
		print ("---------------------------------------------Awake--------------------------------------------------");
		//PlayerPrefs.SetInt (Constants.CurrentLevelName, 0);
		if (instance == null) {
			instance = this;
			instance.InitGame ();
		}
		else if (instance != this) {
			instance.InitGame ();
			Destroy (gameObject);

		}

		//StartCoroutine (instance.StartAfter1Sec());
	//	DontDestroyOnLoad (gameObject);

	}

	private void GenerateBalls(){
		Instantiate (ball,position , Quaternion.identity);

	}
	//------ SUPERCAT----------------
	private GameObject superCat;
	private Chicken mChicken;

	void InitGame () {
		MainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
	//	GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMove>().enabled = true;
		Time.timeScale = 1;
		superCat = GameObject.FindGameObjectWithTag ("supercat");
		mChicken = superCat.GetComponent<Chicken>();
		GameManager.CurrentGameState = GameState.BOMB_FLYING;
		GameObject mt = GameObject.Find ("cointext");
		if (mt != null) {
			
			moneytext = mt.GetComponent<Text> ();
			money = PlayerPrefs.GetInt ("user_money", 0);
			//print ("money NULL NOT money="+money);
			moneytext.text =money.ToString() ;
		} 
		//подсчет врагов
		//GameObject[] enemy;
		//enemy = GameObject.FindGameObjectsWithTag("enemy_cats");
	   // mt = GameObject.Find ("enemytext");
		//if (mt != null) {

		//	enemytext = mt.GetComponent<Text> ();
		//	 colEnemy = enemy.Length;
		//	print (colEnemy);
		//	enemytext.text =colEnemy.ToString() ;
		//} 
		StartGame ();
	}

	[HideInInspector]
	public float joystickLX = 0;
	[HideInInspector]
	public float joystickLY = 0;
	[HideInInspector]
	public float joystickRX = 0;
	[HideInInspector]
	public float joystickRY = 0;
	[HideInInspector]
	public bool  loystickRactive = false;
	[HideInInspector]
	public void setJoysticValues(float x , float y,bool isLeft){
		//print ("SetLeftJoysticValues x="+x+" y="+y);
		if (isLeft) {
			if (x < 0.2 && x > -0.2)
				x = 0;
			if (y < 0.2 && y > -0.2)
				y = 0;
		}
		if (isLeft) {
			this.joystickLX = x;
			this.joystickLY = y;
		} else {
			this.joystickRX = x;
			this.joystickRY = y;
			if (x == 0)
				loystickRactive = false;
			else
				loystickRactive = true;
		}
	}
	public void JoystickUp(bool isLeft){
	mChicken.JoystickUp( isLeft);
	}
	public void JoystickDown(bool isLeft){
		mChicken.JoystickDown( isLeft);
	}




	/*
	 * добавлнеи жизни	
	 */
	public void plusMoney(){
		money++;
		moneytext.text =money.ToString() ;
		PlayerPrefs.SetInt("user_money",money);
	}


	public bool OnMouseDown(Vector3 mousePos){
		 return false;
	}
	public void onMouseUp(){
		
	}






	// Use this for initialization

	Vector3	position;




	public void PlayerWon(){
	//	mChicken.EndGame ();
        GameGUI	mGameGUI = GameObject.FindGameObjectWithTag ("GameCanvas").GetComponent<GameGUI>();
		mGameGUI.PlayerWon ();
		//PlayerPrefs.SetInt("bonus_binokl_col",count_bonusPricel);

		//PlayerPrefs.SetInt("user_money",money);


	}
	public void PlayerLost(){
		SCLock = true;
		//print ("PlayerLost");
		GameGUI	mGameGUI = GameObject.FindGameObjectWithTag ("GameCanvas").GetComponent<GameGUI>();

		mGameGUI.PlayerLost ();
		//wonDialog.SetActive(true);
		//lostDialog.SetActive (true);
	}



	

	//-----------------------------------------------


}
