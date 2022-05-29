using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour, ISubejct
{
    private GameManager gameManager;
    public List<IObserver> observers;
    public PlayerMode playerMode;
    public float BestLapTime { get; private set; } = Mathf.Infinity;
    public float LastLapTime { get; private set; } = 0;
    public float CurrentLapTime { get; private set; } = 0;
    public int CurrentLap { get; private set; } = 0;

    private float lapTimerTimeStamp;
    private int lastCheckpointPassed = 0;

    private Transform checkpointsParent;
    private int checkpointCount;
    private int checkpointLayer;
    private Car car;
    public int point;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != checkpointLayer)
        {
            return;
        }
        if (other.gameObject.name == "1")
        {
            if (lastCheckpointPassed == checkpointCount)
            {
                EndLap();
            }

            if (CurrentLap == 0 || lastCheckpointPassed == checkpointCount)
            {
                StartLap();
            }
        }

        if (other.gameObject.name == (lastCheckpointPassed + 1).ToString())
        {
            lastCheckpointPassed++;
        }
    }

    private void Awake()
    {
        //LINQ
        observers = FindObjectsOfType<MonoBehaviour>().OfType<IObserver>().ToList();
        checkpointsParent = GameObject.Find("Checkpoints").transform;
        checkpointCount = checkpointsParent.childCount;
        checkpointLayer = LayerMask.NameToLayer("Checkpoint");
        car = GetComponent<Car>();
        gameManager = GameManager.GetInstance();
    }

    private void Update()
    {
        if (!gameManager.isGameStarted) return;
        if (gameManager.isGamePaused) return;

        CurrentLapTime = lapTimerTimeStamp > 0 ? Time.time - lapTimerTimeStamp : 0;
        if (playerMode == PlayerMode.Human)
        {
            car.Steer = Input.GetAxis("Horizontal");
            car.Throttle = Input.GetAxis("Vertical");
        }
    }

    private void StartLap()
    {
        CurrentLap++;
        lastCheckpointPassed = 1;
        lapTimerTimeStamp = Time.time;
        if (CurrentLap == 3)
        {
            CurrentLap = 0;
            NotifyObservers(NotificationType.LapsFinished, this);
        }
    }

    private void EndLap()
    {
        LastLapTime = Time.time - lapTimerTimeStamp;
        if (LastLapTime < BestLapTime)
        {
            BestLapTime = LastLapTime;
            NotifyObservers(NotificationType.BestLap, this);
        }
    }

    public void Register(IObserver observer)
    {
        observers.Add(observer);
    }

    public void NotifyObservers(NotificationType type, Player player)
    {
        foreach (IObserver observer in observers)
        {
            observer.OnValueChanged(type, player);
        }
    }
}
