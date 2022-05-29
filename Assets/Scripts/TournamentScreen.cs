using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TournamentScreen : Screen
{
    public TextMeshProUGUI TMPLeaderboardText;
    public override void DoThingsAtClose()
    {
        Debug.Log("Tournament Screen is Off");
    }

    public override void DoThingsAtShow()
    {
        Debug.Log("Tournament Screen is On");
        PrintLeaderboard();
    }

    public void GoMenuOnClick()
    {
        CloseScreen();
        screenManager.menuScreen.ShowScreen();
    }


    private void PrintLeaderboard()
    {
        List<Player> players = TournamentHandler.GetInstance().players;

        string leaderboard = "";

        foreach (Player player in players)
        {
            string point = "\n" + player.transform.name + " : " + player.point;
            leaderboard += point;
        }

        TMPLeaderboardText.text = leaderboard;
    }
}