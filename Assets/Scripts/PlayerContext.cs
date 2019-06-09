using UnityEngine;

public class PlayerContext: MonoBehaviour 
{
	public static PlayerContext Instance;
       
    public bool closeToWater = false;
    public int inPuzzleArea = 0;
    public int tempreture = 10;
    public bool inBattle = false;
    public bool inDark = false;
    public bool bleeding = false;
    public bool inRain = false;
    public bool hasElectricity = false;
    public bool canCook = false;
    public bool facingRightSide = true;
    public bool motorcycling = false;
    public string lastSceneName = "";
    public GameObject focus = null;
    public int riding = 0;

    public float speedFactor = 1;

    public bool inDesert = false;
    public bool isSnowing = false;
    public bool isRaining = false;

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