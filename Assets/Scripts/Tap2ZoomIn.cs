using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tap2ZoomIn : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseUp()
    {
        if (PlayerContext.Instance.focus == gameObject)
        {
            PlayerContext.Instance.focus = null;
        }
        else
        {
            PlayerContext.Instance.focus = gameObject;
        }
    }
}
