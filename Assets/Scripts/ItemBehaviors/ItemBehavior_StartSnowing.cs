using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ItemEventManager : MonoBehaviour {

    [SerializeField]
    private GameObject snowPrefab;

    public void StartSnowing(Item item, Sprite sprite, GameObject obj)
    {
        Debug.Log("calling snowing");

        GameObject player = GameObject.FindWithTag("Player");

        GameObject snowEmitter = Instantiate(snowPrefab, player.transform);
        snowEmitter.name = "SnowEmitter";
       
        UIManager.Instance.ShowTip(item.StoryOnUse);
        obj.GetComponent<OnGround>().AdjustDurability(-1);

        PlayerContext.Instance.feelingCold = true;

    }
        


}
