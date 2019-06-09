using UnityEngine;
using System.Collections;

public partial class ItemEventManager : MonoBehaviour {



    //public void Stab(Item item , Sprite sprite, GameObject obj)
    //{
    //    float weaponRadius = item.SizeH / 100;
    //    float distance = 0.3f;
    //    Vector3 attackPos = new Vector3(PlayerContext.Instance.facingRightSide? distance : -distance, 0, 0);
    //    Collider2D[] enemieToDamage = Physics2D.OverlapCircleAll(player.transform.localPosition + attackPos, weaponRadius, enemyLayer);

    //    foreach( Collider2D enemy in enemieToDamage)
    //    {
    //        enemy.GetComponent<EnemyHP>().TakeDamage(item.Attack);
    //    }

    //    //GameObject lineee = new GameObject("lineR");
    //    //lineee.transform.localPosition = player.transform.localPosition + attackPos;
    //    //LineRenderer lineR = lineee.AddComponent<LineRenderer>();
    //    //lineR.positionCount = 4;
    //    //lineR.useWorldSpace = false;
    //    //lineR.SetWidth(0.02f, 0.02f);
    //    //lineR.sortingOrder = 200;
    //    //lineR.SetPosition(0, new Vector3(-weaponRadius, weaponRadius, 0));
    //    //lineR.SetPosition(1, new Vector3(weaponRadius, weaponRadius, 0));
    //    //lineR.SetPosition(2, new Vector3(weaponRadius, -weaponRadius, 0));
    //    //lineR.SetPosition(3, new Vector3(-weaponRadius, -weaponRadius, 0));

    //    UIManager.Instance.ShowTip(item.StoryOnUse);
    //}
        
     public void Stab(Item item , Sprite sprite, GameObject obj)
    {
        playerAnimator.SetTrigger("attack");

        StartCoroutine(Wield2(item,sprite,obj));


        //GameObject lineee = new GameObject("lineR");
        //lineee.transform.localPosition = player.transform.localPosition + attackPos;
        //LineRenderer lineR = lineee.AddComponent<LineRenderer>();
        //lineR.positionCount = 4;
        //lineR.useWorldSpace = false;
        //lineR.SetWidth(0.02f, 0.02f);
        //lineR.sortingOrder = 200;
        //lineR.SetPosition(0, new Vector3(-weaponRadius, weaponRadius, 0));
        //lineR.SetPosition(1, new Vector3(weaponRadius, weaponRadius, 0));
        //lineR.SetPosition(2, new Vector3(weaponRadius, -weaponRadius, 0));
        //lineR.SetPosition(3, new Vector3(-weaponRadius, -weaponRadius, 0));



        UIManager.Instance.ShowTip(item.StoryOnUse);
    }


}
