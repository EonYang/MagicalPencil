using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ItemEventManager : MonoBehaviour {

    [SerializeField]
    private GameObject pointLightPrefab;

    public void LightUp(Item item, Sprite sprite, GameObject obj)

    {
        Transform previousLight = obj.transform.Find("Light");

        if (previousLight == null)
        {
            if (PlayerContext.Instance.hasElectricity)
            {
                GameObject l = Instantiate(pointLightPrefab, obj.transform);
                l.name = "Light";

            }
            else
            {
                UIManager.Instance.ShowTip("I need electricity. \n Perhaps draw a power outlet first.");
            }
        }
        else 
        { 
            UIManager.Instance.ShowTip("Already lighted up.");

            }



    }

}
