using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadNextSceneOnButtonDown : MonoBehaviour {
    public int resetScene = 1;
    // Use this for initialization
    void Start () {
        InputManager.OnButtonDown += HandleButtonDown;
    }

    private void HandleButtonDown() {
        SceneManager.LoadScene(resetScene);
    }
}
