using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RequestDoodlePredictionAPI : MonoBehaviour {

	private string predictionURL = LevelUtil.predictionURL;   
	private string processSpriteURL = LevelUtil.processSpriteURL; 
	private string downloadSpriteURL = LevelUtil.downloadSpriteURL;  

	public PredictionResponse serverResponse = new PredictionResponse();

    //private GameObject doodleCanvas;
    //public GameObject spritePrefab;
    [SerializeField]
    private float lineWidthFactor = 10f;

    [SerializeField]
    private int itemID = 162;

    [SerializeField]
    private float factorY = 0f;

    public RenderTexture RTexture;
	public float SpriteSizeFactor;

    private byte[] doodleData;

	private Paintable paintable;

	private SketchBookManager scene1Manager;


    private void Awake()
    {

        scene1Manager = GetComponent<SketchBookManager>();
    }
	void Start () {
        //doodleCanvas = GameObject.FindWithTag("SketchBook");
		paintable = UIManager.Instance.SketchBook.GetComponent<Paintable>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonUp(0) && paintable.isDrawing){
			RequestPrediction();
		}

        //foreach (var obj in Paintable.Instance.toDestroy)
        //{
        //    obj.GetComponent<LineRenderer>().widthMultiplier = lineWidthFactor / ItemEventManager.Instance.ItemData[itemID].SizeH + factorY * lineWidthFactor;
        //}

        //Debug.Log(Paintable.Instance.toDestroy[0].GetComponent<LineRenderer>().widthMultiplier);
	}

   
    public void RequestPrediction()
    {
		WWWForm form = PrepareForm();
        StartCoroutine(GetPrediction(form));
    }

	public void RequestSpriteAndSpawn(Item item)
    { 

        WWWForm form = PrepareForm();

		StartCoroutine(GetSpriteLink(form,item));
    }
    

	private WWWForm PrepareForm(){
		RenderTexture.active = RTexture;

        var tex2d = new Texture2D(RTexture.width, RTexture.height);
        tex2d.ReadPixels(new Rect(0, 0, RTexture.width, RTexture.height), 0, 0);
        tex2d.Apply();

        doodleData = tex2d.EncodeToJPG();
        string b64 = Convert.ToBase64String(doodleData);

        WWWForm form = new WWWForm();
        form.AddField("data", b64);
		return form;
	}

	public IEnumerator GetPrediction(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(predictionURL, form))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    Debug.Log(www.downloadHandler.text);
                    serverResponse = JsonUtility.FromJson<PredictionResponse>(www.downloadHandler.text);
					giveTop3ToSceneManager(serverResponse.prediction);
                }
            }
        }

    }

	public IEnumerator GetSpriteLink(WWWForm form, Item item)
    {
		using (UnityWebRequest www = UnityWebRequest.Post(processSpriteURL, form))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    Debug.Log(www.downloadHandler.text);
					SpriteNameResponse spriteNameResponse = JsonUtility.FromJson<SpriteNameResponse>(www.downloadHandler.text);
					StartCoroutine(GetTexture(spriteNameResponse.fileName, item));
                }
            }
        }

    }

	private	IEnumerator GetTexture(string fileName, Item item)
    {
		
		UnityWebRequest www = UnityWebRequestTexture.GetTexture(downloadSpriteURL + fileName);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
			
            Texture2D tex = ((DownloadHandlerTexture)www.downloadHandler).texture;

            Sprite mySprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
			ItemEventManager.Instance.ItemData[item.Id].sprite = mySprite;
			ItemSpawner.Instance.SpanwItem(item, mySprite);
        }
    }

    
    
	private void giveTop3ToSceneManager(PredictionData prediction){
		List<int> temp = new List<int>();
		for (int i = 0; i < 5; i++)
		{
			if(ItemEventManager.Instance.ItemData[prediction.numbers[i]].Tag2 != "Filtered"){
					temp.Add(prediction.numbers[i]);
					Debug.Log(prediction.numbers[i] + " : " + prediction.names[i]);
				}

			}
              
	    if(temp.Count >= 3){
			for (int k = 0; k < 3; k++)
			{
				scene1Manager.top3PredictionIds[k] = temp[k];
			}
			UIManager.Instance.ActiveChoiceBtns(true);
            scene1Manager.SetBtnsName();
        }
        
        
	}
    
}
