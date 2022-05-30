using System.Collections.Generic;
using System.Linq;
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
        screenHandler.menuScreen.ShowScreen();
    }


    private void PrintLeaderboard()
    {
        List<Player> players = PointHandler.GetInstance().players;
        Dictionary<Player, int> points = new Dictionary<Player, int>();
        for (int i = 0; i < players.Count; i++)
        {
            points.Add(players[i], players[i].totalPoint);
        }

        //LINQ
        Dictionary<Player, int> sortedPoints = (Dictionary<Player, int>)(from point in points orderby point.Value ascending select point); 

        string leaderboard = "";

        foreach (KeyValuePair<Player, int> player in sortedPoints)
        {
            string point = "\n" + player.Key.transform.name + " : " + player.Value;
            leaderboard += point;
        }

        TMPLeaderboardText.text = leaderboard;
    }
}