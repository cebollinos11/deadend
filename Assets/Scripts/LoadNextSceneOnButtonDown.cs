using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadNextSceneOnButtonDown : MonoBehaviour {
    public int resetScene = 1;
    public static KeyCode Key = KeyCode.Space;

    bool disabled;

    void Update() {
        if(!disabled && Input.GetKeyDown(Key) || InputManager.State == InputManager.InputState.ButtonUp) {
            StartCoroutine(StartRoutine());
            disabled = true;
        }
    }

    IEnumerator StartRoutine()
    {
        GameObject.FindObjectOfType<Animator>().enabled = true;
        
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(resetScene);
    }
}
