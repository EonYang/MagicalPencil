//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Networking;
//using UnityEngine.UI;
//using UnityEngine.Windows;
//using System.Runtime.Serialization.Formatters.Binary;

public class drawHull : MonoBehaviour
{

    public GameObject lineGeneratorPrefeb;
	public GameObject hullPrefab;
    public GameObject magicPencilBtn;
    private List<GameObject> toDestroy = new List<GameObject>();

    private LineRenderer currentRenderer;
    private int pointCount = 0;

    public bool isMouseOvering = false;
    public bool isDrawing = false;

    void Update()
    {

        // 1. wait to draw first line, instantiate a line renderer at zero.
        if (Input.GetMouseButtonDown(0) && isMouseOvering)
        {
            isDrawing = true;
            createNewRenderer();
            pointCount = 0;
        }
        else if (Input.GetMouseButtonUp(0))
        {
			DrawHull(currentRenderer);
            StartCoroutine(stopDrawing());
        }

        // 2. if mousePressed(mouse must inside the canvas), added points to the 1st line renderer.

        if (Input.GetMouseButton(0) && isMouseOvering)
        {
            var Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(Ray, out hit))
            {
                // the prefab mast uncheck "use world space"
                addPointsToCurrentRenderer(hit.point + Vector3.back * 0.1f - currentRenderer.transform.position);
            }
        }


    }

    private IEnumerator stopDrawing()
    {
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

    private void createNewRenderer()
    {
        GameObject lineGen = Instantiate(lineGeneratorPrefeb);
        lineGen.transform.parent = gameObject.transform;
        currentRenderer = lineGen.GetComponent<LineRenderer>();
        toDestroy.Add(lineGen);
    }

    private void addPointsToCurrentRenderer(Vector3 newPoint)
    {
        currentRenderer.positionCount = pointCount + 1;
        currentRenderer.SetPosition(pointCount, newPoint);
        pointCount++;
    }



    // managing canvas

    public void Activate(bool bol)
    {
        switch (bol)
        {
            case true:
                gameObject.SetActive(true);
                magicPencilBtn.SetActive(false);
                pointCount = 0;
                break;
            case false:
                StartCoroutine(setCanvasInvisible());
                break;
        }
    }

    //public void cancelDrwaing()
    //   {
    //       StartCoroutine(setCanvasInvisible());
    //   }

    //public void hideCanvas (){

    //}

    private IEnumerator setCanvasInvisible()
    {
        yield return new WaitForEndOfFrame();
        magicPencilBtn.SetActive(true);
        destroyAllLineGens();
        gameObject.SetActive(false);
    }

    public void destroyAllLineGens()
    {
        foreach (var lineGen in toDestroy)
        {
            Destroy(lineGen, 0.3f);
        }
        toDestroy = new List<GameObject>();
    }

	private void DrawHull(LineRenderer renderer){
		int skip = 3;
		int posCount = renderer.positionCount;

		Mesh mesh = new Mesh();



		Vector3[] poss = new Vector3[posCount];

		renderer.GetPositions(poss);

		mesh.vertices = poss;

		List<int> tris = new List<int>();
        

		for (int i = 2+skip; i < posCount; i+=skip)
		{
			tris.Add(0);
			tris.Add(i - skip);
			tris.Add(i);
		}

		mesh.triangles = tris.ToArray();

		Transform transform = renderer.gameObject.transform;
		transform.position += Vector3.back * 0.1f;

		GameObject hull = Instantiate(hullPrefab, transform);
		hull.transform.parent = gameObject.transform;
		toDestroy.Add(hull);

        MeshFilter meshfil = hull.GetComponent<MeshFilter>();
        meshfil.sharedMesh = mesh;


	}

}
