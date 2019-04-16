using UnityEngine;
using System.Collections;

public partial class ItemEventManager : MonoBehaviour {

    public void AllowElectricity(Item item , Sprite sprite, GameObject obj)
    {
        PlayerContext.Instance.hasElectricity = true;
        UIManager.Instance.ShowTip(item.StoryOnUse);
    }


}
