using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lander : MonoBehaviour {
    public event EventHandler OnUpForce;
    public event EventHandler OnLeftRot;
    public event EventHandler OnRightRot;
    public event EventHandler OnBeforeForce;
    
    private Rigidbody2D landerRigidbody2D;
    
    private float forwardForce = 700f;
    private float turnSpeed = 100f;
    private void Awake() {
        landerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        OnBeforeForce?.Invoke(this,EventArgs.Empty);
        if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed) {
            landerRigidbody2D.AddForce(forwardForce * transform.up * Time.deltaTime);
            OnUpForce?.Invoke(this,EventArgs.Empty);
        }
        
        if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.dKey.isPressed) {
            landerRigidbody2D.AddTorque(-turnSpeed * Time.deltaTime);
            OnRightRot?.Invoke(this,EventArgs.Empty);
        }
        
        if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.aKey.isPressed) {
            landerRigidbody2D.AddTorque(+turnSpeed * Time.deltaTime);  
            OnLeftRot?.Invoke(this,EventArgs.Empty);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        float softLandingVelocityMagnitude = 4f;
        float relativeVelocityMagnitude = other.relativeVelocity.magnitude;
        float minLandingDotVal = .90f;
        float landingDotVal = Vector2.Dot(Vector2.up, transform.up);

        if (!other.gameObject.TryGetComponent(out LandingPad landingPad)) {
            Debug.Log("crashed on terrain!");
            return;
        }
        
        if (relativeVelocityMagnitude > softLandingVelocityMagnitude) {
            Debug.Log("landed too hard!");
            return;
        }
        if (landingDotVal < minLandingDotVal) {
            Debug.Log("landed with bad angle!");
            return;
        }
        
        Debug.Log("successful landing!");


        float maxScoreLandingAngle = 100;
        float scoreDotVectorMultiplier = 10f;
        float landingAngleScore = maxScoreLandingAngle -
                                  Mathf.Abs(landingDotVal - 1f) * scoreDotVectorMultiplier * maxScoreLandingAngle;
        float maxScoreLandingSpeed = 100;
        float landingSpeedScore = (softLandingVelocityMagnitude - relativeVelocityMagnitude) * maxScoreLandingSpeed;
        
        Debug.Log("Landing Angle Score: " + landingAngleScore);
        Debug.Log("Landing Speed Score: " + landingSpeedScore);

        float totalScore = (landingSpeedScore + landingAngleScore) * landingPad.getMultiplier();
        
        Debug.Log("Total Landing Score: " + totalScore);


    }
    
    
}
