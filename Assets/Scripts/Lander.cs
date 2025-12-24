using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = System.Random;

public class Lander : MonoBehaviour {
    public static Lander Instance { get; private set; }
    
    public event EventHandler OnUpForce;
    public event EventHandler OnLeftRot;
    public event EventHandler OnRightRot;
    public event EventHandler OnBeforeForce;
    public event EventHandler OnCoinPickup;
    public event EventHandler OnFeulPickup;
    public event EventHandler<OnLandedEventArgs> OnLanded;
    public class OnLandedEventArgs : EventArgs {
        public int score;
    }
    
    private Rigidbody2D landerRigidbody2D;
    
    private float forwardForce = 700f;
    private float turnSpeed = 100f;
    private float feul = 10f;
    private float feulConsumption = 1f;

    private int coins = 0;
    
    private void Awake() {
        Instance = this;
        landerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void consumeFuel() {
        feul -= feulConsumption * Time.deltaTime;
        Debug.Log(feul);
    }

    private void FixedUpdate() {
        OnBeforeForce?.Invoke(this,EventArgs.Empty);

        bool upKeyPressed = Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed;
        bool rightKeyPressed = Keyboard.current.upArrowKey.isPressed || Keyboard.current.dKey.isPressed;
        bool leftKeyPressed = Keyboard.current.upArrowKey.isPressed || Keyboard.current.aKey.isPressed;

        if (feul <= 0f) {
            Debug.Log("out of feul!!!!");
            return;
        }
        if(upKeyPressed || leftKeyPressed || rightKeyPressed) consumeFuel();
        if (upKeyPressed) {
            landerRigidbody2D.AddForce(forwardForce * transform.up * Time.deltaTime);
            OnUpForce?.Invoke(this,EventArgs.Empty);
        }
        
        if (rightKeyPressed) {
            landerRigidbody2D.AddTorque(-turnSpeed * Time.deltaTime);
            OnRightRot?.Invoke(this,EventArgs.Empty);
        }
        
        if (leftKeyPressed) {
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

        //landing score calculation
        float maxScoreLandingAngle = 100;
        float scoreDotVectorMultiplier = 10f;
        float landingAngleScore = maxScoreLandingAngle -
                                  Mathf.Abs(landingDotVal - 1f) * scoreDotVectorMultiplier * maxScoreLandingAngle;
        float maxScoreLandingSpeed = 100;
        float landingSpeedScore = (softLandingVelocityMagnitude - relativeVelocityMagnitude) * maxScoreLandingSpeed;
        
        Debug.Log("Landing Angle Score: " + landingAngleScore);
        Debug.Log("Landing Speed Score: " + landingSpeedScore);

        int totalScore = (int)((landingSpeedScore + landingAngleScore) * landingPad.getMultiplier());
        
        Debug.Log("Total Landing Score: " + totalScore);
        
        OnLanded?.Invoke(this,new OnLandedEventArgs{ score = totalScore });

    }

    private float randomFeulPickupAmount() {
        float minAmount = 4f;
        float maxAmount = 8f;
        Random rand = new Random();
        return (float)(rand.NextDouble() * (maxAmount - minAmount) + minAmount);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        float maxFeulAmount = 10f;
        if(other.TryGetComponent(out FeulPickup feulPickup)) {
            feul += randomFeulPickupAmount();
            if (feul > maxFeulAmount) feul = maxFeulAmount;
            Debug.Log("refilled");
            OnFeulPickup?.Invoke(this,EventArgs.Empty);
            feulPickup.destroySelf();
        }

        if (other.TryGetComponent(out Coin coin)) {
            coins++;
            Debug.Log(coins);
            OnCoinPickup?.Invoke(this,EventArgs.Empty);
            coin.destroySelf();
        }
    }
}
