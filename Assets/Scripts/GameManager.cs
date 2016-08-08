using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(InputManager))]
public class GameManager : MonoBehaviour {
    // Game state variables
    private bool sendEnemies;
    private int score;

    // References
    [SerializeField]
    private GameObject PerfectText;
    [SerializeField]
    private Text ScoreUI;
    [SerializeField]
    Transform spawnPoint;
    [SerializeField]
    private AudioClip perfectClip;


    private Player player;
    private EnemyGroupTypeManager groupTypes;


    [SerializeField]
    private float startDelay = 1;

    [Header("Generated Level Parameters")]

    [SerializeField]
    private bool generateInfiniteLevel = false;
    [SerializeField]
    private float outerDelayIfGenerated;


     [Header("Custom Level Parameters")]
    [SerializeField]
    private EnemyGroup[] level;

    [HideInInspector]public CameraHandler camHandler;

    // Use this for initialization
    void Start() {

        camHandler = GameObject.FindObjectOfType<CameraHandler>();
        score = 0;
        Time.timeScale = 1f;
        sendEnemies = true;
        player = FindObjectOfType<Player>();
        if (player == null) {
            Debug.LogError("No Player found in scene!");
            return;
        }
        player.OnDeath += new System.Action(HandlePlayerDeath);

        groupTypes = FindObjectOfType<EnemyGroupTypeManager>();
        if (groupTypes == null) {
            Debug.LogError("No EnemyGroupTypeManager found in scene!");
            return;
        }

        StartCoroutine(Game());
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
        StartCoroutine(Restart());
    }

    private void StopSendingEnemies() {
        sendEnemies = false;
    }

    void StopAllEnemies() {
        Enemy[] e = GameObject.FindObjectsOfType<Enemy>();
        for (int i = 0; i < e.Length; i++) {
            e[i].enabled = false;
        }
    }

    private IEnumerator Restart() {
        yield return new WaitForSeconds(1.0f);
        yield return new WaitUntil(() => InputManager.State == InputManager.InputState.ButtonDown);
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
        // Play perfect sfx
        AudioManager.PlayClip(perfectClip);
    }

    void LoadNextLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        //current scene number as int
        int i = int.Parse(currentScene.name);
        //load next scene
        SceneManager.LoadScene((i + 1).ToString());
    }

    IEnumerator LevelTransitionRoutine()
    {

        float delayStartTransition = 1f;
        float delayLoadNextScene = 3f;
        //zoom in to player
        yield return new WaitForSeconds(delayStartTransition);
        camHandler.zoomTo(player.transform.position);

        //startPlayerWinAnimation
        player.GetComponent<Animator>().enabled = true;

        /*
        yield return new WaitForSeconds(1f);
        AudioManager.PlayClip(AudioManager.Instance.hammerDown);
        //camHandler.PlayBump();
        GameObject.FindObjectOfType<LevelCleared>().GetComponent<Text>().enabled = true;
        */
        //load next Scene
        yield return new WaitForSeconds(delayLoadNextScene);
        LoadNextLevel();
        
    }

    private IEnumerator Game() {
        yield return new WaitForSeconds(startDelay);
        int levelIndex = 0;
        while (sendEnemies) {
            if(generateInfiniteLevel) {
                // Select what group to spawn at random
                int index = Random.Range(0, groupTypes.Types.Length);
                yield return StartCoroutine(SpawnGroup(groupTypes.Types[index]));
                yield return new WaitForSeconds(outerDelayIfGenerated);
            } else if(levelIndex < level.Length) {
                // Spawn group next group in level
                EnemyGroupType groupType = groupTypes.GetGroupByName(level[levelIndex].Name);
                yield return StartCoroutine(SpawnGroup(groupType));
                yield return new WaitForSeconds(level[levelIndex].DelayAfter);
                ++levelIndex;
            } else {
                // TODO
                Debug.Log("You just beat this level!");
                StartCoroutine(LevelTransitionRoutine());
                
                
                break;
            }           
        }
    }

    private IEnumerator SpawnSingle(AbstractEnemy enemyPrefab) {        
        // Create enemy from prefab
        AbstractEnemy enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity) as AbstractEnemy;
        enemy.Player = player;
        // Subscribe to events
        enemy.OnAttack += new AbstractEnemy.EnemyAttackHandler(HandleEnemyAttack);
        enemy.OnDeath += new AbstractEnemy.EnemyDeathHandler(HandleEnemyDeath);
        // Special delays
        if(enemy.GetType() == System.Type.GetType("WaitEnemy")) {
            yield return new WaitForSeconds(((WaitEnemy)enemy).WaitTime);
        } else if(enemy.GetType() == System.Type.GetType("HammerEnemy")) {
            yield return new WaitForSeconds(((HammerEnemy)enemy).WaitTime);
        }
    }


    private IEnumerator SpawnGroup(EnemyGroupType group) {
        if(group.IsRandom) {
            for(int n = 0; n < group.AmountIfRandom; ++n) {
                // Select on of the group at random
                int index = Random.Range(0, group.Enemies.Length);
                yield return StartCoroutine(SpawnSingle(group.Enemies[index]));
                yield return new WaitForSeconds(group.InnerDelay);
            }            
        } else {
            // Spawn in sequence
            foreach (AbstractEnemy e in group.Enemies) {
                yield return StartCoroutine(SpawnSingle(e));
                yield return new WaitForSeconds(group.InnerDelay);
            }
        }
            
    }
}
