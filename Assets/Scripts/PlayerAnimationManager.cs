﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour {

	public static PlayerAnimationManager Instance;

	//private GameObject player;

	public Animator animator;
	private Rigidbody2D body2d;

	private float velo = 0f;

    [SerializeField]
    private GameObject shadow;

	private void Awake()
	{
		if (Instance == null)
        {
            Instance = this;

            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Destroy(gameObject);
        }

	//	//player = ;


	}

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
        body2d = GetComponent<Rigidbody2D>();

        Init();
		
	}

    public void Driving()
    {
        PlayerContext.Instance.riding = 2;
        animator.SetInteger("riding", 2);
    }

    public void Cycling()
    {
        PlayerContext.Instance.riding = 1;
        animator.SetInteger("riding", 1);
    }

    public void SkateBoarding()
    {
        PlayerContext.Instance.riding = 3;
        animator.SetInteger("riding", 3);
    }

    public void Walking()
    {
        PlayerContext.Instance.riding = 0;
        animator.SetInteger("riding", 0);
    }
	
	// Update is called once per frame
	void Update () {

        //if (PlayerContext.Instance.motorcycling != animator.GetBool("cycling"))
        //{
        //    animator.SetBool("cycling", PlayerContext.Instance.motorcycling);
        //    Debug.Log("setting cycling");
        //}
        //else 
        if(PlayerContext.Instance.riding == 0)
        {
         velo = Mathf.Abs(body2d.velocity.x);

        bool isWalking = velo > 0.01 ? true : false;

            animator.SetBool("walking", isWalking);
        }
                
	}

    public void Init()
    {
        animator.SetBool("dead", false);
        animator.SetBool("hurt", false);
    }
    public void Die()
    {
        //if(animator.GetCurrentAnimatorClipInfo())
        animator.SetBool("dead", true);
    }

    public IEnumerator TakingDamage(float t = 0.1f)
    {
        if (!animator.GetBool("dead"))
        {
            animator.SetBool("hurt", true);
            yield return new WaitForSeconds(t);
            animator.SetBool("hurt", false);
        }


    }
}
