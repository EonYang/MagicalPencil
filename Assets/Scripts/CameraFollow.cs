using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public int MinX = -13;
    public int MaxX = -3;

    private GameObject player;       //Public variable to store a reference to the player game object
    

    private Vector3 offset;         //Private variable to store the offset distance between the player and camera
	private Vector3 targetPos;
	private Vector3 moveTo;
	private Transform playerTrans;
	private float offsetAbsX;

	private void Awake()
	{
		player = GameObject.FindWithTag("Player");
		playerTrans = player.GetComponent<Transform>();
	}
	// Use this for initialization
	void Start()
    {  
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
		offsetAbsX = 1f;
		targetPos = player.transform.position + offset;
		moveTo = targetPos;

    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
	    
		PlayerContext.Instance.facingRightSide = playerTrans.localScale.x > 0 ? true : false;
		offset.x = PlayerContext.Instance.facingRightSide ? offsetAbsX : - offsetAbsX;
		targetPos = player.transform.position + offset;

		if (Mathf.Abs((transform.position.x - targetPos.x)) > 0.1){
            moveTo.x = Mathf.Clamp(transform.position.x + (targetPos.x - transform.position.x) / 30, MinX, MaxX);
			//Debug.Log(transform.position.x - targetPos.x);
        }
		    
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.


	    
		transform.position = moveTo;
    }
}
