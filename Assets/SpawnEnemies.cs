using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour {

    // Use this for initialization
    public GameObject enemyPrefab;
    List<GameObject> enemies = new List<GameObject>();
	void Start () {
        StartCoroutine(Spawn());
        Physics2D.IgnoreLayerCollision(11,11);
        //Physics2D.IgnoreLayerCollision(9,11);

	}

    IEnumerator Spawn()
    {
        while (enemies.Count < 8) 
        {
            yield return new WaitForSeconds(1);
            GameObject e =  SpawnEnemy();
            enemies.Add(e);
        }
    }
	
	GameObject SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform);
        enemy.transform.localPosition = Vector3.zero;
        enemy.transform.parent = null;
        return enemy;
    }
}
