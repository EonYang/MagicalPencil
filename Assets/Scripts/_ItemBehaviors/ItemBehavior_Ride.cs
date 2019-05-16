using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class ItemEventManager : MonoBehaviour {

    // ride means
    // player speed up
    // horse is moving with user
    // user animation = ridin horse, skating, rooler skating, cycling, motorcycling ;


    //private IDictionary<string, float> rideHeights = new Dictionary<string, float> ();


    [SerializeField]
    private GameObject spotLightPrefab;


    public void Ride(Item item, Sprite sprite, GameObject obj)
    {
        if (item.Name != "camel" && PlayerContext.Instance.inDesert)
        {
            UIManager.Instance.ShowTip("I can't ride this in a desert.");
            return;
        }

        //GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<BoxCollider2D>().offset += Vector2.down/40 * item.SizeH;
        PlayerContext.Instance.speedFactor = item.Speed/100;
        PlayerContext.Instance.motorcycling = true;

        obj.name = "Ride";
        if(item.Name == "motorbike" && obj.transform.Find("motorLight") == null)
        {
            GameObject motorLight = Instantiate(spotLightPrefab, obj.transform);
            motorLight.name = "motorLight";
            motorLight.transform.localPosition = new Vector3(2, 2, -1); 
        }
        StartCoroutine(Ride2(obj, item));

        UIManager.Instance.ShowTip(item.StoryOnUse);


    }

    private IEnumerator Ride2(GameObject obj, Item item)
    {
        yield return new WaitForSeconds(0.2f);
        //GameObject player = GameObject.FindWithTag("Player");
        obj.transform.parent = player.transform;
        Vector3.Scale(obj.transform.localPosition, (Vector3.up + Vector3.forward));
        //obj.transform.localPosition =  * );
        obj.GetComponent<Rigidbody2D>().simulated = false;
        obj.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -1f);
        player.AddComponent<StopRiding>().me = item;

    }

    public void StopRide( int size)
    {
        //GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<BoxCollider2D>().offset += Vector2.up/40 * size;
        GameObject ride = player.transform.Find("Ride").gameObject;
        PlayerContext.Instance.speedFactor = 1;
        PlayerContext.Instance.motorcycling = false;
        //ride.transform.localPosition = player.transform.localPosition;
        ride.transform.parent = null;
        ride.GetComponent<Rigidbody2D>().simulated = true;
    }


}
