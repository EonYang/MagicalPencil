using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior_AttackPlayerMono : MonoBehaviour {

    public Item me;
	
    Transform player;
    Rigidbody2D rb;
    public bool facingRight = true;

    public float attackDelay = 2f;

    //[SerializeField]
    //GameObject bubble;

    //Animator anim;

    bool attacking;
    float range;

    void Start()
    {
        range = Random.Range(0.5f, 1f);
        player = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        //StartCoroutine(ShowHideBubble());
    }

    //IEnumerator ShowHideBubble()
    //{
    //    while (true)
    //    {
    //        float time = Random.Range(1, 10);
    //        yield return new WaitForSeconds(time);
    //        bubble.SetActive(true);
    //        yield return new WaitForSeconds(1);
    //        bubble.SetActive(false);    
    //    }
    //}
    
    // Update is called once per frame
    void Update () {
        bool wasFacingRight = facingRight;
        if (!attacking)
        {
            Vector3 dist = player.transform.localPosition - transform.localPosition;

            if (dist.x < 0)
                {
                    facingRight = false;
                }
                else
                {
                    facingRight = true;
                }

            if (Mathf.Abs(dist.x) < range )
            {
                StartCoroutine(Attack());
            }
            else if (Mathf.Abs(dist.x) < 10)
            {   
                rb.velocity = new Vector3(dist.x / 2, 0, 0);
            }
        
        }

        if(wasFacingRight != facingRight)
        {
            transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1,1,1));
        }
    }

    IEnumerator Attack()
    {
        attacking = true;
        //anim.SetTrigger("Shoot");
        Vector3 moveTo = new Vector3(facingRight ? 0.1f : -0.1f, 0.05f, 0f);
        Vector3 moveBack = new Vector3(facingRight ? -0.1f : 0.1f, 0.02f, 0f);
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.03f);
            transform.localPosition += moveTo;
        }

        PlayerHP.Instance.AdjustHP(-me.DemageToPlayer/10);

        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.06f);
            transform.localPosition += moveBack;
        }

        yield return new WaitForSeconds(attackDelay);
        attacking = false;
    }

    private void OnMouseDown()
    {
        InventoryManager.Instance.UseFirstWeapon();
    }


}
