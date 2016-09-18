using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
    public delegate void InputHandler();
    public static event InputHandler OnButtonDown;
    public static event InputHandler OnButton;
    public static event InputHandler OnButtonUp;

    public static KeyCode Key = KeyCode.Space;

    public enum InputState {
        Default,
        ButtonDown,
        Button,
        ButtonUp
    }
    public static InputState State = InputState.Default;

    private void OnEnable() {
        State = InputState.Default;
    }

    void Update () {
        if (Input.touchSupported) {
            if (Input.touchCount > 0) {
                if (Input.GetTouch(0).phase == TouchPhase.Began) {
                    if (OnButtonDown != null) {
                        OnButtonDown();
                    }
                    State = InputState.ButtonDown;
                } else if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary) {
                    if (OnButtonDown != null) {
                        OnButton();
                    }
                    State = InputState.Button;
                } else if (Input.GetTouch(0).phase == TouchPhase.Ended) {
                    if (OnButtonDown != null) {
                        OnButtonUp();
                    }
                    State = InputState.ButtonUp;
                } else {
                    State = InputState.Default;
                }
            } else {
                State = InputState.Default;
            }       
        } else {
            if (Input.GetKeyDown(Key)) {
                if (OnButtonDown != null) {
                    OnButtonDown();
                }
                State = InputState.ButtonDown;
            } else if (Input.GetKey(Key)) {
                if (OnButtonDown != null) {
                    OnButton();
                }
                State = InputState.Button;
            } else if (Input.GetKeyUp(Key)) {
                if (OnButtonDown != null) {
                    OnButtonUp();
                }
                State = InputState.ButtonUp;
            } else {
                State = InputState.Default;
            }
        }
    }
}
