using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Car Property", fileName = "NewCarProperty")]
public class CarProperty : ScriptableObject
{
    public float motorTorque;
    public float steeringAngle;
    public int massOfCar;
}
