using UnityEngine;
using System.Collections;

public partial class ItemEventManager : MonoBehaviour {

    // ride means
    // player speed up
    // horse is moving with user
    // user animation = ridin horse, skating, rooler skating, cycling, motorcycling ;


    public void Ride(Item item, Sprite sprite, GameObject obj)
    {
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<BoxCollider2D>().offset += Vector2.down;
        TouchToMove.Instance.speedFactor = item.Speed/100;
        PlayerContext.Instance.motorcycling = true;

        obj.name = "Ride";
        StartCoroutine(Ride2(obj));

        UIManager.Instance.ShowTip(item.StoryOnUse);



    }

    private IEnumerator Ride2(GameObject obj)
    {
        yield return new WaitForSeconds(0.2f);
        GameObject player = GameObject.FindWithTag("Player");
        obj.transform.parent = player.transform;
        Vector3.Scale(obj.transform.localPosition, (Vector3.up + Vector3.forward));
        //obj.transform.localPosition =  * );
        obj.GetComponent<Rigidbody2D>().simulated = false;
        obj.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -1f);
        player.AddComponent<StopRiding>();

    }

    public void StopRide(GameObject player)
    {
        //GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<BoxCollider2D>().offset += Vector2.up;
        GameObject ride = player.transform.Find("Ride").gameObject;
        TouchToMove.Instance.speedFactor = 1;
        PlayerContext.Instance.motorcycling = false;
        //ride.transform.localPosition = player.transform.localPosition;
        ride.transform.parent = null;
        ride.GetComponent<Rigidbody2D>().simulated = true;
    }


}
