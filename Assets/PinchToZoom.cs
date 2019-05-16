using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchToZoom : MonoBehaviour {

    // Use this for initialization
    //private float 
    private Camera cam;

    [SerializeField]
    private float speedFactor = 1;

    private Vector3 farEndDist;
    private Transform player;

	void Start () {
        cam = Camera.main;
        player = GameObject.FindWithTag("Player").transform;
        farEndDist = player.position - cam.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        TryZoom();
	}

    void TryZoom()
    {
        if (Input.touchCount == 2)
        {
            Touch t0 = Input.GetTouch(0);
            Touch t1 = Input.GetTouch(1);
            float newDist = Vector2.Distance(t0.position, t1.position);
            float prevDist = Vector2.Distance(t0.position - t0.deltaPosition, t1.position - t1.deltaPosition);

            float zoom = prevDist / newDist * speedFactor;

            float newFiedlOfView = Mathf.Clamp( cam.fieldOfView * zoom , 30, 42);
            cam.fieldOfView = newFiedlOfView;
            Debug.LogFormat("new dist: {0}, prev dist: {1}, new field: {2}, zoom: {3} ", newDist, prevDist, newFiedlOfView, zoom);
        }
    }
}
