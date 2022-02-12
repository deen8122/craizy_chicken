using UnityEngine;
using System.Collections;
using Assets.Scripts;
public class CameraManager : MonoBehaviour {
	public static GameObject superCat;

//	private supercat msupercat;
	void Start () {
			Time.timeScale = 1;
		superCat = GameObject.FindGameObjectWithTag ("supercat");
	//	msupercat = superCat.GetComponent<supercat> ();
		//GameManager.CurrentGameState = GameState.BOMB_FLYING;
		
	}
	void Update()
	{
		
		if (Input.GetMouseButtonDown (0)) {
			OnMouseDown(new Vector3(0,0,0));
		}
		if (Input.GetMouseButtonUp (0)) {
			onMouseUp();
		}
	}
	public bool OnMouseDown(Vector3 mousePos){
		//print ("OnMouseDown");
		//Time.timeScale = 0.1f;
		//Time.fixedDeltaTime = 0.1f;
	//	msupercat.TouchDown(mousePos);
		

		return true;
	}
	public void onMouseUp(){
		//Time.fixedDeltaTime = 1f;
		//msupercat.TouchUp();

	}
}
