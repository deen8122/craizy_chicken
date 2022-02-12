using System;
using UnityEngine;
using System.Collections;
using Assets.Scripts;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Chicken : MonoBehaviour {

public float JumpSpeed = 10;
	public float speed = 5;
	public int bombCol = 0;
	//public GameObject  bomb;
	protected GameManager mGameManager = null;
	protected Rigidbody2D rigidbody2D;
	public float ThrowSpeed = 10;
	protected int doubleShoot = 0;
	Vector3 location;
	Vector3 pos;
	Vector3 posStart;
	Vector3 delta;
	private float distance = 1;
	private float MaxL = 1;
	public ParticleSystem collisionEffect;
	//public GameObject circleSmall = null;
	public int ACTION = 0;

	//public int Life = 100;
	private bool ignoreCollision;



//	private GameObject pricel = null;

	private Vector3 different;
	//private GameObject lighting = null;
	public Sprite dieSprite;
	//private Text helthtext;
	private bool flock = false;
	public int lifeN = 3;
	private GameObject face = null;	
	private float startTime;
	GameGUI	mGameGUI;  
	SpriteRenderer faceSprite;
	private Animator anim;
	void Start () {
		ignoreCollision = false;
		collisionEffect.Stop ();
		//base.Start ();
	
		print ("supercat Start()");
		GameObject gm = GameObject.FindGameObjectWithTag ("gm");
		mGameManager = GameManager.instance;

	
		rigidbody2D = GetComponent<Rigidbody2D> ();
		GameObject ht = GameObject.Find ("helthtext");
		face = GameObject.Find ("Face");
		anim = face.GetComponent<Animator> ();
		anim.SetBool("isrun",false);
		faceSprite = face.GetComponent<SpriteRenderer> ();
		//lifeN = PlayerPrefs.GetInt("lifeN",1);
			mGameGUI = GameObject.FindGameObjectWithTag ("GameCanvas").GetComponent<GameGUI>();
		mGameGUI.SetHelthText (lifeN);
		SetLookRight (false);
	}









	public void JoystickUp(bool isLeft){
		anim.SetBool("isrun",false);
		if (isLeft) {
			print ("Left Up? shoot.....");
		} else {
			print ("Right Up? shoot.....");
			//Shoot ();
		}

	}
	public void JoystickDown(bool isLeft){
		anim.SetBool("isrun",true);
		this.TouchDown (new Vector3 (0, 0, 0));

	}

	public void EndGame(){
		 PlayerPrefs.SetInt("lifeN",lifeN);
		// PlayerPrefs.SetInt("life",Life);
	}


	void Die(){
		GetComponent<Animator> ().enabled = false;
		GetComponent<SpriteRenderer>().sprite = dieSprite;
	}
	public void Stope(){

		flock = true;
		TouchUp();
	}
	private void BlinklStop(){
		faceSprite.enabled = true;

	}
	private void BlinklStart(){
		StartCoroutine(DoBlinks(1f, 0.1f));

	}




	IEnumerator DoBlinks(float duration, float blinkTime) {
		while (duration > 0f) {
			duration -= Time.deltaTime;
            faceSprite.enabled = !faceSprite.enabled;
			if (ignoreCollision) {
				yield return new WaitForSeconds (blinkTime);
			}
		}
		faceSprite.enabled = true;
	}




	// Update is called once per frame
	void Update () {
		//print ("StartGame()");
		if (ignoreCollision) {
			if ((Time.time - startTime) > 3) {
				ignoreCollision = false;
				BlinklStop ();
			}
		}
		if(ACTION == 1){
			posStart = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			different = transform.position;
			//posStart = Input.mousePosition;

			ACTION = 2;
		}
		if (ACTION == 2) {

			pos = gameObject.transform.position; 
			//location = Camera.main.ScreenToWorldPoint (Input.mousePosition)+posStart;
			location = Camera.main.ScreenToWorldPoint (Input.mousePosition)-(pos -different);
		//	location = Input.mousePosition;
			location = new Vector2(this.speed*mGameManager.joystickLX,this.speed*mGameManager.joystickLY);
			//we will let the user pull the bird up to a maximum distance
			distance = Vector3.Distance (location, posStart);
			if (distance > MaxL) {
				//basic vector maths :)
				distance = MaxL;
				delta = (location - posStart).normalized * MaxL;
				//var maxPosition = (location - pos1).normalized * 0.2f;

			} else {
				delta =(location - posStart);
			}
			delta = new Vector2(mGameManager.joystickRX,mGameManager.joystickRY);
			if (mGameManager.loystickRactive) {
		//		pricel.SetActive (true);
			} else {
			//	pricel.SetActive (false);
			}
			//circleSmall.transform.position = pos+delta ;
			rigidbody2D.velocity = new Vector2(this.speed*mGameManager.joystickLX,this.speed*mGameManager.joystickLY) *speed/10;

				if ( mGameManager.joystickLX >0) {
					SetLookRight (false);
				} else {
					SetLookRight (true);
				}
			
			//this.transform.position = new Vector2 (this.speed*mGameManager.joystickX ,this.speed*mGameManager.joystickY);

		}

	}

	public void GUI_updateLife(){
		mGameGUI.SetHelthText (lifeN);
	}
	public void GUI_updateMoney(){
		mGameGUI.SetHelthText (lifeN);
	}


	void OnCollisionEnter2D(Collision2D col)
	{

		if(ignoreCollision){ return;}
		if (col.gameObject.GetComponent<Rigidbody2D>() == null) return;	
		if(col.gameObject.tag == "ball" ){
			ignoreCollision = true;
			startTime = Time.time;
			BlinklStart ();		
			collisionEffect.Play ();
			lifeN-=1;
			mGameGUI.SetHelthText (lifeN);

			if(lifeN<=0){
				//Die ();
				//mGameManager.ChickenEvent ("die");
				//helthtext.text ="0:0";
				RigidbodyConstraints2D fr = RigidbodyConstraints2D.None;
				this.GetComponent<Rigidbody2D>().constraints = fr;
				mGameManager.PlayerLost ();
			//SoundManager.instance.RandomizeSfx(destroySound1)			
			}
		}


	}


	/*
	 * Вызывается при начале установки прыжка
	 */
	public void TouchDown(Vector3 mousePos){
		//print("TouchDown");
		if (flock)
			return;
		//lighting.Play ("lighting");
		//lighting.SetBool ("run", true);
		//lighting.SetBool ("run", false);
		//posStart = Camera.main.ScreenToWorldPoint (mousePos);
	//	circleSmall.transform.position = gameObject.transform.position;
		if (mGameManager.SCLock == true)
			return;
		ACTION = 1;
		//pricel.SetActive (true);

		
	}
	/*
	 * Вызывается при начале установки прыжка
	 */
	public void TouchUp(){
		if (mGameManager.SCLock == true)
			return;
		if (flock)
			return;
	//	lighting.SetActive (false);
	//	lighting.SetActive (true);
		//lighting.SetBool ("run", false);
	//	print("TouchUp");
		ACTION = 0;
		Vector3 velocity = delta;
//		print (delta);
	//	print (distance);
		//GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x, velocity.y) *distance* JumpSpeed*(-1) ;
		//pricel.SetActive (false);
		//Time.timeScale = 1;
		//Time.fixedDeltaTime = 0.02F * Time.timeScale;
	
	
	}
	public void SetLookRight(bool f){

		if(f == false){
			this.transform.localScale = new Vector3 (-1,1,0);
	//		head.transform.localScale=new Vector3(-1,1,1);
//			this.GetComponent<Fire>().napravlenie = -1;
		}
		else {
			this.transform.localScale = new Vector3 (1,1,0);
		//	head.transform.localScale=new Vector3(1,1,1);
		//	this.GetComponent<Fire>().napravlenie = 1;
			
		}
	}

}
