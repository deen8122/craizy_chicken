using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
public class Ball : MonoBehaviour {

	public float speed = 2;
	private Rigidbody2D rigidbody;
	public GameObject line;
	private Vector3 lastPos;
	private Vector3 curPos;
	void Start () {
		this.rigidbody = GetComponent<Rigidbody2D> ();
		rigidbody.velocity = new Vector2 (Random.Range(0f, -1f),Random.Range(-1f, 1f));


	}

	void FixedUpdate () {




		if (GameManager.instance.showDirection) {
			line.SetActive (true);
			curPos = (transform.position - lastPos) * 100;
			//lineRenderer.SetPosition(0, transform.position);
			//line.transform.localEulerAngles = transform.forward;
			//Quaternion target = Quaternion.Euler(rigidbody.velocity.x , rigidbody.velocity.y , 0f);
			//print(curPos.x + ":"+curPos.y+ ":"+curPos.z);

			float angle = Mathf.Atan2 (curPos.y, curPos.x) * Mathf.Rad2Deg;
			line.transform.rotation = Quaternion.Euler (0, 0, angle);
			//line.transform.localEulerAngles = rigidbody.velocity;
			//lineRenderer.SetPosition(1, transform.forward * 20 + transform.position);
			lastPos = transform.position;
		} else {

			line.SetActive (false);
		}
	}

	void Update () {


		if (GameManager.instance.bonusSLow) {
			speed = 1.2f;
		} 
		else if(GameManager.instance.showDirection){
			speed = 1.5f;
		}
		else {
			speed = 2;
		}
		rigidbody.velocity = rigidbody.velocity.normalized * speed;	
	}
	void OnCollisionEnter2D(Collision2D col){


		if(col.gameObject.tag == "bomb" ){
			//lifeTime = 0;
			Destroy(this.gameObject);
		}


	}
}

