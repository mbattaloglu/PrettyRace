using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject UIRacePanel;

    public TextMeshProUGUI TMPCurrentLapText;
    public TextMeshProUGUI TMPCurrentLapTimeText;
    public TextMeshProUGUI TMPLastLapText;
    public TextMeshProUGUI TMPBestLapText;

    public Player UpdateUIForPlayer;

    private int currentLap = -1;
    private float currentLapTime;
    private float lastLapTime;
    private float bestLapTime;

    private void Update()
    {
        if (UpdateUIForPlayer == null) return;

        if (UpdateUIForPlayer.CurrentLap != currentLap)
        {
            currentLap = UpdateUIForPlayer.CurrentLap;
            TMPCurrentLapText.text = "LAP:" + currentLap;
        }

        if (UpdateUIForPlayer.CurrentLapTime != currentLapTime)
        {
            currentLapTime = UpdateUIForPlayer.CurrentLapTime;
            TMPCurrentLapTimeText.text = $"TIME: {(int)currentLapTime / 60}:{(currentLapTime) % 60:00.000}";
        }

        if (UpdateUIForPlayer.LastLapTime != lastLapTime)
        {
            lastLapTime = UpdateUIForPlayer.LastLapTime;
            TMPLastLapText.text = $"LAST LAP: {(int)lastLapTime / 60}:{(lastLapTime) % 60:00.000}";
        }

        if (UpdateUIForPlayer.BestLapTime != bestLapTime)
        {
            bestLapTime = UpdateUIForPlayer.BestLapTime;
            TMPBestLapText.text = bestLapTime < 1000000 ?   $"BEST LAP: {(int)bestLapTime / 60}:{(bestLapTime) % 60:00.000}" : "BEST LAP:";
        }
    }
}
