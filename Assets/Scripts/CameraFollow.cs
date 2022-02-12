using UnityEngine;
using System.Collections;
using Assets.Scripts;
public class CameraFollow : MonoBehaviour
{
	
	// Use this for initialization
	void Start()
	{
		//leftConstraint = Camera.main.ScreenToWorldPoint( new Vector3(0.0f, 0.0f, 0) ).x;
		//	rightConstraint = Camera.main.ScreenToWorldPoint( new Vector3(Screen.width, 0.0f, 0) ).x;
		
		
		StartingPosition = transform.position;
		minY = Screen.height/2;	
		//GameObject g = GameObject.Find("Destroyer_left");
		minCameraX = -999;	
		//g = GameObject.Find("Destroyer_right");
		maxCameraX = 999;
		minY= 2f * Camera.main.orthographicSize;
		cameraW = minY * Camera.main.aspect/2;
		minY /= 4;
		transform.position =new Vector3(0,0,0);// BirdToFollow.transform.position;
	}
	
	
	// Update is called once per frame
	void Update()
	{
		if (GameManager.CurrentGameState == GameState.BOMB_FLYING)
		{
			if (BirdToFollow != null) //bird will be destroyed if it goes out of the scene
			{
				birdPosition = BirdToFollow.transform.position;
				

				
				transform.position = new Vector3(birdPosition.x , birdPosition.y, StartingPosition.z);
				//}
				
			}
			else {
				////StartingPosition = transform.position;
				//IsFollowing = false;
				//offsetX = 0;
			}
			
		}
		else {
			//mainCamera.GetComponents<CameraMove>().minX;
			//StartingPosition = transform.position;
			///IsFollowing = false;
			//offsetX = 0;
		}
	}
	

	public Vector3 StartingPosition;
	public Vector3 birdPosition;
	public float cameraW = 1;
	public  float minCameraX = -33;
	public  float maxCameraX = 33;
	private float offsetX=0;
	private float minY = 0;
	public bool IsFollowing = true;
	public static Transform BirdToFollow;
}
