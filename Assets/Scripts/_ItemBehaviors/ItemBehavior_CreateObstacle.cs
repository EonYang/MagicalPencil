using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ItemEventManager : MonoBehaviour {


    public void CreateObstacle(Item item, Sprite sprite, GameObject obj)
    {
        Debug.Log("calling creating obstacle");

        GameObject player = GameObject.FindWithTag("Player");

        obj.transform.Find("Collider").gameObject.layer = player.layer;
        obj.GetComponent<Rigidbody2D>().gravityScale = 100;

    }
        


}
