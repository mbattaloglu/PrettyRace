using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNode : MonoBehaviour
{
    public bool applySpeedLimit;
    public float minDistanceToReachWaypoint = 5;
    
    public WaypointNode[] nextWaypointNode;

}
