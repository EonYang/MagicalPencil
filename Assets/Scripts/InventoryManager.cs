using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

	public static InventoryManager Instance;
	public int SlotsNum = 3;
	private GameObject inventoryUI;


	public List<Item> Inventory = new List<Item>();
	private List<GameObject> slots = new List<GameObject>();
    private List<Sprite> sprites = new List<Sprite>();

	[SerializeField]
	private GameObject slotPrefab;

	private void Awake()
	{
		if(Instance == null){
			Instance = this;
			DontDestroyOnLoad(gameObject);
           
		} else{
            InventoryManager.Instance.Init();
            Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		Init();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Init (){
		inventoryUI = GameObject.FindWithTag("InventoryUI");
        RestoreInventory();
		//for (int i = 0; i < SlotsNum; i++)
		//{
		//	slots.Add(Instantiate(slotPrefab, inventoryUI.transform));
		//}
	}

	public void AddItem(int id, Sprite sprite){

		if (slots.Count < SlotsNum)
		{
			Item item = ItemEventManager.Instance.ItemData[id];

			Inventory.Add(item);
            sprites.Add(sprite);
            slots.Add(Instantiate(slotPrefab, inventoryUI.transform));

			Image uiImage = slots[Inventory.Count - 1].transform.Find("ItemImage").GetComponent<Image>();

			uiImage.sprite = sprite;
			uiImage.preserveAspect = true;
			InventoryItemDragable dragable = slots[Inventory.Count - 1].transform.Find("ItemImage").gameObject.AddComponent<InventoryItemDragable>();
			dragable.myId = id;
            dragable.mySprite = sprite;
		}else{
            ErrorBackIsFull();

        }
	}

    public void RestoreInventory()
    {
        if (Inventory.Count > 0)
        {
            slots.Clear();
            for (int i = 0; i < Inventory.Count; i++)
            {
                Sprite sprite = sprites[i];
                Item item = Inventory[i];

                slots.Add(Instantiate(slotPrefab, inventoryUI.transform));

                Image uiImage = slots[slots.Count - 1].transform.Find("ItemImage").GetComponent<Image>();
                uiImage.sprite = sprite;
                uiImage.preserveAspect = true;
                InventoryItemDragable dragable = slots[slots.Count - 1].transform.Find("ItemImage").gameObject.AddComponent<InventoryItemDragable>();
                dragable.myId = item.Id;
            }
        }
    }

    public void DropItem(int id){
		int index = -1;
		for (int i = 0; i < Inventory.Count; i++)
		{
			if (Inventory[i].Id == id)
			{
				index = i;
			}
		}

		if (index != -1)
		{
            Sprite sprite = slots[index].transform.Find("ItemImage").GetComponent<Image>().sprite;
			ItemSpawner.Instance.SpanwItem(Inventory[index], sprite);
			Inventory.RemoveAt(index);
			Destroy(slots[index]);
			slots.RemoveAt(index);    
            sprites.RemoveAt(index);
        }
              
	}

    public IEnumerator DestroyItemById(int id)
    {
        yield return new WaitForEndOfFrame();
        int index = -1;
        for (int i = 0; i < Inventory.Count; i++)
        {
            if (Inventory[i].Id == id)
            {
                index = i;
            }
        }

        if (index != -1)
        {
            Inventory.RemoveAt(index);
            Destroy(slots[index]);
            slots.RemoveAt(index);
            sprites.RemoveAt(index);
        }

    }

    public IEnumerator DestroyItemByIndex(int index)

    {
           yield return new WaitForEndOfFrame();
            Inventory.RemoveAt(index);
            Destroy(slots[index]);
            slots.RemoveAt(index);    
            sprites.RemoveAt(index);
              
    }

    public void AdjustDurability(int id, int change)
    {
        int index = -1;
        for (int i = 0; i < Inventory.Count; i++)
        {
            if (Inventory[i].Id == id)
            {
                index = i;
            }
        }

        if (index != -1)
        {
            Inventory[index].Durability += change;
        }

        if (Inventory[index].Durability <= 0)
        {
            StartCoroutine(DestroyItemByIndex(index));
        }
    }


	public void ErrorBackIsFull(){
		UIManager.Instance.ShowTip("I can't carry more, unless I have a bigger backpack.");
	}
}
