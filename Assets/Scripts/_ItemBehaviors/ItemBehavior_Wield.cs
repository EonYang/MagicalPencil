using UnityEngine;
using System.Collections;

public partial class ItemEventManager : MonoBehaviour {



    public void Wield(Item item , Sprite sprite, GameObject obj)
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

    private IEnumerator Wield2(Item item , Sprite sprite, GameObject obj)
    {
        GameObject weaponImg = new GameObject("weaponImg");
        weaponImg.transform.parent = player.transform;
        weaponImg.transform.localScale = new Vector3(2, 2, 2);
        SpriteRenderer spriteRenderer =  weaponImg.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        spriteRenderer.sortingOrder = 800;

        Vector3 newPos = Vector3.zero;

        for (int i = 0; i < 12; i++)
        {
            yield return 0;
            newPos += (Vector3.right * 0.2f);
            weaponImg.transform.localPosition = newPos;
        }
        yield return new WaitForSeconds(0.1f);
        float weaponRadius = item.SizeH / 100;
        float distance = 0.3f;
        Vector3 attackPos = new Vector3(PlayerContext.Instance.facingRightSide? distance : -distance, 0, 0);
        Collider2D[] enemieToDamage = Physics2D.OverlapCircleAll(player.transform.localPosition + attackPos, weaponRadius, enemyLayer);

        foreach( Collider2D enemy in enemieToDamage)
        {
            enemy.GetComponent<EnemyHP>().TakeDamage(item.Attack);
        }
        Destroy(weaponImg);
    }
        


}
