using UnityEngine;
using System.Collections;

public partial class ItemEventManager : MonoBehaviour {

    [SerializeField]
    Sprite fuckoffPrefab;

    [SerializeField]
    Material diffuse2D;

    public void Smell(Item item , Sprite sprite, GameObject obj)
    {
        StartCoroutine(FuckOff(obj));
    }

    private IEnumerator FuckOff( GameObject obj)
    {
        GameObject fuckOff = new GameObject("fuckOff");
        fuckOff.transform.parent = obj.transform;
        fuckOff.transform.localScale = new Vector3(1, 1, 1);
        fuckOff.transform.localPosition = new Vector3(4, 8, 0);
        SpriteRenderer foRenderer = fuckOff.AddComponent<SpriteRenderer>();
        foRenderer.sortingOrder = 200;
        foRenderer.material = diffuse2D;
        foRenderer.sprite = fuckoffPrefab;
        yield return new WaitForSeconds(3);
        Destroy(fuckOff);
    }


}
