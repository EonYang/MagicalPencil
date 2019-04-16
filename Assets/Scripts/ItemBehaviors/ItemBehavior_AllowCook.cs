using UnityEngine;
using System.Collections;

public partial class ItemEventManager : MonoBehaviour {

    public void AllowCook(Item item , Sprite sprite, GameObject obj)
    {
        PlayerContext.Instance.canCook = true;
        UIManager.Instance.ShowTip(item.StoryOnUse);
    }


}
