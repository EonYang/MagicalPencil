using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour {

	public static ItemSpawner Instance;
	public GameObject spawnBase;
	public List<GameObject> Spawned;
	private GameObject player;
    
	[SerializeField]
	private float spriteSizeFactor = 0.003f;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
	}

	public void Start()
	{
        Init();
	}

	public GameObject SpanwItem( Item item, Sprite sprite){

        if (item.Prerequisite)
        {
            return null;
        }

        int direction = PlayerContext.Instance.facingRightSide  ? 1 : -1;
		GameObject toAttach = Instantiate(spawnBase, player.transform.position + Vector3.right * direction * 0.5f, player.transform.rotation) as GameObject;
		Spawned.Add(toAttach);
		GameObject newOne = Spawned[Spawned.Count - 1];
		float size = item.SizeH * spriteSizeFactor;
        Vector3 size3 = new Vector3(size, size, size);
		newOne.GetComponent<SpriteRenderer>().sprite = sprite;
		newOne.transform.localScale = size3;
        newOne.name = item.Name;

        if (item.StoryOnCreate != "")
        {
            UIManager.Instance.ShowTip(item.StoryOnCreate);
        }
		StartCoroutine(SetId(newOne, item));
        return newOne;

	}
    
	private IEnumerator SetId(GameObject toSet, Item item){
		yield return new WaitForEndOfFrame();
		toSet.GetComponent<OnGround>().Init(item);
	}

	public void Init(){
		player = GameObject.FindWithTag("Player");
	}
    
}
