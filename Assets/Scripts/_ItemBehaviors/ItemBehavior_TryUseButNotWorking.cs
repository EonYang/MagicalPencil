using UnityEngine;
using System.Collections;

public partial class ItemEventManager : MonoBehaviour {

    public void TryUseButNotWorking(Item item , Sprite sprite, GameObject obj)
    {
        UIManager.Instance.ShowTip(item.StoryOnUse);
    }


}
