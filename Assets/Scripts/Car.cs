using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public CarProperty carProperty;

    public Transform centerOfMass;

    public WheelCollider frontLeftCollider;
    public WheelCollider rearLeftCollider;
    public WheelCollider frontRightCollider;
    public WheelCollider rearRightCollider;

    public Transform frontLeftTransform;
    public Transform rearLeftTransform;
    public Transform frontRightTransform;
    public Transform rearRightTransform;
    

    private void Awake()
    {
        GetComponent<Rigidbody>().mass = carProperty.massOfCar;
        GetComponent<Rigidbody>().centerOfMass = centerOfMass.localPosition;
    }


    private void FixedUpdate()
    {
        rearLeftCollider.motorTorque = Input.GetAxis("Vertical") * carProperty.motorTorque;
        rearRightCollider.motorTorque = Input.GetAxis("Vertical") * carProperty.motorTorque;

        frontLeftCollider.steerAngle = Input.GetAxis("Horizontal") * carProperty.steeringAngle;
        frontRightCollider.steerAngle = Input.GetAxis("Horizontal") * carProperty.steeringAngle;
    }

    private void Update()
    {
        var pos = Vector3.zero;
        var rot = Quaternion.identity;

        frontLeftCollider.GetWorldPose(out pos, out rot);
        frontLeftTransform.position = pos;
        frontLeftTransform.rotation = rot;

        frontRightCollider.GetWorldPose(out pos, out rot);
        frontRightTransform.position = pos;
        frontRightTransform.rotation = rot * Quaternion.Euler(0, 180, 0);

        rearLeftCollider.GetWorldPose(out pos, out rot);
        rearLeftTransform.position = pos;
        rearLeftTransform.rotation = rot;

        rearRightCollider.GetWorldPose(out pos, out rot);
        rearRightTransform.position = pos;
        rearRightTransform.rotation = rot * Quaternion.Euler(0, 180, 0);
    }
}
