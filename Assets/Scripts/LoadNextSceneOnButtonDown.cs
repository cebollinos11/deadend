using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadNextSceneOnButtonDown : MonoBehaviour {
    public int resetScene = 1;
    public static KeyCode Key = KeyCode.Space;

    void Update() {
        if(Input.GetKeyDown(Key) || InputManager.State == InputManager.InputState.ButtonUp) {
            SceneManager.LoadScene(resetScene);
        }
    }
}
