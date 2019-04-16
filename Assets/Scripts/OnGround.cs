using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

public class OnGround : MonoBehaviour {
	

    public GameObject UIPrefab;


    public GameObject ActionButtonPrefab;

    private List<Button> actionBtns = new List<Button>();

	private BoxCollider2D playerCollider;
	private BoxCollider2D thisTrigger;
	private GameObject childCollider;
    
	private GameObject parentCanvas;
    
	private GameObject UIobj;

	private RectTransform UIRect;
	private RectTransform parentRect;

	private Text itemNameUI;
	private int myId;
	private Item me;
	private Button Btn_1;
    private Sprite mySprite;

    private BoxCollider2D collider;

	[SerializeField]
	private float yOffset = 0.01f;

	void Start () {
		thisTrigger = gameObject.AddComponent<BoxCollider2D>();
        thisTrigger.isTrigger = true;

        gameObject.AddComponent<Rigidbody2D>();

		playerCollider = GameObject.FindWithTag("Player").GetComponent<BoxCollider2D>();

		childCollider = transform.Find("Collider").gameObject;
		childCollider.GetComponent<BoxCollider2D>().size = thisTrigger.size;

		parentCanvas = GameObject.FindWithTag("HUD");
		parentRect = parentCanvas.GetComponent<RectTransform>();

        Transform tran = parentCanvas.transform;
        tran.localPosition += Vector3.forward * 0.1f;

		UIobj = Instantiate(UIPrefab, tran );      
		UIRect = UIobj.GetComponent<RectTransform>();


		Physics2D.IgnoreLayerCollision(9, 10);
		UIobj.SetActive(false);

		itemNameUI = UIobj.transform.Find("ItemName/Text").GetComponent<Text>();

        collider = GetComponent<BoxCollider2D>();

	}

    private void Update()

    {

        if (UIobj.activeSelf)
        {
            UIobj.SetActive(!UIManager.Instance.SketchBook.activeSelf);
            ShowAndPositionMenu();
        }
    }
    


	public void Init(Item item){
		me = item;
		myId = item.Id;
		itemNameUI.text = me.Name;
		UIobj.name = me.Name;

        if (item.AutoTriggered)
        {
            StartCoroutine(AutoTrigger());
        }

        if (item.CanPickUp)
        {
            mySprite = GetComponent<SpriteRenderer>().sprite;
            AddPickupBtn(mySprite);
        } 
        else {
            AddGeneralUseBtn(item);
        }

        AddDestroyBtn();


    }

    private void AddPickupBtn(Sprite sprite)
    {
        actionBtns.Add(Instantiate(ActionButtonPrefab, UIobj.transform).GetComponent<Button>());
        actionBtns[actionBtns.Count - 1].GetComponentInChildren<Text>().text = "Pick Up";
        actionBtns[actionBtns.Count - 1].onClick.AddListener(() => {
            PickUpMe(myId, sprite);
        });
    }

    private void AddGeneralUseBtn(Item item)
    {
        if (item.ActionOnGroundName != "")
        {
            actionBtns.Add(Instantiate(ActionButtonPrefab, UIobj.transform).GetComponent<Button>());
            actionBtns[actionBtns.Count - 1].GetComponentInChildren<Text>().text = item.ActionOnGroundName;
            actionBtns[actionBtns.Count - 1].onClick.AddListener(() =>
            {
                Use();
            });
        }

    }

    private void AddDestroyBtn()
    {
        actionBtns.Add(Instantiate(ActionButtonPrefab, UIobj.transform).GetComponent<Button>());
        actionBtns[actionBtns.Count - 1].GetComponentInChildren<Text>().text = "Destroy";
        actionBtns[actionBtns.Count - 1].onClick.AddListener(() => DestroySelf());
    }



	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("1");
        if (collision.gameObject.tag == "Player")
        {
			ShowAndPositionMenu();
			UIobj.SetActive(true);
        }
	}
    
	private void OnTriggerStay2D(Collider2D collision)
	{
        if (collision.gameObject.tag == "Player")
        {
			
        }
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		Debug.Log("3");
        if (collision.gameObject.tag == "Player")
        {
			UIobj.SetActive(false);
        }

	}

	private void ShowAndPositionMenu()

    {
        Vector3 itemPos = Camera.main.WorldToViewportPoint(this.transform.position + new Vector3(0f,-collider.size.y/2*transform.localScale.y,0f));
		itemPos = new Vector3(itemPos.x * parentRect.sizeDelta.x - parentRect.sizeDelta.x / 2, itemPos.y * parentRect.sizeDelta.y - parentRect.sizeDelta.y / 2 + yOffset, itemPos.z);
        UIRect.anchoredPosition = itemPos;
    }

	public void PickUpMe(int id, Sprite sprite){
		if (InventoryManager.Instance.Inventory.Count < InventoryManager.Instance.SlotsNum)
		{
            Item item = ItemEventManager.Instance.ItemData[id];
            if (item.StoryOnPickUp != "")
            {
                UIManager.Instance.ShowTip(item.StoryOnPickUp);
            }
            InventoryManager.Instance.AddItem(id, sprite);
			Destroy(gameObject, 0.1f);
			Destroy(UIobj, 0.1f);
		} else{
			InventoryManager.Instance.ErrorBackIsFull();
		}
	}

    public void Use()
    { 
        if (!PuzzleManager.Instance.TrySolvePuzzle(myId, mySprite ))
        {
            ItemEventManager.Instance.InvokeItemFunction(myId, mySprite, me.ActionOnGroundFunction, gameObject);
        }
    }

    public void DestroySelf(){
		Destroy(gameObject, 0.1f);
        Destroy(UIobj, 0.1f);
	}

    public IEnumerator AutoTrigger()
    {
        Debug.Log("auto triggering");
        yield return new WaitForSeconds(2f);
        if (!PuzzleManager.Instance.TrySolvePuzzle(myId, mySprite ))
        {
            ItemEventManager.Instance.InvokeItemFunction(myId, mySprite, me.ActionAutoTriggerFunction, gameObject);
        }

    }

    public void AdjustDurability(int change)
    {
        me.Durability += change;
        if (me.Durability <= 0)
        {
            Destroy(UIobj, 0.1f);
            Destroy(gameObject, 0.1f);
        }
    }

}
