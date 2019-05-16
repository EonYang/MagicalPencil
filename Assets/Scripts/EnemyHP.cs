using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour {

    public int HP = 100;

    [SerializeField] private GameObject HPBarPrefab;
    private Slider HPBar;

    [SerializeField] private int yOffset = 10;

    private GameObject HUDCanvas;
    private RectTransform HUDRect;

    private GameObject UIobj;
    private RectTransform UIRect;

    private BoxCollider2D selfCollider;

	void Start () {
        InitUIobj();
        selfCollider = GetComponent<BoxCollider2D>();
        ShowAndPositionMenu();
	}
	
	// Update is called once per frame
	void Update () {
        ShowAndPositionMenu();
	}

    public void TakeDamage(int damagePoint)
    {
        HP -= damagePoint;
        HPBar.value = HP;

        if(HP <=0)
        {
            Die();
        }
    }

    private void InitUIobj()
    {
        HUDCanvas = GameObject.FindWithTag("HUD");
        HUDRect = HUDCanvas.GetComponent<RectTransform>();

        Transform tran = HUDCanvas.transform;
        //tran.localPosition += Vector3.forward * 0.1f;
        UIobj = Instantiate(HPBarPrefab, tran);
        UIRect = UIobj.GetComponent<RectTransform>();


        UIobj.SetActive(true);
        UIobj.name = "EnemyHP";
        HPBar = UIobj.GetComponent<Slider>();
        HPBar.value = HP;
    }

    private void ShowAndPositionMenu()

    {
        //Vector3 itemPos = Camera.main.WorldToViewportPoint(this.transform.position + new Vector3(0f,-selfCollider.size.y/2*transform.localScale.y,0f));
        Vector3 itemPos = Camera.main.WorldToViewportPoint(this.transform.position + new Vector3(0, selfCollider.size.y/2 * transform.localScale.y , 0));
        itemPos = new Vector3(itemPos.x * HUDRect.sizeDelta.x - HUDRect.sizeDelta.x / 2, itemPos.y * HUDRect.sizeDelta.y - HUDRect.sizeDelta.y / 2 + yOffset , itemPos.z);
        UIRect.anchoredPosition = itemPos;
    }

    private void Die()
    {
        Destroy(UIobj);
        Destroy(gameObject);
    }
}
