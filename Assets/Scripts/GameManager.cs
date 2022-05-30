using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager gameManager;

    public string playerCarName;
    public GameObject playerCar;
    public GameObject[] carPrefabs;
    public Transform player;
    public Transform AICars;
    public Transform startingPoints;

    public GameObject[] createdCars;

    [HideInInspector]
    public float powerUp;
    [HideInInspector]
    public bool isGameStarted;
    [HideInInspector]
    public bool isGamePaused;
    [HideInInspector]
    public bool isRaceOver;

    private GameManager()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
    }

    public static GameManager GetInstance()
    {
        return gameManager;
    }
    private void Awake()
    {
        //Initialize();
    }

    public void Initialize()
    {
        powerUp = 1;
        isGameStarted = false;
        isGamePaused = false;

        CreateAICars();
    }

    public void CreateAICars()
    {
        //Generate Random Car Indexes
        HashSet<int> indexes = new HashSet<int>();
        System.Random random = new System.Random();
        while (indexes.Count < random.Next(6, 10))
        {
            indexes.Add(random.Next(10));
        }

        createdCars = new GameObject[indexes.Count];

        //Create AI Cars
        for (int i = 0; i < indexes.Count; i++)
        {
            GameObject car = Instantiate(carPrefabs[i], startingPoints.GetChild(i).position, Quaternion.identity, AICars);
            createdCars[i] = car;
            car.GetComponent<Car>().ID = i;
            car.AddComponent<AI>();
            car.GetComponent<AI>().AIMode = AIMode.FollowWaypoints;
            car.AddComponent<Player>();
            car.GetComponent<Player>().enabled = true;
            car.GetComponent<Player>().playerMode = PlayerMode.AI;
            PointHandler.GetInstance().players.Add(car.GetComponent<Player>());
        }
    }

    public void CreatePlayerCar()
    {
            
        int temp = 0;
        for (int i = 0; i < carPrefabs.Length; i++)
        {
            if (carPrefabs[i].name == playerCarName)
            {
                temp = i;
                break;
            }
        }
        playerCar = Instantiate(carPrefabs[temp], startingPoints.GetChild(createdCars.Length).position, Quaternion.identity);
        playerCar.name = "Player";
        playerCar.AddComponent<Player>();
        playerCar.GetComponent<Player>().playerMode = PlayerMode.Human;
        Camera.main.transform.parent = playerCar.transform;
        Camera.main.transform.localPosition = new Vector3(0, 4f, -6);
        Camera.main.transform.rotation = Quaternion.Euler(new Vector3(25, 0, 0));
        PointHandler.GetInstance().players.Add(playerCar.GetComponent<Player>());
    }

    private void OnEnable()
    {
        Initialize();
    }
}
