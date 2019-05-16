using UnityEngine;
using System.Collections;

public partial class ItemEventManager : MonoBehaviour {

    public void Kick(Item item , Sprite sprite, GameObject obj)
    {
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        int side = PlayerContext.Instance.facingRightSide ? 1 : -1;
        Vector2 newVel = new Vector2(3 * side, 1);
        rb.velocity = newVel;
        //UIManager.Instance.ShowTip(item.StoryOnUse);
    }


}
