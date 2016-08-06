using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {

    [SerializeField]
    AbstractEnemy[] enemyDB;

    bool sendEnemies;

    public float spawnFreq;

    public GameObject PerfectText;

    private Player player;

    int score;
    public Text ScoreUI;

    // Use this for initialization
    void Start() {
        score = 0;
        Time.timeScale = 1f;
        sendEnemies = true;
        player = FindObjectOfType<Player>();
        if (player == null) {
            Debug.LogError("No Hero found in scene!");
            return;
        }
        player.OnDeath += new System.Action(HandlePlayerDeath);
        StartCoroutine(SpawnEnemies());
    }

    public void CallPerfect() {
        Instantiate(PerfectText);
    }

    public void IncreaseScore(int i) {
        score += i;
        ScoreUI.text = score.ToString();
    }

    private void HandlePlayerDeath() {
        StopSendingEnemies();
        StopAllEnemies();
        Invoke("Restart", 3.0f);
    }

    void StopSendingEnemies() {
        sendEnemies = false;
    }

    void StopAllEnemies() {
        Enemy[] e = GameObject.FindObjectsOfType<Enemy>();
        for (int i = 0; i < e.Length; i++) {
            e[i].enabled = false;
        }
    }

    private void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void HandleEnemyDeath(AbstractEnemy enemy) {
        // Update score
        IncreaseScore(1);
        // Unsubscribe from enemy events
        enemy.OnAttack -= new AbstractEnemy.EnemyAttackHandler(HandleEnemyAttack);
        enemy.OnDeath -= new AbstractEnemy.EnemyDeathHandler(HandleEnemyDeath);

    }

    private void HandleEnemyAttack(AbstractEnemy enemy, System.Type type) {
        // Grant perfect?
        if(enemy.EvaluatePerformance()) {
            DisplayPerfect();
        }
    }

    private void DisplayPerfect() {
        Instantiate(PerfectText);
    }

    IEnumerator SpawnEnemies() {
        while(sendEnemies) {
            // Select random enemy to instantiate
            int d = Random.Range(0, enemyDB.Length);
            AbstractEnemy enemy = Instantiate(enemyDB[d]);
            enemy.Player = player;
            // Subscribe to events
            enemy.OnAttack += new AbstractEnemy.EnemyAttackHandler(HandleEnemyAttack);
            enemy.OnDeath += new AbstractEnemy.EnemyDeathHandler(HandleEnemyDeath);
            // Wait enemies enforce delay on next enemy spawn to avoid unsolvable situation
            float delay = 0f;
            WaitEnemy we = enemy.gameObject.GetComponent<WaitEnemy>();
            if (we) {
                delay += we.WaitTime;
            }
            // Delay next spawn
            yield return new WaitForSeconds(spawnFreq + delay);
        }
    }
}
