using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItemDragable : MonoBehaviour,IBeginDragHandler,  IDragHandler, IEndDragHandler {

	private RectTransform UIRect;
	private RectTransform HudRect;
	private RectTransform InventoryPanelRect;
	//private int yOffset = 0;
	private Vector2 mouseStartPoint = Vector3.zero;
	private Vector2 UIStartPoint = Vector3.zero;

	public int myId;
    private Item me;

    private Button myBtn;
    public Sprite mySprite;


    private void Start()
	{
        me = ItemEventManager.Instance.ItemData[myId];
        myBtn = gameObject.AddComponent<Button>();
        myBtn.onClick.AddListener(() => Use());

        UIRect = GetComponent<RectTransform>();
		HudRect = GameObject.FindWithTag("HUD").GetComponent<RectTransform>();
		InventoryPanelRect = GameObject.FindWithTag("InventoryUI").GetComponent<RectTransform>();

	}

	public void OnBeginDrag(PointerEventData eventData){
		RectTransformUtility.ScreenPointToLocalPointInRectangle(HudRect, Input.mousePosition, Camera.main, out mouseStartPoint);
		UIStartPoint = UIRect.anchoredPosition;

	}

	public void OnDrag( PointerEventData eventData)
	{
        myBtn.enabled = false;
		Vector2 localPoint = Vector2.zero;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(HudRect, Input.mousePosition, Camera.main, out localPoint);
		Vector2 movement = localPoint - mouseStartPoint;
		UIRect.anchoredPosition = UIStartPoint + movement;
      
	}

	public void OnEndDrag(PointerEventData eventData){
		if (RectTransformUtility.RectangleContainsScreenPoint(InventoryPanelRect, Input.mousePosition, Camera.main))
		{
			UIRect.anchoredPosition = UIStartPoint;
            myBtn.enabled = true;
        }
		else{
			UIRect.anchoredPosition = UIStartPoint;
			InventoryManager.Instance.DropItem(myId);
		}

	}

    public void Use()
    {

        if (!PuzzleManager.Instance.TrySolvePuzzle(myId, mySprite))
        {
            ItemEventManager.Instance.InvokeItemFunction(myId, mySprite, me.ActionInBagFunction, gameObject);

        }
    }
}
