using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHP : MonoBehaviour {

	public static PlayerHP Instance;

    public int PlayerHydrated = 100;
    
    public int HP;

	private void Awake()
	{
		if(Instance == null ){
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}else
		{
			Destroy(gameObject);
		}

	}
   
	// Use this for initialization
	void Start () {
		Refresh();
        StartCoroutine(CheckEvery1Second());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator CheckEvery1Second()
    {
        while (true)
        {
            //do something
            if (!Enumerable.Range(0,40).Contains(PlayerContext.Instance.tempreture))
            {
                AdjustHP(-1);
            }

            if (PlayerContext.Instance.inDesert && !PlayerContext.Instance.isSnowing)
            {
                AdjustHydrated(-1);
            }

            yield return new WaitForSeconds(1f);
        }

    }

    public void AdjustHydrated(int change)
    {
        PlayerHydrated += change;
        PlayerHydrated = Mathf.Clamp(PlayerHydrated, 0, 100);
        PlayerContext.Instance.tempreture = PlayerHydrated > 0 ? 30 : 45;
        UIManager.Instance.HydratedBar.value = PlayerHydrated;

    }

	public void AdjustHP(int change){
		HP += change;
		HP = Mathf.Clamp(HP, 0, 100);
		UIManager.Instance.HPBar.value = HP;
        if (HP == 0) GameManager.Instance.GameOverByLowHP();
	}

	public void Refresh(){
		UIManager.Instance.HPBar.value = HP;
	}

    void Init()
    {
        Refresh();
    }
}
