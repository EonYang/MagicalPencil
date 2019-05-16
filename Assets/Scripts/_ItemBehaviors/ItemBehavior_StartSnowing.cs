using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ItemEventManager : MonoBehaviour {

    [SerializeField]
    private GameObject snowPrefab;

    public void StartSnowing(Item item, Sprite sprite, GameObject obj)
    {
        Debug.Log("calling snowing");


        GameObject snowEmitter = Instantiate(snowPrefab, Camera.main.gameObject.transform);
        snowEmitter.name = "SnowEmitter";
       
        UIManager.Instance.ShowTip(item.StoryOnUse);
        obj.GetComponent<OnGround>().AdjustDurability(-1);

        PlayerContext.Instance.tempreture -= 20;
        PlayerContext.Instance.isSnowing = true;
        Debug.Log("Current Temprature : " + PlayerContext.Instance.tempreture.ToString());

    }
        

}