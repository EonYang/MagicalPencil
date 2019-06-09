using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {

    // Use this for initialization
    public int attackPoint = 10;

    Rigidbody2D rb;
	void Start () {
        bool parentFacingRight = transform.parent.GetComponent<EnemyVelocity>().facingRight;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(parentFacingRight ? 10 : -10, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Hit Player");
            PlayerHP.Instance.AdjustHP(-attackPoint);
            StartCoroutine(PlayerAnimationManager.Instance.TakingDamage());
        }
    }
}
