using UnityEngine;
using System.Collections;

public class RestartButton : MonoBehaviour {


    GameManager GM;
	// Use this for initialization
	void Start () {
        GM = GameObject.FindObjectOfType<GameManager>();
	}

    public void RestartGame()
    {
        GM.RestartGame();
    }
}
