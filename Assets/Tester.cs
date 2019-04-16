using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class Tester : MonoBehaviour {

	[SerializeField]
	private Sprite testSprite;
	private int testItemId = 295;

	// Use this for initialization

	public void SpawnTestItem( int id){
		ItemEventManager.Instance.ItemData[id].sprite = testSprite;
		//ItemSpawner.Instance.SpanwItem(ItemEventManager.Instance.ItemData[id]);
        MethodInfo mi = ItemSpawner.Instance.GetType().GetMethod("SpanwItem");
        mi.Invoke(ItemSpawner.Instance, new object[] { ItemEventManager.Instance.ItemData[id], testSprite } );
    }

}
