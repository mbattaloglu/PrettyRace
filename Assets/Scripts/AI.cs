using System.Linq;
using UnityEngine;

public class AI : MonoBehaviour
{
    private GameManager gameManager;
    [HideInInspector]
    public Transform startingPosition;

    [Header("AI Settings")]
    public AIMode AIMode;

    [Header("Sensor Settings")]
    public float sensorLength = 5f;
    public Vector3 frontSensorPosition = new Vector3(0f, 1f, 3f);
    public float frontSideSensorPosition = 1.5f;
    public float sensorAngle = 30;

    private float maxSpeed = 30f;
    private int carLayer;


    private Car car;

    private Vector3 targetPosition = Vector3.zero;
    private Transform targetTransform = null;

    private WaypointNode currentWaypoint = null;
    private WaypointNode previoustWaypoint = null;
    private WaypointNode[] allWaypoints;

    private void Awake()
    {
        gameManager = GameManager.GetInstance();
        carLayer = 1 << LayerMask.NameToLayer("Car");
        car = GetComponent<Car>();
        allWaypoints = FindObjectsOfType<WaypointNode>();
    }

    private void FixedUpdate()
    {
        if (!gameManager.isGameStarted) return;
        if (gameManager.isGamePaused) return;

        switch (AIMode)
        {
            case AIMode.FollowPlayer:
                FollowPlayer();
                break;
            case AIMode.FollowWaypoints:
                FollowWaypoints();
                break;
        }

        car.Steer = TurnTowardsTarget();
        car.Throttle = ApplyThrottleOrBreak(car.Steer);
    }

    void FollowPlayer()
    {
        if (targetTransform == null) targetTransform = GameObject.FindWithTag("Player").transform;
        if (targetTransform != null) targetPosition = targetTransform.position;
    }

    void FollowWaypoints()
    {
        if (currentWaypoint == null)
        {
            currentWaypoint = FindStartingWaypoint();
            previoustWaypoint = currentWaypoint;
        } 

        if (currentWaypoint != null)
        {
            targetPosition = currentWaypoint.transform.position;
            float distanceToWayPoint = Vector3.Distance(targetPosition, transform.position);

            if(distanceToWayPoint > 20)
            {
                Vector3 nearestPointOnTheWayPointLine = FindNearestPointOnLine(previoustWaypoint.transform.position, currentWaypoint.transform.position, transform.position);
                float segments = distanceToWayPoint / 20.0f;
                targetPosition = (targetPosition + nearestPointOnTheWayPointLine * segments) / (segments + 1);
                Debug.DrawLine(transform.position, targetPosition, Color.green);
            }
            if (distanceToWayPoint <= currentWaypoint.minDistanceToReachWaypoint)
            {
                if (currentWaypoint.applySpeedLimit) maxSpeed = car.maxSpeedToEnteringTurn;
                else maxSpeed = 60;

                previoustWaypoint = currentWaypoint;
                currentWaypoint = currentWaypoint.nextWaypointNode[Random.Range(0, currentWaypoint.nextWaypointNode.Length)];
            }
        }
    }

    WaypointNode FindClosestWaypoint()
    {
        return allWaypoints
        .OrderBy(t => Vector3.Distance(transform.position, t.transform.position))
            .FirstOrDefault();
    }

    WaypointNode FindStartingWaypoint()
    {
        return GameObject.FindGameObjectWithTag("Starting Waypoint").GetComponent<WaypointNode>();
    }

    float TurnTowardsTarget()
    {
        Vector3 vectorToTarget = targetPosition - transform.position;
        vectorToTarget = vectorToTarget.normalized;

        ActivateSensors(vectorToTarget, out vectorToTarget);

        float angleToTarget = Vector3.SignedAngle(transform.forward, vectorToTarget, Vector3.up);
        float steerAmount = angleToTarget / 45.0f;
        steerAmount = Mathf.Clamp(steerAmount, -1.0f, 1.0f);

        return steerAmount;
    }

    float ApplyThrottleOrBreak(float turn)
    {
        if (car.speed > maxSpeed)
        {
            return -(car.speed * 0.08f) * (car.motorTorque * 0.008f);
        }

        if(car.speed < 5f){
            return 3f;
        }

        return 1.05f - Mathf.Abs(turn) / 1.0f;
    }

    private Vector3 FindNearestPointOnLine(Vector3 lineStartPosition, Vector3 lineEndPosition, Vector3 point)
    {
        Vector3 lineHeadingVector = lineEndPosition - lineStartPosition;

        float maxDistance = lineHeadingVector.magnitude;
        lineHeadingVector.Normalize();

        Vector3 lineVectorStartToPoint = point - lineStartPosition;
        float dot = Vector3.Dot(lineVectorStartToPoint, lineHeadingVector);

        dot = Mathf.Clamp(dot, 0f, maxDistance);

        return lineStartPosition + lineHeadingVector * dot;
    }

    private void ActivateSensors(Vector3 vectorToTarget, out Vector3 newTargetVector)
    {
        RaycastHit hit;
        Vector3 sensorStartPos = transform.position;
        sensorStartPos += transform.forward * frontSensorPosition.z;
        sensorStartPos += transform.up * frontSensorPosition.y;

        //Check front center
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength, carLayer))
        {
            Vector3 avoidanceVector = Vector3.Reflect((hit.point - transform.position).normalized, -hit.transform.right);
            newTargetVector = avoidanceVector;
            newTargetVector.Normalize();
            Debug.DrawRay(sensorStartPos + transform.up, transform.forward * sensorLength, Color.red);
            return;
        }

        //Check front right
        sensorStartPos += transform.right * frontSideSensorPosition;
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength, carLayer))
        {
            Vector3 avoidanceVector = Vector3.Reflect((hit.point - transform.position).normalized, -hit.transform.right);
            newTargetVector = avoidanceVector;
            avoidanceVector.Normalize();
            Debug.DrawRay(sensorStartPos + transform.up, transform.forward * sensorLength, Color.red);
            return;
        }

        //Check front right with angle
        else if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(sensorAngle, transform.up) * transform.forward, out hit, sensorLength, carLayer))
        {
            Vector3 avoidanceVector = Vector3.Reflect((hit.point - transform.position).normalized, hit.transform.right);
            newTargetVector = avoidanceVector;
            avoidanceVector.Normalize();
            Debug.DrawRay(sensorStartPos + transform.up, Quaternion.AngleAxis(sensorAngle, transform.up) * transform.forward * sensorLength, Color.red);
            return;
        }

        //Check front left
        sensorStartPos -= 2 * transform.right * frontSideSensorPosition;
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength, carLayer))
        {
            Vector3 avoidanceVector = Vector3.Reflect((hit.point - transform.position).normalized, hit.transform.right);
            newTargetVector = avoidanceVector;
            avoidanceVector.Normalize();
            Debug.DrawRay(sensorStartPos + transform.up, transform.forward * sensorLength, Color.red);
            return;
        }


        //Check front left with angle
        else if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-sensorAngle, transform.up) * transform.forward, out hit, sensorLength, carLayer))
        {
            Vector3 avoidanceVector = Vector3.Reflect((hit.point - transform.position).normalized, hit.transform.right);
            newTargetVector = avoidanceVector;
            avoidanceVector.Normalize();
            Debug.DrawRay(sensorStartPos + transform.up, Quaternion.AngleAxis(-sensorAngle, transform.up) * transform.forward * sensorLength, Color.red);
            return;
        }

        newTargetVector = vectorToTarget;

    }

    private void Initialize()
    {
        //TODO: Complete this method.
        currentWaypoint = null;
        transform.position = startingPosition.position;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        //TODO: Check rigidbody.Sleep() and rigidbody.WakeUp().
        GetComponent<Rigidbody>().Sleep();
        GetComponent<Rigidbody>().WakeUp();
    }
}
