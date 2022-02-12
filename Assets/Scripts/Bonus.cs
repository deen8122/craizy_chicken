using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
public class Bonus : MonoBehaviour {
	public float lifeTime = 10f;
	public int typeBonus = 0;
	private float startTime = 0;

	public GameObject bomb;
	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if ((Time.time - startTime) > lifeTime) {
			Destroy(this.gameObject);

		}

		if (direction !=null) {
			
			this.transform.Translate(direction * 0.05f, Space.World);   
		}

	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "supercat" ){
			print (typeBonus);
			if (typeBonus == 0) {
				GameManager.instance.startTimeBonusLine = Time.time;
				GameManager.instance.showDirection = true;
			}
			if (typeBonus == 1) {
				GameManager.instance.startTimeBonusSlow = Time.time;
				GameManager.instance.bonusSLow = true;
			}
			if (typeBonus == 2) {
				GameManager.instance.plusLife();
			}
			if (typeBonus == 3) {
				Instantiate (bomb, this.transform.position, Quaternion.identity);
			}
			//Деньги!!!
			if (typeBonus == 4) {
				GameManager.instance.plusMoney();

				AnimateCoin ();
				return;

			}
			Destroy(this.gameObject);
		}



	}



	private Vector3 direction;
	void AnimateCoin(){
		print ("AnimateCoin//////////////////////////");
		this.GetComponent<BoxCollider2D> ().enabled = false;
		GameObject Coin = GameObject.Find ("coin");
		Vector3 coinPos = Coin.transform.position;
		print (coinPos); 
		Vector3 moveTo = Camera.main.ScreenToWorldPoint (coinPos);
		direction = moveTo - this.transform.position;
		print (moveTo);

		print (direction);
		StartCoroutine(Die());
	}

	private IEnumerator Die()
	{

		yield return new WaitForSeconds( 3f);
		Destroy(this.gameObject);
	}


}
