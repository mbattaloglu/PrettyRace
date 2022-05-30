using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections.Generic;

public class GameEndScreen: Screen 
{
    public TextMeshProUGUI TMPLeaderboardText;
    public override void DoThingsAtClose()
    {
        Debug.Log("Game End Screen Is Off");
    }

    public override void DoThingsAtShow()
    {
        Debug.Log("Game End Screen Is On");
        GameManager.GetInstance().isRaceOver = false;
        PrintLeaderboard();
    }

    public void PauseOnClick()
    {
        CloseScreen();
        screenHandler.pauseScreen.ShowScreen();
    }

    public void GoMenuOnClick()
    {
        CloseScreen();
        screenHandler.menuScreen.ShowScreen();
        screenHandler.gameManager.gameObject.SetActive(false);
    }

    private void PrintLeaderboard()
    {

        List<Player> players = FindObjectsOfType<Player>().ToList();
        string leaderboard = "";

        foreach (Player player in players)
        {
            string point = "\n" + player.transform.name + " : " + player.lastPointsEarned;
            leaderboard += point;
        }

        TMPLeaderboardText.text = leaderboard;
    }

}