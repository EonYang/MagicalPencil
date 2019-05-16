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
    public GameObject BlackFade;
	public Slider HPBar;
    public Slider HydratedBar;
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

    private Image blackFadeImage;

    public bool fading = false;


	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
            //DontDestroyOnLoad(gameObject);
		}else{
            //Destroy(gameObject);
		}
	}

	void Start () {

        Init();

	}

    public void Init()
    {
        EndOfLevelUICanvas.gameObject.SetActive(false);

        ActiveSketchBook(false);

        MagicalPencilHighlighter.SetActive(false);
        Magical_Pencil.onClick.AddListener(() => ActiveSketchBook(true));
        CancelDrawingButton.onClick.AddListener(() => ActiveSketchBook(false));

        blackFadeImage = BlackFade.GetComponent<Image>();
        StartCoroutine(Fade(1, 0, 1));
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

    public IEnumerator Fade(float alpha1, float alpha2, float time)
    {
        fading = true;
        if (!BlackFade.activeSelf)
        {
            BlackFade.SetActive(true);
        }
        blackFadeImage.canvasRenderer.SetAlpha(alpha1);
        blackFadeImage.CrossFadeAlpha(alpha2, time, false);
        yield return new WaitForSeconds(time);
        if(System.Math.Abs(alpha2) < 0.05f)
        {
            BlackFade.SetActive(false);
        }
        fading = false;
    }


}
