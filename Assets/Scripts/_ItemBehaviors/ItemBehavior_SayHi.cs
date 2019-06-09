using UnityEngine;
using System.Collections;

public partial class ItemEventManager : MonoBehaviour {


    public void SayHi(Item item , Sprite sprite, GameObject obj)
    {
        // fuckoff is from "Smell"
        StartCoroutine(FuckOff(obj));
    }

}
