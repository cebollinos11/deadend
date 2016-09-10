using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadNextSceneOnButtonDown : MonoBehaviour {
    public int resetScene = 1;
    public KeyCode Key = KeyCode.E;

    void Update() {
        if(Input.GetKeyDown(Key)) {
            SceneManager.LoadScene(resetScene);
        }
    }
}
