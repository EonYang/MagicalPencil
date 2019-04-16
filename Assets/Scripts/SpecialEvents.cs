using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEvents : MonoBehaviour {
    
	//private Scene1Manager sceneManager;

	private void Awake()
	{
		//sceneManager = GetComponent<Scene1Manager>();
	}

	// Use this for initialization
	public void EnableElectric(bool bol){
		PlayerContext.Instance.hasElectricity = bol;
	}

	public void PlayMusic(bool bol)
    {
        PlayerContext.Instance.hasElectricity = bol;
    }
}
