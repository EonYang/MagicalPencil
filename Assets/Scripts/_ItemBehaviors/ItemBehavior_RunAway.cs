using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ItemEventManager : MonoBehaviour {


    public void RunAway(Item item, Sprite sprite, GameObject obj)
    {
        Debug.Log("calling RunAway");

        Vector2 velo = Vector2.zero;
        velo.x = item.Speed * 0.002f;
        //velo.y = 0.1f;
        velo.x = PlayerContext.Instance.facingRightSide ? velo.x : -velo.x;
        StartCoroutine(Accelorate(item, velo, obj));
        obj.GetComponent<Rigidbody2D>().gravityScale = 0;
        //UIManager.Instance.ShowTip(item.StoryOnUse);

    }
        


}
