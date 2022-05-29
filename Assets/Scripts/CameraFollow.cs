using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public Vector3 offset;
    public Vector3 eulerRotation;

    public float damper;
    private void Start()
    {
        transform.eulerAngles = eulerRotation;
    }

    private void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
