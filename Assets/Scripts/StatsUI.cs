using System;
using TMPro;
using UnityEngine;

public class StatsUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI coinTMP;
    [SerializeField] private TextMeshProUGUI scoreTMP;
    [SerializeField] private TextMeshProUGUI feulTMP;
    [SerializeField] private TextMeshProUGUI timeTMP;

    private void Update() {
        updateStatsTextMesh();
    }

    private void updateStatsTextMesh() {
        coinTMP.text = "" + GameManager.Instance.getCoin();
        scoreTMP.text = "" + GameManager.Instance.getScore();
        feulTMP.text = "" + GameManager.Instance.getFeul();
        timeTMP.text = "" + (int) (Mathf.Round(GameManager.Instance.getTime())/60) + " : " + (int)Mathf.Round(GameManager.Instance.getTime());
    }
    
}
