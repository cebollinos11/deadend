using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {

    [SerializeField]
    GameObject[] enemyDB;

    bool sendEnemies;

    public float spawnFreq;

    public GameObject PerfectText;
    
    int score;
    public Text ScoreUI;

	// Use this for initialization
	void Start () {
        score = 0;
        Time.timeScale = 1f;
        sendEnemies = true;
        StartCoroutine(SpawnEnemies());
	}

    public void CallPerfect()
    {
        Instantiate(PerfectText);
    }

    public void IncreaseScore(int i)
    {
        score += i;
        ScoreUI.text = score.ToString();

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
            GameObject GO = (GameObject) Instantiate(enemyDB[d]);
            float delay = 0f;

            WaitEnemy we = GO.GetComponent<WaitEnemy>();

            if (we)
            {
                delay += we.waitTime;
            }
            yield return new WaitForSeconds(spawnFreq+delay);
            
        } while (sendEnemies);
    }
	
	


}
