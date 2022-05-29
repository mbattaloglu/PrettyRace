using UnityEngine;
using TMPro;
using System.Collections;

public class GameScreen: Screen 
{
    public TextMeshProUGUI TMPCountdownText;
    public TextMeshProUGUI TMPCurrentLapText;
    public TextMeshProUGUI TMPCurrentLapTimeText;
    public TextMeshProUGUI TMPLastLapText;
    public TextMeshProUGUI TMPBestLapText;

    public Player UpdateUIForPlayer;

    private int currentLap = -1;
    private float currentLapTime;
    private float lastLapTime;
    private float bestLapTime;

    private void Start()
    {
        StartCoroutine(StartGame());
    }

    public override void DoThingsAtClose()
    {
        Debug.Log("Game Screen Is Off");
    }

    public override void DoThingsAtShow()
    {
        Debug.Log("Game Screen Is On");
        UpdateTimingTable();
    }

    public void PauseOnClick()
    {
        CloseScreen();
        screenManager.pauseScreen.ShowScreen();
    }


    
    private void Update()
    {
        if (!screenManager.gameManager.isGameStarted) return;
        if (screenManager.gameManager.isGamePaused) return;
        UpdateTimingTable();   
    }

    IEnumerator StartGame()
    {
        TMPCountdownText.text = "" + 3;
        yield return new WaitForSeconds(0.93f);
        TMPCountdownText.gameObject.SetActive(false);

        TMPCountdownText.text = "" + 2;
        TMPCountdownText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.93f);
        TMPCountdownText.gameObject.SetActive(false);

        TMPCountdownText.text = "" + 1;
        TMPCountdownText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.93f);
        TMPCountdownText.gameObject.SetActive(false);

        TMPCountdownText.text = "GO";
        TMPCountdownText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.93f);
        TMPCountdownText.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.07f);
        screenManager.gameManager.isGameStarted = true;
    }

    private void UpdateTimingTable()
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
            TMPBestLapText.text = bestLapTime < 1000000 ? $"BEST LAP: {(int)bestLapTime / 60}:{(bestLapTime) % 60:00.000}" : "BEST LAP:";
        }
    }

}