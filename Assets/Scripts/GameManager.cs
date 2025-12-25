using System;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    public static GameManager Instance { get; private set; }
    private int score;
    private float time;

    private void Awake() {
        Instance = this;
    }
    private void start() {
        Lander.Instance.OnCoinPickup += OnCoinPickup;
        Lander.Instance.OnLanded += OnLanded;
    }

    private void Update() {
        time += Time.deltaTime;
    }

    private void OnLanded(object sender, Lander.OnLandedEventArgs e) {
        addScore(e.score);
        Debug.Log(score);
    }

    private void OnCoinPickup(object sender, EventArgs e) {
        addScore(500);
        Debug.Log(score);
    }

    private void addScore(int scoreAmount) {
        score += scoreAmount;
    }

    public int getScore() {
        return score;
    }

    public int getCoin() {
        return Lander.Instance.getCoin();
    }

    public float getTime() {
        return time;
    }

    public int getFeul() {
        return Lander.Instance.getFeul();
    }
}
