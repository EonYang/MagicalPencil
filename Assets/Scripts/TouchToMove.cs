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
    public float speedFactor = 1f;
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
		initialScaleX = playerTrans.localScale.x;
		newScaleX = initialScaleX;
		//sketchBook = GameObject.FindWithTag("SketchBook");
	}

	void Update()

    {
		Vector3 playerPos = cam.WorldToScreenPoint(playerTrans.position);

		float playerPosX = playerPos.x;
		xPosition = Input.mousePosition.x;

		bool noUIcontrolsInUse = !EventSystem.current.IsPointerOverGameObject();

		touchDown = Input.GetMouseButton(0) && noUIcontrolsInUse;

		var right = playerPosX < xPosition -10 && touchDown && !UIManager.Instance.SketchBook.activeSelf;
		var left = playerPosX > xPosition + 10 && touchDown && !UIManager.Instance.SketchBook.activeSelf;
        var velX = speed * speedFactor;

		if (right || left) newScaleX = left ? -initialScaleX : initialScaleX;
        if (right || left) velX *= left ? -1 : 1;
        else velX = 0;
      
		playerBody2d.velocity = new Vector2(velX, playerBody2d.velocity.y);
		playerTrans.localScale = new Vector3(newScaleX, playerTrans.localScale.y, playerTrans.localScale.z);

            
    }
    
}
