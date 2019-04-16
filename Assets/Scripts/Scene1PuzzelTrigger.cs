using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1PuzzelTrigger : MonoBehaviour {

   

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player"){
			UIManager.Instance.MagicalPencilHighlighter.SetActive(true);
			PlayerContext.Instance.inPuzzleArea = 1;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
        {
			UIManager.Instance.MagicalPencilHighlighter.SetActive(false);
			PlayerContext.Instance.inPuzzleArea = 0;
        }
	}

}
