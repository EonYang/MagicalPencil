using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    private string puzzelDataURL = LevelUtil.puzzelDataURL;
   
    private List<PuzzleAndItsSolvers> puzzlesAndSolvers = new List<PuzzleAndItsSolvers>();

    private void Awake()
    {
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Use this for initialization
    void Start () {

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
                        return true;
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
        UIManager.Instance.EndOfLevelUIMainButton.onClick.AddListener(() => StartCoroutine(GoToNextLevel(solver.NextLevel)));
        UIManager.Instance.EndOfLevelUIMainButton.GetComponentInChildren<Text>().text = "Next Level";

        UIManager.Instance.EndOfLevelUISecondButton.gameObject.SetActive(true);
        UIManager.Instance.EndOfLevelUISecondButton.onClick.AddListener(() => RestartLevel());
        UIManager.Instance.EndOfLevelUISecondButton.GetComponentInChildren<Text>().text = "Play Again";

        UIManager.Instance.EndOfLevelUICanvas.gameObject.SetActive(true);

    }

    public IEnumerator GoToNextLevel(string levelName)
    {
        StartCoroutine(UIManager.Instance.Fade(0, 1, 1));
        while (UIManager.Instance.fading)
        {
            yield return new WaitForSeconds(0.1f);
        }
        PlayerContext.Instance.inPuzzleArea = 0;
        LevelUtil.GoToLevel(levelName);
    }

    public void GoToEnemy()
    {
        StartCoroutine(GoToNextLevel("Level_HomeGuards"));
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

    public void GameOverByLowHP()
    {
        PlayerAnimationManager.Instance.Die();
        Debug.Log("game over called");
        UIManager.Instance.EndOfLevelUIH1.text = "Game Over";
        UIManager.Instance.EndOfLevelUIH3.text = "You're dead.";
        UIManager.Instance.ActiveSketchBook(false);
        UIManager.Instance.EndOfLevelUIMainButton.gameObject.SetActive(true);
        UIManager.Instance.EndOfLevelUIMainButton.onClick.AddListener(() => StartCoroutine(RestartLevel()));
        UIManager.Instance.EndOfLevelUIMainButton.GetComponentInChildren<Text>().text = "Retry";
        UIManager.Instance.EndOfLevelUISecondButton.gameObject.SetActive(false);
        UIManager.Instance.EndOfLevelUICanvas.gameObject.SetActive(true);
    }

    public IEnumerator RestartLevel()
    {
        StartCoroutine(UIManager.Instance.Fade(0, 1, 1));
        while (UIManager.Instance.fading)
        {
            yield return new WaitForSeconds(0.1f);
        }
        PlayerHP.Instance.AdjustHP(+100);
        LevelUtil.RestartScene();
        PlayerAnimationManager.Instance.Init();
    }

    public void ResetGame()
    {
        StartCoroutine(GoToSlides());
    }
    public IEnumerator GoToSlides()
    {
        LevelUtil.GoToLevel("Slides");
        yield return new WaitForSeconds(0.5f);
    }

}
