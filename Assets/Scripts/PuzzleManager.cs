using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour {

	public static PuzzleManager Instance;

	private string puzzelDataURL = LevelUtil.puzzelDataURL;
   
	private List<PuzzleAndItsSolvers> puzzlesAndSolvers = new List<PuzzleAndItsSolvers>();

	private void Awake()
	{
		if (Instance == null){
			Instance = this;
		}
	}
	// Use this for initialization
	void Start () {
		StartCoroutine(GetPuzzelData(1));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool TrySolvePuzzle( int itemID, Sprite sprite){
		bool r = false;

        // test if there is puzzle
		if (PlayerContext.Instance.inPuzzleArea == 0)
		{
			return false;
		} 
        // if there is puzzle, see if can we find this item in solves
		else {
			PuzzleSolver solver = puzzlesAndSolvers.Find((obj) => obj.Id == PlayerContext.Instance.inPuzzleArea).Solvers.Find((i) => i.Id == itemID);
            // if we find, check the result

			if (solver!= null){
                Debug.Log(solver.Name);
                Debug.Log(solver.Result);

                switch (solver.Result)
				{
					case 1:
						LevelClear(solver);
						return true;
					case 2:
						GameOverByPuzzle(solver);
						return true;
					case 3:
						UIManager.Instance.ShowTip(solver.ResultText);
						return false;
					default:
						break;
				}
			}
		}
		return r;
	}

	public IEnumerator GetPuzzelData(int puzzleId)
    {

		UnityWebRequest www = UnityWebRequest.Get(puzzelDataURL + puzzleId.ToString());
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
			if (www.isDone)
            {
                //Debug.Log(www.downloadHandler.text);
				puzzlesAndSolvers.Add(JsonUtility.FromJson<PuzzleAndItsSolvers>(www.downloadHandler.text));
				//Debug.Log(puzzlesAndSolvers[0].Solvers[0].ResultText);
            }
 
        }
    }

	public void LevelClear(PuzzleSolver solver)

    {
		Debug.Log("levelclear called");
		Debug.Log(solver.NextLevel);
		UIManager.Instance.EndOfLevelUIH1.text = "Level Clear !";
		UIManager.Instance.EndOfLevelUIH3.text = solver.ResultText;
        UIManager.Instance.EndOfLevelUIMainButton.gameObject.SetActive(true);
		UIManager.Instance.EndOfLevelUIMainButton.onClick.AddListener(() => GoToNextLevel(solver.NextLevel));
        UIManager.Instance.EndOfLevelUIMainButton.GetComponentInChildren<Text>().text = "Next Level";

        UIManager.Instance.EndOfLevelUISecondButton.gameObject.SetActive(true);
        UIManager.Instance.EndOfLevelUISecondButton.onClick.AddListener(() => RestartLevel());
		UIManager.Instance.EndOfLevelUISecondButton.GetComponentInChildren<Text>().text = "Play Again";

		UIManager.Instance.EndOfLevelUICanvas.gameObject.SetActive(true);

    }

    public void GoToNextLevel(string levelName)
    {
        LevelUtil.GoToLevel(levelName);
    }


	public void GameOverByPuzzle(PuzzleSolver solver)
    {
        Debug.Log("game over called");
		UIManager.Instance.EndOfLevelUIH1.text = "Game Over";
        UIManager.Instance.EndOfLevelUIH3.text = solver.ResultText;
        UIManager.Instance.EndOfLevelUIMainButton.gameObject.SetActive(true);
		UIManager.Instance.EndOfLevelUIMainButton.onClick.AddListener(() => RestartLevel());
        UIManager.Instance.EndOfLevelUIMainButton.GetComponentInChildren<Text>().text = "Retry";
        UIManager.Instance.EndOfLevelUISecondButton.gameObject.SetActive(false);

        UIManager.Instance.EndOfLevelUICanvas.gameObject.SetActive(true);
    }

    public void RestartLevel()
    {
        LevelUtil.RestartScene();
    }

}
