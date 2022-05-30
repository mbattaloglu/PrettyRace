using System.Collections.Generic;
using UnityEngine;

public class PointHandler : MonoBehaviour, IObserver
{
    //TODO : Turnuva modunu kur.
    private static PointHandler instance;

    private readonly int[] points = new int[] { 6, 5, 4, 3, 2, 1 };
    private const int BEST_LAP_POINT = 1;

    public List<Player> players;
    public List<Player> raceResult;

    private Player bestLappedPlayer;
    private float bestLapTime;

    private PointHandler()
    {
        if (instance == null) instance = this;
    }

    public static PointHandler GetInstance()
    {
        return instance;
    }
    private void Start()
    {
        //From first to sixth
        bestLapTime = Mathf.Infinity;

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
                raceResult.Add(player);
                if (player.playerMode == PlayerMode.Human)
                {
                    player.playerMode = PlayerMode.AI;
                    player.gameObject.AddComponent<AI>();
                    player.gameObject.GetComponent<AI>().AIMode = AIMode.FollowWaypoints;
                    Camera.main.transform.localPosition = new Vector3(0, 7, 0);
                    Camera.main.transform.rotation = Quaternion.Euler(new Vector3(75, 0, 0));
                }
                if (raceResult.Count == players.Count)
                {
                    FinishRace();
                }
                break;
        }
    }

    private void FinishRace()
    {
        //Assign Points
        for (int i = 0; i < (points.Length >= raceResult.Count ? raceResult.Count : points.Length); i++)
        {
            raceResult[i].totalPoint += points[i];
            raceResult[i].lastPointsEarned = points[i];

        }
        bestLappedPlayer.totalPoint += BEST_LAP_POINT;
        bestLappedPlayer.lastPointsEarned += BEST_LAP_POINT;

        GameManager.GetInstance().isRaceOver = true;
    }
}