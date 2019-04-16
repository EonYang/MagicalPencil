using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public static UIManager Instance;

	//public Canvas GameUIPrefeb;
	//public Canvas EndOfLevelUIPrefeb;

	public Canvas HUD;

	public GameObject SketchBook;
	public GameObject TextFeedbackPanel;
	public Slider HPBar;
	public GameObject MagicalPencilHighlighter;
	public Button Magical_Pencil;
	public Button CancelDrawingButton;
	public Button[] MakeChoiceBtns;
	public SketchBookManager scene1Manager;

	private Coroutine waitingToHideFeedback;
    
	public Canvas EndOfLevelUICanvas;
	public Text EndOfLevelUIH1;
	public Text EndOfLevelUIH3;
	public Button EndOfLevelUIMainButton;
	public Button EndOfLevelUISecondButton;


	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}else{
		}
	}

	void Start () {

		EndOfLevelUICanvas.gameObject.SetActive(false);

		ActiveSketchBook(false);

		MagicalPencilHighlighter.SetActive(false);
		Magical_Pencil.onClick.AddListener(() => ActiveSketchBook(true));
		CancelDrawingButton.onClick.AddListener(() => ActiveSketchBook(false));

	}
    
	public void ActiveSketchBook(bool bol){
		SketchBook.SetActive(bol);
		ActiveChoiceBtns(false);
		SketchBook.GetComponent<Paintable>().DestroyAllLineGens();

	}
        
    
	public void ActiveChoiceBtns(bool bol)
    {
		MakeChoiceBtns[0].transform.parent.gameObject.SetActive(bol);
    }


	public void ShowTip(string t){
		if(waitingToHideFeedback != null){
			StopCoroutine(waitingToHideFeedback);
		}

		TextFeedbackPanel.GetComponentInChildren<Text>().text = t;
		TextFeedbackPanel.SetActive(true);
		waitingToHideFeedback = StartCoroutine(HideFeedback(3));
	}
    

	public IEnumerator HideFeedback( int t){
        yield return new WaitForSeconds(t*1.5f);
		TextFeedbackPanel.SetActive(false);
		Debug.Log("clearing: " + TextFeedbackPanel.GetComponentInChildren<Text>().text);
    } 


}
