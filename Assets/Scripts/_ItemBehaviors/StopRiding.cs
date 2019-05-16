using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopRiding : MonoBehaviour {

    // Use this for initialization
    public Item me;

	void Start () {
        Physics.queriesHitTriggers = true;
	}
	
    private void OnMouseDown()
    {
        StopRidingAndDestroySelf();
    }

    public void StopRidingAndDestroySelf()
    {
        Debug.Log("called 1");
        ItemEventManager.Instance.StopRide( me.SizeH);
        Destroy(this);
    }
}
