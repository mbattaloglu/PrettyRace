using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager inputManager;

    private InputManager()
    {
        if (inputManager == null)
        {
            inputManager = this;
        }
    }
    public static InputManager GetInstance()
    {
        return inputManager;
    }

    private string steerAxis = "Horizontal";
    private string throttleAxis = "Vertical";

    public float SteerInput { get; private set; }
    public float ThrottleInput { get; private set; }

    private void Update()
    {
        SteerInput = Input.GetAxis(steerAxis);
        ThrottleInput = Input.GetAxis(throttleAxis);
    }
}
