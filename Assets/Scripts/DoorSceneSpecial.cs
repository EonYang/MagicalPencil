using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSceneSpecial : MonoBehaviour {

    GameObject door;
	// Use this for initialization
	void Start () {
        door = ItemSpawner.Instance.SpanwItem(ItemEventManager.Instance.ItemData[93], ItemEventManager.Instance.ItemData[93].sprite);
        door.transform.position = door.transform.position + new Vector3(-3, 0, 0);
        UIManager.Instance.ShowTip(ItemEventManager.Instance.ItemData[93].StoryOnUse);
        PlayerContext.Instance.inDesert = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
