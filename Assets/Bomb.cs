using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
	private float startTime = 0;
	private float lifeTime = 4f;
	private GameObject explosion;
	private GameObject face;
	public float delay = 0f;
	private bool isIgnore = false;
	void Start () {
		startTime = Time.time;
		explosion  = transform.Find("Explosion").gameObject; //GameObject.Ch.Find ("Explosion");
		face  =transform.Find("BombFace").gameObject; //GameObject.Find ("BombFace");
		explosion.SetActive (false);
		face.SetActive (true);
	}

	// Update is called once per frame
	void Update () {
		if (isIgnore)
			return;
		if ((Time.time - startTime) > lifeTime) {
			isIgnore = true;
			//Destroy(this.gameObject);
			this.gameObject.GetComponent<Rigidbody2D>().velocity =new Vector2(0,0);
			explosion.SetActive (true);
			face.SetActive (false);
			//Destroy (gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay); 
			StartCoroutine(Die());


		}
	}

	private IEnumerator Die()
	{
		
		yield return new WaitForSeconds( 1f);
		//explosion.SetActive (false);
		//Destroy(explosion);
		//Destroy(face);
		Destroy(this.gameObject);
	}

	void OnCollisionEnter2D(Collision2D col){
		
	
		if(col.gameObject.tag == "ball" ){
			lifeTime = 0;
		}


	}

}
