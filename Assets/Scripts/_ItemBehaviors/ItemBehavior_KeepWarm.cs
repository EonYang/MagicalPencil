using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ItemEventManager : MonoBehaviour {

    public void KeepWarm(Item item, Sprite sprite, GameObject obj)
    {
        Debug.Log("calling keep warm");

        UIManager.Instance.ShowTip(item.StoryOnUse);

        PlayerContext.Instance.tempreture += 20;

    }
}
