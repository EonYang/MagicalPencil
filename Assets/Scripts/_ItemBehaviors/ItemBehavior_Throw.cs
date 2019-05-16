using UnityEngine;
using System.Collections;

public partial class ItemEventManager : MonoBehaviour {

    public void Throw(Item item , Sprite sprite, GameObject obj)
    {
        playerAnimator.SetTrigger("throw");
        StartCoroutine(Throw2(item));
    }

    IEnumerator Throw2( Item item)
    {
        yield return new WaitForSeconds(0.4f);
        GameObject projectile = InventoryManager.Instance.DropItem(item.Id);
        yield return new WaitForEndOfFrame();
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        int side = PlayerContext.Instance.facingRightSide ? 1 : -1;
        Vector2 newVel = new Vector2(10 * side, 1);
        rb.velocity = newVel;
        projectile.AddComponent<Projectile>().me = item;
        UIManager.Instance.ShowTip(item.StoryOnUse);
    }


}
