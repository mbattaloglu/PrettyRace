using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class TournamentHandler : MonoBehaviour, IObserver
{
    private static TournamentHandler instance;

    private readonly int[] points = new int[] { 6, 5, 4, 3, 2, 1 };
    private const int BEST_LAP_POINT = 1;
    public List<Player> players;

    private List<Player> result;

    private Player bestLappedPlayer;
    private float bestLapTime;

    private TournamentHandler()
    {
        if (instance == null) instance = this;
    }

    public static TournamentHandler GetInstance()
    {
        return instance;
    }
    private void Start()
    {
        //From first to sixth
        bestLapTime = Mathf.Infinity;

        players = GameObject.FindObjectsOfType<Player>().ToList();
        result = new List<Player>();
    }

    public void OnValueChanged(NotificationType type, Player player)
    {
        switch (type)
        {
            case NotificationType.BestLap:
                if (player.BestLapTime < bestLapTime)
                {
                    bestLapTime = player.BestLapTime;
                    bestLappedPlayer = player;
                }
                break;
            case NotificationType.LapsFinished:
                result.Add(player);
                if (result.Count == players.Count)
                {
                    FinishRace();
                }
                break;
        }
    }

    private void FinishRace()
    {
        //Assign Points
        for (int i = 0; i < (points.Length >= result.Count ? result.Count : points.Length); i++)
        {
            result[i].point += points[i];
        }
        bestLappedPlayer.point += BEST_LAP_POINT;

        foreach (Player player in result)
        {
            Debug.Log("Car Name:" + player.transform.name + " Point:" + player.point);
        }
    }
}