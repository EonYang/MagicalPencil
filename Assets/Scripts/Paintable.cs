//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Networking;
//using UnityEngine.UI;
//using UnityEngine.Windows;
//using System.Runtime.Serialization.Formatters.Binary;

public class Paintable : MonoBehaviour {

    public static Paintable Instance;
    
	public GameObject lineGeneratorPrefeb;
	public GameObject magicPencilBtn;
	public List<GameObject> toDestroy = new List<GameObject>();

	private LineRenderer currentRenderer;
	private int pointCount = 0;

	public bool isMouseOvering = false;
	public bool isDrawing = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Update () {
    
		// 1. wait to draw first line, instantiate a line renderer at zero.
		if(Input.GetMouseButtonDown(0) && isMouseOvering){
			isDrawing = true;
			CreateNewRenderer();
			pointCount = 0;
		} else if (Input.GetMouseButtonUp(0))
        {
			StartCoroutine(StopDrawing());
        }

		// 2. if mousePressed(mouse must inside the canvas), added points to the 1st line renderer.
        
		if (Input.GetMouseButton(0) && isMouseOvering)
		{
			var Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(Ray, out hit))
            {
				// the prefab mast uncheck "use world space"
				AddPointsToCurrentRenderer(hit.point+ Vector3.back * 0.1f - currentRenderer.transform.position);
            }         
		}


	}

	private IEnumerator StopDrawing(){
		yield return new WaitForEndOfFrame();
		isDrawing = false;
	}

	void OnMouseEnter()
    {
		isMouseOvering = true;
    }

	void OnMouseExit()
    {
		isMouseOvering = false;
    }

	private void CreateNewRenderer(){
		GameObject lineGen = Instantiate(lineGeneratorPrefeb);
		lineGen.transform.parent = gameObject.transform;
		currentRenderer = lineGen.GetComponent<LineRenderer>();
		toDestroy.Add(lineGen);
	}

	private void AddPointsToCurrentRenderer(Vector3 newPoint){
		currentRenderer.positionCount = pointCount + 1;
		currentRenderer.SetPosition(pointCount, newPoint);
		pointCount++;
	}

    

    // managing canvas

	public void Activate (bool bol){
		switch (bol)
		{
			case true:
				gameObject.SetActive(true);
                magicPencilBtn.SetActive(false);
                pointCount = 0;
				break;
			case false:
				StartCoroutine(SetCanvasInvisible());
				break;
		}
	}

	//public void cancelDrwaing()
 //   {
 //       StartCoroutine(setCanvasInvisible());
 //   }

	//public void hideCanvas (){
		
	//}

	private IEnumerator SetCanvasInvisible(){
        yield return new WaitForEndOfFrame();
		magicPencilBtn.SetActive(true);
		DestroyAllLineGens();
		gameObject.SetActive(false);
	}

	public void DestroyAllLineGens()
    {
        foreach (var lineGen in toDestroy)
        {
            Destroy(lineGen, 0.3f);
        }
        toDestroy = new List<GameObject>();
    }
  
}
