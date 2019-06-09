using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Based_Settings : MonoBehaviour {

    public static Scene_Based_Settings Instance;

    public Color UIcolor = Color.white;

    public int[] PuzzleIDs = new int[1] { 1 };

    public Vector2 playerStartPosition;
    private Transform player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }

    private void Start()
    {
        StartCoroutine(GameManager.Instance.GetPuzzelData(1));
        player = GameObject.FindWithTag("Player").transform;
        player.transform.position = new Vector3(playerStartPosition.x, playerStartPosition.y, 0);

        if (SceneManager.GetActiveScene().name != "Level_Desert")
        {
            UIManager.Instance.HydratedBar.transform.parent.gameObject.SetActive(false);
            PlayerContext.Instance.inDesert = false;
        }
        else
        {
            UIManager.Instance.HydratedBar.transform.parent.gameObject.SetActive(true);
            PlayerContext.Instance.inDesert = true;
            if (PlayerContext.Instance.riding != 0)
            {
                player.gameObject.GetComponent<StopRiding>().StopRidingAndDestroySelf();
            }
        }



        //Vector3 direction = Vector3.one;
        //direction.x = PlayerContext.Instance.facingRightSide ? 1 : -1;

        //direction = Vector3.Scale(player.transform.localScale, direction);
        //player.transform.localScale = direction;
        StartCoroutine(LateInit());

    }

    private IEnumerator LateInit()
    {
        yield return 0;
        PlayerAnimationManager.Instance.Init();
        UIManager.Instance.Init();
    }

}
