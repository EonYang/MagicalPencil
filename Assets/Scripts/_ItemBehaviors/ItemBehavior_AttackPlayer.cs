using UnityEngine;
using System.Collections;

public partial class ItemEventManager : MonoBehaviour {

    public void AttackPlayer(Item item , Sprite sprite, GameObject obj)
    {
        ItemBehavior_AttackPlayerMono atkMono = obj.AddComponent<ItemBehavior_AttackPlayerMono>();
        EnemyHP hp = obj.GetComponent<EnemyHP>();
        hp.enabled = true;
        obj.tag = "Enemy";
        obj.layer = 11;
        atkMono.me = item;

    }


}
