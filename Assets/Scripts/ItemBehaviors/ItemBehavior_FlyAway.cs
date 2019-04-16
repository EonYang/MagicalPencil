using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ItemEventManager : MonoBehaviour {


    public void FlyAway(Item item, Sprite sprite, GameObject obj)
    {
        Vector2 velo = Vector2.zero;
        velo.x = item.Speed * 0.002f;
        velo.y = item.Speed * 0.001f;
        velo.x = PlayerContext.Instance.facingRightSide ? velo.x : -velo.x;
        StartCoroutine(Accelorate(item, velo, obj));
        obj.GetComponent<Rigidbody2D>().gravityScale = 0;
        GameObject.Destroy(obj.transform.Find("Collider").gameObject);
        UIManager.Instance.ShowTip(item.StoryOnUse);
    }

    private IEnumerator Accelorate(Item item, Vector2 velo, GameObject obj)
    {

        GameObject player = GameObject.FindWithTag("Player");

        while(Mathf.Abs( obj.transform.localPosition.x - player.transform.localPosition.x) <= 5 )
        {
            yield return 0;
            obj.GetComponent<Rigidbody2D>().velocity += velo;
            //Debug.Log(velo.x);
        }

        Destroy(obj);
        GameObject hud = GameObject.FindWithTag("HUD");
        GameObject.Destroy(hud.transform.Find(item.Name).gameObject);
    }


}
