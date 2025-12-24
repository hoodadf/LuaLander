using System;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private int score;

    private void start() {
        Lander.Instance.OnCoinPickup += OnCoinPickup;
        Lander.Instance.OnLanded += OnLanded;
    }

    private void OnLanded(object sender, Lander.OnLandedEventArgs e) {
        addScore(e.score);
    }

    private void OnCoinPickup(object sender, EventArgs e) {
        addScore(500);
    }

    private void addScore(int scoreAmount) {
        score += scoreAmount;
    }
}
