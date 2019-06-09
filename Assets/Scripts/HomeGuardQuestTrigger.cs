using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeGuardQuestTrigger : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("enter tigger to Level_Desert");
            StartCoroutine(GameManager.Instance.GoToNextLevel("Level_Desert"));
        }

    }
}
