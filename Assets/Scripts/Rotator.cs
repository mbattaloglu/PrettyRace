using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    float angle = 1;
    private void FixedUpdate()
    {
        transform.Rotate(0, angle, 0);
    }
}
