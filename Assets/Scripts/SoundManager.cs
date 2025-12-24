using System;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private AudioClip feulSound;
    [SerializeField] private Lander lander;
    public void Start() { 
        lander = GetComponent<Lander>();
        lander.OnFeulPickup += LanderOnFeulPickup;
        lander.OnCoinPickup += LanderOnCoinPickup;
    }

    private void LanderOnCoinPickup(object sender, EventArgs e) {
        AudioSource.PlayClipAtPoint(coinSound,Camera.main.transform.position);
    }

    private void LanderOnFeulPickup(object sender, EventArgs e) {
        AudioSource.PlayClipAtPoint(feulSound,Camera.main.transform.position);
    }
}
