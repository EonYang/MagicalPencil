using UnityEngine;

public class PlayerContext: MonoBehaviour 
{
	public static PlayerContext Instance;
       
    public bool closeToWater = false;
    public int inPuzzleArea = 0;
    public bool inBattle = false;
    public bool inDark = false;
    public bool hot = false;
    public bool cold = false;
    public bool bleeding = false;
    public bool inRain = false;
    public bool hasElectricity = false;
    public bool canCook = false;
    public bool facingRightSide = true;
    public bool motorcycling = false;
    public bool feelingCold = false;
    public bool feelingWarm = false;
    public string lastSceneName = "";

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void toggleCloseToWater(){
		closeToWater = !closeToWater;
		Debug.Log("near water : " + closeToWater);
	}

}