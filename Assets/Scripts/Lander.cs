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

    private void OnCollisionEnter2D(Collision2D other) {
        float softLandingVelocityMagnitude = 4f;
        float minLandingDotVal = .90f;
        float landingDotVal = Vector2.Dot(Vector2.up, transform.up);
        
        if (other.relativeVelocity.magnitude > softLandingVelocityMagnitude) {
            Debug.Log("crash");
        }
        if (landingDotVal < minLandingDotVal) {
            Debug.Log("landed with bad angle");
        }
        else {
            Debug.Log("land");
        }
        
    }
    
    
}
