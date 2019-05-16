using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class LevelUtil {

    private static bool isIOS = Application.isMobilePlatform ? true : false;

    private static string URL = isIOS ? "http://point99.xyz:5800" : "http://localhost:5800";

    public static string itemDataAPILink = URL + "/api/getItemData";
    public static string predictionURL = URL + "/api/doodlePredict";
    public static string processSpriteURL = URL + "/api/askForSprite";
    public static string downloadSpriteURL = URL + "/api/downloadSprite?fileName=";
    public static string puzzelDataURL = URL + "/api/getPuzzleData?id=";

    public static void GoToLevel(string levelName){
		SceneManager.LoadScene(levelName);
	}
	public static void RestartScene()
    {
        Debug.Log("restarting scene");
        GameObject.FindWithTag("Singleton").GetComponent<DestroySingleton>().SelfDestroy();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    
}
