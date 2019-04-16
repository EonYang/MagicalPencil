using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SketchBookManager : MonoBehaviour {

   
	public int[] top3PredictionIds = new int[3];

	private RequestDoodlePredictionAPI myRequester;

	private void Awake()
	{
		myRequester = GetComponent<RequestDoodlePredictionAPI>();
	}

	// Use this for initialization
	void Start () {
		UIManager.Instance.SketchBook.SetActive(false);
		UIManager.Instance.TextFeedbackPanel.SetActive(false);
		PlayerContext.Instance.inDark = true;
		PlayerHP.Instance.Refresh();      
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    

	public void ClearTop3Prediction(){
		Debug.Log("top 3 cleared");
		for (int i = 0; i < 3; i++)
		{
			UIManager.Instance.MakeChoiceBtns[i].onClick.RemoveAllListeners();

		}
		top3PredictionIds = new int[3];
	}

	public void MakeChoice(int choiseID){

		myRequester.RequestSpriteAndSpawn(ItemEventManager.Instance.ItemData[choiseID]);

		ClearTop3Prediction();
		UIManager.Instance.ActiveSketchBook(false);    

	}

	public void SetBtnsName(){
		
		for (int i = 0; i < 3; i++)
		{
			int id = top3PredictionIds[i];
			//UIManager.Instance.MakeChoiceBtns[i].GetComponentInChildren<Text>().text = itemEventManager.ItemData[id].Name;
			UIManager.Instance.MakeChoiceBtns[i].GetComponentInChildren<Text>().text = ItemEventManager.Instance.ItemData[id].Name;
			UIManager.Instance.MakeChoiceBtns[i].onClick.RemoveAllListeners();
			UIManager.Instance.MakeChoiceBtns[i].onClick.AddListener(() => MakeChoice(ItemEventManager.Instance.ItemData[id].Id));
		}
	}


}
