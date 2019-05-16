using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ItemEventManager : MonoBehaviour {

    [SerializeField]
    private GameObject rainPrefab;

    public void StartRaining(Item item, Sprite sprite, GameObject obj)
    {
        Debug.Log("calling raining");


        GameObject rainEmitter = Instantiate(rainPrefab, Camera.main.gameObject.transform);
        rainEmitter.name = "SnowEmitter";
       
        UIManager.Instance.ShowTip(item.StoryOnUse);
        obj.GetComponent<OnGround>().AdjustDurability(-1);

        PlayerContext.Instance.tempreture -= 20;
        PlayerContext.Instance.isSnowing = true;
        Debug.Log("Current Temprature : " + PlayerContext.Instance.tempreture.ToString());

    }
        

}