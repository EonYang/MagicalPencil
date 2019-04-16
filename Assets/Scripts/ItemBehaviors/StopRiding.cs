using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopRiding : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Physics.queriesHitTriggers = true;
		
	}
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    RaycastHit hit;
        //    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    if (Physics.Raycast(ray, out hit))
        //    {Debug.Log(hit.transform.gameObject.name);
        //        if (hit.transform.gameObject == gameObject)
        //        {


        //        }
        //    }
        //}
    }

    private void OnMouseDown()
    {
        Debug.Log("called 1");
        ItemEventManager.Instance.StopRide(gameObject);
        Destroy(this);
    }
}
