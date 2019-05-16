using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySingleton : MonoBehaviour {

	public void SelfDestroy()
    {
        Debug.Log("singleton destroy called");
        Destroy(transform.parent.gameObject);
    }
}
