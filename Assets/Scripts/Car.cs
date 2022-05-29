
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public CarProperty carProperty;

    public Transform centerOfMass;

    private Wheel[] wheels;
    [HideInInspector]
    public float motorTorque;
    [HideInInspector]
    public float steeringAngle;
    [HideInInspector]
    public float maxSpeedToEnteringTurn;
    public float speed;

    public float Steer { get; set; }
    public float Throttle { get; set; }

    private void Awake()
    {
        motorTorque = carProperty.motorTorque;
        steeringAngle = carProperty.steeringAngle;
        maxSpeedToEnteringTurn = carProperty.maxSpeedToEnteringTurn;
        wheels = GetComponentsInChildren<Wheel>();
        GetComponent<Rigidbody>().mass = carProperty.massOfCar;
        GetComponent<Rigidbody>().centerOfMass = centerOfMass.localPosition;
    }

    private void Update()
    {
        speed = GetComponent<Rigidbody>().velocity.magnitude;   

        foreach (var wheel in wheels)
        {
            wheel.SteerAngle = Steer * steeringAngle;
            wheel.Torque = Throttle * motorTorque;
        }
    }


}
