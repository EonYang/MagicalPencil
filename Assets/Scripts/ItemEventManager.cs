using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public partial class ItemEventManager : MonoBehaviour {

    public static ItemEventManager Instance;

    public Item[] ItemData;
    
	private string itemDataAPILink = LevelUtil.itemDataAPILink;
    private Items items;

    private void Awake()
    {
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
        
        StartCoroutine(GetItemDataFromServer());
    }

    private IEnumerator GetItemDataFromServer()
    {
        Debug.Log(itemDataAPILink);
        using (UnityWebRequest www = UnityWebRequest.Get(itemDataAPILink))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    Debug.Log(www.downloadHandler.text);
                    items = JsonUtility.FromJson<Items>(www.downloadHandler.text);
                    ItemData = items.items;
                    Debug.Log(ItemData[223].StoryOnPickUp);
                }
            }
        }
    }

    public void InvokeItemFunction(int id, Sprite sprite, string functionName, GameObject gameObject)
    {
        MethodInfo mi = Instance.GetType().GetMethod(functionName);
        if (mi != null)
        {
            Item item = ItemData[id];
            mi.Invoke(this, new object[] { item, sprite, gameObject });
        }
        else
        {
            Debug.Log(functionName + " doesn't exist");
            UIManager.Instance.ShowTip(ItemData[id].StoryOnUse + "\n (this function is not finished yet)");
        }

    }

	public void ShowItemStory(int id, Sprite sprite){
        Debug.Log("using item in general context");
        Item item = ItemData[id];
        GameObject panelAndText = UIManager.Instance.TextFeedbackPanel;
        // priority: solving puzzle, battle, general;
        //if (  PlayerContext.Instance.inPuzzle1Area && item.CanSolvePuzzle1 != 0){
        //    trySolvePuzzle1(item, panelAndText);
        //} else 

        // edit: puzzle sovling is now tied to the game controller of level.

        if (  PlayerContext.Instance.inBattle && item.Attack >= 1){
            // Use as weapon and autofight;
        } else {

        }
            
    }
   

    private void TryUseWeapon(Item item, GameObject panelAndText)
    {
        Debug.Log("if you re in a battle, this will be a weapon.");
    }



}
