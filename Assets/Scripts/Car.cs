using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public CarProperty carProperty;

    public Transform centerOfMass;

    private Wheel[] wheels;

    private float motorTorque;
    private float steeringAngle;

    public float Steer { get; set; }
    public float Throttle { get; set; }
    

    private void Awake()
    {
        motorTorque = carProperty.motorTorque;
        steeringAngle = carProperty.steeringAngle;
        wheels = GetComponentsInChildren<Wheel>();
        GetComponent<Rigidbody>().mass = carProperty.massOfCar;
        GetComponent<Rigidbody>().centerOfMass = centerOfMass.localPosition;
    }

    private void Update()
    {

        Steer = GameManager.GetInstance().inputManager.SteerInput;
        Throttle = GameManager.GetInstance().inputManager.ThrottleInput;

        foreach (var wheel in wheels)
        {
            wheel.SteerAngle = Steer * steeringAngle;
            wheel.Torque = Throttle * motorTorque;
        }
    }



}
