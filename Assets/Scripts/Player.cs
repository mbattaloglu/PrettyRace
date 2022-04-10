using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ControlType controlType = ControlType.HumanInput;

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
    private float powerUp;

    private void Start()
    {
        powerUp = GameManager.GetInstance().powerUp;
    }

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
        checkpointsParent = GameObject.Find("Checkpoints").transform;
        checkpointCount = checkpointsParent.childCount;
        checkpointLayer = LayerMask.NameToLayer("Checkpoint");
        car = GetComponent<Car>();
    }

    private void Update()
    {
        CurrentLapTime = lapTimerTimeStamp > 0 ? Time.time - lapTimerTimeStamp : 0;
        if (controlType == ControlType.HumanInput)
        {
            car.Steer = GameManager.GetInstance().inputManager.SteerInput;
            car.Throttle = GameManager.GetInstance().inputManager.ThrottleInput * powerUp;
        }
    }

    private void StartLap()
    {
        CurrentLap++;
        lastCheckpointPassed = 1;
        lapTimerTimeStamp = Time.time;
    }

    private void EndLap()
    {
        LastLapTime = Time.time - lapTimerTimeStamp;
        BestLapTime = Mathf.Min(LastLapTime, BestLapTime);
    }

    


}
