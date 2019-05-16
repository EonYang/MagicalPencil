using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    static PlayerAttack Instance;

    public bool CanAttack = true;

    // Use this for initialization
    [SerializeField]
    private float timeEachAttack = 0.5f;

    private float timeSinceLastAttack;

    private Transform attackPos;

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

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (timeSinceLastAttack < timeEachAttack)
        {
            timeSinceLastAttack += Time.deltaTime;
        }
        else
        {
            //attack
        }
		
	}

    public void Attack(Item item, GameObject obj)
    {






        timeSinceLastAttack = 0f;
    }
}
