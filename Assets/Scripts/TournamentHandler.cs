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

    public GameObject[] tracks;
    [HideInInspector]
    public GameObject activeTrack;

    private int trackIndex;

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
        trackIndex = 0; 
    }

    public void CreateRaceEvent()
    {
        activeTrack = Instantiate(tracks[trackIndex], tracks[trackIndex].transform.position, tracks[trackIndex].transform.rotation);
        activeTrack.name = "ActiveTrack";
        startingPoints = activeTrack.transform.GetChild(4);

        AICars = GameObject.Find("AICars").transform;

        for (int i = 0; i < AICars.childCount; i++)
        {
            AICars.GetChild(i).GetComponent<Player>().checkpointsParent = activeTrack.transform.GetChild(2);
        }

        GameManager.GetInstance().CreatePlayerCar();
        playerCar = GameObject.Find("Player");

        playerCar.transform.position = startingPoints.GetChild(AICars.childCount).position;
        playerCar.SetActive(true);

    }
}
