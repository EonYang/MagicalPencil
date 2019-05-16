using UnityEngine;
using System.Collections;

public partial class ItemEventManager : MonoBehaviour {

    public void CookAndEat(Item item , Sprite sprite, GameObject obj)
    {
        if (PlayerContext.Instance.canCook)
        {
            PlayerHP.Instance.AdjustHP(item.RecoverHP);
            string t = item.StoryOnUse + "\n HP + " + item.RecoverHP.ToString();
            UIManager.Instance.ShowTip(t);
            InventoryManager.Instance.AdjustDurability(item.Id, -1);
        }
        else
        {
            UIManager.Instance.ShowTip("Can't cook now. Do you have a stove or something like that?");

        }

    }


}
