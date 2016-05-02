using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    [SerializeField]
    GameObject[] enemyDB;

    bool sendEnemies;

	// Use this for initialization
	void Start () {
        Time.timeScale = 1f;
        sendEnemies = true;
        StartCoroutine(SpawnEnemies());
	}

    void StopSendingEnemies() {
        sendEnemies = false;
    }

    void StopAllEnemies() {
        Enemy[] e = GameObject.FindObjectsOfType<Enemy>();
        for (int i = 0; i < e.Length; i++)
        {
            e[i].enabled = false;
        }
    }

    public void PlayerGotKilled() {
        StopSendingEnemies();
        StopAllEnemies();
    }

    IEnumerator SpawnEnemies()
    {


        do
        {
            int d = Random.Range(0, enemyDB.Length);
            //d = 1;
            Instantiate(enemyDB[d]);
            yield return new WaitForSeconds(1.3f);
            
        } while (sendEnemies);
    }
	
	


}
