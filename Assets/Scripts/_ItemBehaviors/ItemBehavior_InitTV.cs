using UnityEngine;
using System.Collections;

public partial class ItemEventManager : MonoBehaviour {

    [SerializeField]
    GameObject UMPPrefab;

    public void InitTV(Item item , Sprite sprite, GameObject obj)
    {

        GameObject ump = Instantiate(UMPPrefab, obj.transform);
        GameObject screen = ump.transform.Find("screen").gameObject;
        MeshRenderer screenMesh = screen.GetComponent<MeshRenderer>();
        screenMesh.sortingOrder = 300;
        screen.AddComponent<Tap2ZoomIn>();
        ump.transform.localPosition = new Vector3(0f, -0.5f, -0.1f);
        ump.transform.localScale = new Vector3(0.2f, 0.2f, 1);

        //obj.GetComponent<SpriteRenderer>().sortingOrder = 0;
    }

}
