using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class DevFunctions : MonoBehaviour {

    [SerializeField]
    private Sprite testSprite;
    private int testItemId = 295;

	public void ResetGame()
    {
        GameManager.Instance.ResetGame();
    }

    public void HPUp()
    {
        PlayerHP.Instance.AdjustHP(+10);
    }
    public void HPDown()
    {
        PlayerHP.Instance.AdjustHP(-10);
    }

    public void NextLevel()
    {
        LevelUtil.GoToNextLevel();
    }

    public void LastLevel()
    {
        LevelUtil.GoToLastLevel();
    }

    public void SpawnTestItem( int id){
        ItemEventManager.Instance.ItemData[id].sprite = testSprite;
        //ItemSpawner.Instance.SpanwItem(ItemEventManager.Instance.ItemData[id]);
        MethodInfo mi = ItemSpawner.Instance.GetType().GetMethod("SpanwItem");
        mi.Invoke(ItemSpawner.Instance, new object[] { ItemEventManager.Instance.ItemData[id], testSprite } );
    }
}
