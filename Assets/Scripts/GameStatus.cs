using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStatus : MonoBehaviour {


    public int accumulatedScore;
    public int lives;
    string currentScene;

    public void Init()
    {
        accumulatedScore = 0;
        lives = 0;
        currentScene = null;
        
        
    }

    public void OnLoadScene()
    {
        //check if this is the first time loading this scene
        if(currentScene != SceneManager.GetActiveScene().name)
        {
            currentScene = SceneManager.GetActiveScene().name;
            lives = 3;
        }
    }
}
