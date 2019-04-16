using UnityEngine;
using System.Collections;

public partial class ItemEventManager : MonoBehaviour {

    public void Eat(Item item , Sprite sprite, GameObject obj)
    {
        PlayerHP.Instance.AdjustHP(item.RecoverHP);
        string t = item.StoryOnUse + "\n HP + " + item.RecoverHP.ToString();
        UIManager.Instance.ShowTip(t);
        InventoryManager.Instance.AdjustDurability(item.Id , - 1);
    }


}
