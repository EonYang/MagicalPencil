using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public int MinX = -13;
    public int MaxX = -3;

    private GameObject player;       //Public variable to store a reference to the player game object


    private Vector3 offset;         //Private variable to store the offset distance between the player and camera
    private Vector3 targetPos;
    private Vector3 moveTo;
    private Transform playerTrans;
    private float offsetAbsX;
    private int FOV = 41;
    private float initY;

    Camera cam;

    private void Awake()
    {

        player = GameObject.FindWithTag("Player");
        playerTrans = player.GetComponent<Transform>();
        cam = GetComponent<Camera>();
        initY = transform.localPosition.y;
    }
    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
        offsetAbsX = 1f;
        targetPos = player.transform.position + offset;
        moveTo = targetPos;

    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        if (PlayerContext.Instance.focus != null)
        {
            int targetFOV = 4;
            ZoomToTarget(targetFOV);

            targetPos = PlayerContext.Instance.focus.transform.position;
            MoveToTarget(targetPos.x, targetPos.y);
        }
        else
        {
            ZoomToTarget(FOV);

            PlayerContext.Instance.facingRightSide = playerTrans.localScale.x > 0 ? true : false;
            offset.x = PlayerContext.Instance.facingRightSide ? offsetAbsX : -offsetAbsX;
            targetPos = player.transform.position + offset;

            MoveToTarget(targetPos.x, initY);

        }


        transform.position = moveTo;
    }

    void ZoomToTarget(int targetFOV)
    {

        if (Mathf.Abs((cam.fieldOfView - targetFOV)) > 0.1)
        {
            float zoom;
            zoom = cam.fieldOfView + (targetFOV - cam.fieldOfView) / 30;
            cam.fieldOfView = zoom;
        }

    }
    void MoveToTarget(float x, float y)
    {
        targetPos.x = x;
        targetPos.y = y;

        if (Mathf.Abs((transform.position.x - targetPos.x)) > 0.01)
        {
            moveTo.x = Mathf.Clamp(transform.position.x + (targetPos.x - transform.position.x) / 30, MinX, MaxX);
        }

        if (Mathf.Abs((transform.position.y - targetPos.y)) > 0.01)
        {
            moveTo.y = transform.position.y + (targetPos.y - transform.position.y) / 30;

        }
    }
}
