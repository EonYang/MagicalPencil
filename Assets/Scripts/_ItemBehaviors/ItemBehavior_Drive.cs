using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class ItemEventManager : MonoBehaviour {

    // ride means
    // player speed up
    // horse is moving with user
    // user animation = ridin horse, skating, rooler skating, cycling, motorcycling ;


    //private IDictionary<string, float> rideHeights = new Dictionary<string, float> ();

    


    public void Drive(Item item, Sprite sprite, GameObject obj)
    {
        if (item.Name != "camel" && PlayerContext.Instance.inDesert)
        {
            UIManager.Instance.ShowTip("I can't ride this in a desert.");
            return;
        }

        //GameObject player = GameObject.FindWithTag("Player");
        UIManager.Instance.ShowTip(item.StoryOnUse);

        StartCoroutine(Drive2(obj, item));

    }

    private IEnumerator Drive2(GameObject obj, Item item)
    {
        obj.name = "Ride";

        offset = Vector2.up / 40f * item.SizeH;
        player.GetComponent<BoxCollider2D>().offset += offset;
        PlayerContext.Instance.speedFactor = item.Speed/100f;


        if(obj.transform.Find("motorLight") == null)
        {
            GameObject motorLight = Instantiate(spotLightPrefab, obj.transform);
            motorLight.name = "motorLight";
            motorLight.transform.localPosition = new Vector3(2, 1, -1); 
        }

        yield return new WaitForSeconds(0.2f);

        // move vehicle to the center
        obj.transform.parent = player.transform;
        Vector3 ridePosition = obj.transform.localPosition;
        ridePosition.x = 0;
        obj.transform.localPosition = ridePosition;

        obj.GetComponent<Rigidbody2D>().simulated = false;
        obj.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -1f);
        player.AddComponent<StopRiding>().me = item;

        PlayerAnimationManager.Instance.Driving();
    }


}
