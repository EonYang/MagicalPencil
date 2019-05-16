using UnityEngine;
using System.Collections;

public partial class ItemEventManager : MonoBehaviour {

    public void Eat(Item item, Sprite sprite, GameObject obj)
    {
        PlayerHP.Instance.AdjustHP(item.RecoverHP);
        string t = item.StoryOnUse + "\n HP + " + item.RecoverHP.ToString();
        UIManager.Instance.ShowTip(t);
        InventoryManager.Instance.AdjustDurability(item.Id, -1);
        if (item.Tag1 == "Fruits")
        {
            PlayerHP.Instance.AdjustHydrated(item.RecoverHP);
            Debug.Log(item.Name + " is fruit, hydrated ++ ");
        }
    }

    public void Drink(Item item, Sprite sprite, GameObject obj)
    {
        PlayerHP.Instance.AdjustHydrated(item.RecoverHP);
        string t = item.StoryOnUse + "\n Hydrated + " + item.RecoverHP.ToString();
        UIManager.Instance.ShowTip(t);
        InventoryManager.Instance.AdjustDurability(item.Id, -1);
    }


}
