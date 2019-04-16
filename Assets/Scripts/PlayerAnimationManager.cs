using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour {

	//public static PlayerAnimationManager Instance;

	//private GameObject player;

	private Animator animator;
	private Rigidbody2D body2d;

	private float velo = 0f;

	//private void Awake()
	//{
	//	if (Instance == null)
 //       {
 //           Instance = this;
 //           DontDestroyOnLoad(gameObject);
 //       }
 //       else
 //       {
 //           Destroy(gameObject);
 //       }

	//	//player = ;


	//}

	// Use this for initialization
	void Start () {

		animator = GetComponent<Animator>();
        body2d = GetComponent<Rigidbody2D>();
		
	}
	
	// Update is called once per frame
	void Update () {

        if (PlayerContext.Instance.motorcycling != animator.GetBool("cycling"))
        {
            animator.SetBool("cycling", PlayerContext.Instance.motorcycling);
            Debug.Log("setting cycling");
        }
        else if(!PlayerContext.Instance.motorcycling)
        {
         velo = Mathf.Abs(body2d.velocity.x);

        bool isWalking = velo > 0.01 ? true : false;

            animator.SetBool("walking", isWalking);
        }


		
	}
}
