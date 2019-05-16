using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public partial class ItemEventManager : MonoBehaviour {

    public void GoToAnotherWorld(Item item, Sprite sprite, GameObject obj)
    {
        Scene currentScene = SceneManager.GetActiveScene();


        if ("Level_Door" != currentScene.name)
        {
            PlayerContext.Instance.lastSceneName = currentScene.name;
            StartCoroutine(GameManager.Instance.GoToNextLevel("Level_Door"));
        }
        else
        {
            LevelUtil.GoToLevel(PlayerContext.Instance.lastSceneName);
            UIManager.Instance.ShowTip("I'm back.");
        }

    }


}
