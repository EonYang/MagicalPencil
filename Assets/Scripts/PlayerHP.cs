using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour {

	public static PlayerHP Instance;
    
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
            if (PlayerContext.Instance.feelingCold)
            {
                AdjustHP(-1);
            }

            yield return new WaitForSeconds(1f);
        }
    }

	public void AdjustHP(int change){
		HP += change;
		HP = Mathf.Clamp(HP, 0, 100);
		UIManager.Instance.HPBar.value = HP;
	}

	public void Refresh(){
		UIManager.Instance.HPBar.value = HP;
	}

    void Init()
    {
        Refresh();
    }
}
