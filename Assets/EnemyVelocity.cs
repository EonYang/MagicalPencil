using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVelocity : MonoBehaviour {

    // Use this for initialization
    public GameObject bulletPrefab;
    Transform player;
    Rigidbody2D rb;
    public bool facingRight = true;

    [SerializeField]
    GameObject bubble;

    Animator anim;

    bool shooting;
    float range;

    void Start()
    {
        range = Random.Range(2, 4);
        player = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        StartCoroutine(ShowHideBubble());
    }

    IEnumerator ShowHideBubble()
    {
        while (true)
        {
            float time = Random.Range(1, 10);
            yield return new WaitForSeconds(time);
            bubble.SetActive(true);
            yield return new WaitForSeconds(1);
            bubble.SetActive(false);    
        }
    }
	
	// Update is called once per frame
	void Update () {
        bool wasFacingRight = facingRight;
        if (!shooting)
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
                StartCoroutine(Shoot());
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

    IEnumerator Shoot()
    {
        shooting = true;
        anim.SetTrigger("Shoot");
        yield return new WaitForSeconds(1);
        GameObject bullet = Instantiate(bulletPrefab, transform);
        Destroy(bullet, 3f);
        StartCoroutine(ShootingEnd());
    }

    IEnumerator ShootingEnd()
    {
        yield return new WaitForSeconds(2);
        shooting = false;

    }



}
