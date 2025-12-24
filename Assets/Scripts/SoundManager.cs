using System;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private AudioClip feulSound;
    public void Start() { 
        Lander.Instance.OnFeulPickup += LanderOnFeulPickup;
        Lander.Instance.OnCoinPickup += LanderOnCoinPickup;
    }

    private void LanderOnCoinPickup(object sender, EventArgs e) {
        AudioSource.PlayClipAtPoint(coinSound,Camera.main.transform.position);
    }

    private void LanderOnFeulPickup(object sender, EventArgs e) {
        AudioSource.PlayClipAtPoint(feulSound,Camera.main.transform.position);
    }
}
