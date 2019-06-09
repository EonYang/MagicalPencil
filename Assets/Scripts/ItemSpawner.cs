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

    public GameObject SpanwItem(Item item, Sprite sprite)
            //public GameObject SpanwItem(Item item, Sprite sprite, float xOffset = 0, float yOffset = 0)
    {

        if (item.Prerequisite)
        {
            return null;
        }

        int direction = PlayerContext.Instance.facingRightSide  ? 1 : -1;
        //Vector3 pos = player.transform.position + (Vector3.right + new Vector3(xOffset,yOffset,0)) * direction * 0.5f;
         Vector3 pos = player.transform.position + (Vector3.right) * direction * 0.5f;
		GameObject toAttach = Instantiate(spawnBase, pos, player.transform.rotation) as GameObject;
		Spawned.Add(toAttach);
		GameObject newOne = Spawned[Spawned.Count - 1];
		float size = Mathf.Clamp(item.SizeH, 20,200) * spriteSizeFactor;
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
