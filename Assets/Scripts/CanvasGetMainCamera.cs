using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGetMainCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Canvas>().worldCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
