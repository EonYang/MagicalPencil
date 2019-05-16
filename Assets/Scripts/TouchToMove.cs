using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchToMove : MonoBehaviour {

    public static TouchToMove Instance;

	public Text m_Text;
	public Text player_Text;
	public float speed = 1f;
	private Camera cam;
	//private GameObject sketchBook;

	private GameObject player;
	private Transform playerTrans;
	private Rigidbody2D playerBody2d;
	private bool touchDown = false;
	private float xPosition = 0;
	private float initialScaleX = 0;
	float newScaleX;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
	{
        cam = Camera.main;
        player = GameObject.FindWithTag("Player");
		playerTrans = player.GetComponent<Transform>();
		playerBody2d = player.GetComponent<Rigidbody2D>();
		initialScaleX =  Mathf.Abs(playerTrans.localScale.x);
		newScaleX =initialScaleX;
		//sketchBook = GameObject.FindWithTag("SketchBook");
	}

	void Update()

    {

        TryMove();

            
    }

    void TryMove()
    {
        if (Input.GetMouseButton(0))
        {
        Vector3 playerPos = cam.WorldToScreenPoint(playerTrans.position);

        float playerPosX = playerPos.x;
        xPosition = Input.mousePosition.x;

        bool noUIcontrolsInUse = !IsPointerOverUIObject();

        touchDown = Input.GetMouseButton(0) && noUIcontrolsInUse;

        var right = playerPosX < xPosition -10 && touchDown && !UIManager.Instance.SketchBook.activeSelf;
        var left = playerPosX > xPosition + 10 && touchDown && !UIManager.Instance.SketchBook.activeSelf;
        var velX = speed * PlayerContext.Instance.speedFactor;

        if (right || left) newScaleX = left ? -  initialScaleX :  initialScaleX;
        if (right || left) velX *= left ? -1 : 1;
        else velX = 0;
      
        playerBody2d.velocity = new Vector2(velX, playerBody2d.velocity.y);
        playerTrans.localScale = new Vector3(newScaleX, playerTrans.localScale.y, playerTrans.localScale.z);
        }
    }

    private bool IsPointerOverUIObject() {
     PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
     eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
     List<RaycastResult> results = new List<RaycastResult>();
     EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
     return results.Count > 0;
 }

}
