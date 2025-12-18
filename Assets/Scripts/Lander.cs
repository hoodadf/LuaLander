using UnityEngine;
using UnityEngine.InputSystem;

public class Lander : MonoBehaviour
{

    private void Update() {
        if (Input.GetKey(KeyCode.UpArrow)) {
            Debug.Log("up(old)");
        }
        if (Keyboard.current.upArrowKey.isPressed) {
            Debug.Log("up(new)");
        }
    }
}
