using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetSpriteByName : MonoBehaviour {

	public GameObject toAttach;

	private string fn = "test.png";
	private string url = "http://127.0.0.1:5800/api/downloadSprite?fileName=";
	// Use this for initialization
	void Start () {
		StartCoroutine(GetTexture(fn));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
      
 

	IEnumerator GetTexture(string fileName)
    {
		UnityWebRequest www = UnityWebRequestTexture.GetTexture(url + fileName);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
			Texture2D tex = ((DownloadHandlerTexture)www.downloadHandler).texture;

			Sprite mySprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);

			toAttach.GetComponent<SpriteRenderer>().sprite = mySprite;
        }
    }
}
