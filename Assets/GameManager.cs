using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    [SerializeField]
    GameObject[] enemyDB;

	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnEnemies());
	}

    IEnumerator SpawnEnemies()
    {


        do
        {
            yield return new WaitForSeconds(1.3f);
            int d = Random.Range(0,enemyDB.Length);
            Instantiate(enemyDB[d]);
        } while (true);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
