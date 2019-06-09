using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAllSingletonsOnStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
        LevelUtil.KillAllSingleton();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
