using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour {
    public int resetScene = 0;
    public float delay = 5;
    private float timeStamp;
    private bool down;

    void Start() {
        InputManager.OnButtonDown += HandleButtonDown;
        InputManager.OnButton += HandleButton;
        InputManager.OnButtonUp += HandleButtonUp;
        down = false;
    }

    private void HandleButtonDown() {
        down = true;
        timeStamp = Time.time;
    }

    private void HandleButton() {
        // Restart after delay seconds pressed
        if(down && Time.time - timeStamp > delay) {
            SceneManager.LoadScene(resetScene);
        }
    }

    private void HandleButtonUp() {
        down = false;
    }
}
