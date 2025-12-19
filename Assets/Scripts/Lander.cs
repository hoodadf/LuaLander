using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lander : MonoBehaviour {
    private Rigidbody2D landerRigidbody2D;
    private float forwardForce = 700f;
    private float turnSpeed = 100f;
    private void Awake() {
        landerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed) {
            landerRigidbody2D.AddForce(forwardForce * transform.up * Time.deltaTime);
        }
        
        if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.dKey.isPressed) {
            landerRigidbody2D.AddTorque(-turnSpeed * Time.deltaTime);
        }
        
        if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.aKey.isPressed) {
            landerRigidbody2D.AddTorque(+turnSpeed * Time.deltaTime);        
        }
    }

    private void Update() {
        
    }
}
