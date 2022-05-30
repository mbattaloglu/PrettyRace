using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TournamentHandler : MonoBehaviour
{
    private static TournamentHandler instance;

    public GameObject playerCar;

    [HideInInspector]
    public string playerCarName;

    public Transform AICars;
    
    private Transform startingPoints;


    private TournamentHandler()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public static TournamentHandler GetInstance()
    {
        return instance;
    }
    private void Start()
    {
        startingPoints = GameManager.GetInstance().startingPoints;
    }

    public void CreateRaceEvent()
    {
        AICars = GameObject.Find("AICars").transform;

        for (int i = 0; i < AICars.childCount; i++)
        {
            AICars.GetChild(i).GetComponent<Player>().checkpointsParent = GameManager.GetInstance().checkpoints;
        }

        GameManager.GetInstance().CreatePlayerCar();
        playerCar = GameObject.Find("Player");

        playerCar.transform.position = startingPoints.GetChild(AICars.childCount).position;
        playerCar.SetActive(true);

    }
}
