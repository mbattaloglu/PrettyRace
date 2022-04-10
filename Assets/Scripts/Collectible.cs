using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    float rotationSpeed = 5;

    private void FixedUpdate()
    {
        transform.Rotate(0, 1 * rotationSpeed, 0);
    }
}
