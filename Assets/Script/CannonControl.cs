using ChobiAssets.KTP;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CannonControl : MonoBehaviour
{
    [Header("Cannon movement settings")]
    [Tooltip("Rotation speed. (Degree per second)")] public float rotationSpeed = 10.0f;
    [Tooltip("Time to reach the maximum speed from zero. (Sec)")] public float accelerationTime = 0.2f;
    [Tooltip("Time to stop from the maximum speed. (Sec)")] public float decelerationTime = 0.2f;
    [Tooltip("Maximum elevation angle. (Degree)")] public float maxElevation = 15.0f;
    [Tooltip("Maximum depression angle. (Degree)")] public float maxDepression = 10.0f;



    [Header("Positon Component")]


    Transform thisTransform;
    Transform turretBaseTransform;
    Aiming_Control_CS aimingScript;
    public bool isTurning;
    public bool isTracking;
    float angleX;
    Vector3 currentLocalAngles;
    float turnRate;
    float previousTurnRate;
    float bulletVelocity = 250.0f;
    public Vector3 targetposition;


    private void Awake()
    {
        thisTransform = transform;
        turretBaseTransform = thisTransform.parent;
        currentLocalAngles = thisTransform.localEulerAngles;
        angleX = currentLocalAngles.x;
        maxElevation = angleX - maxElevation;
        maxDepression = angleX + maxDepression;
    }


    void FixedUpdate()
    {
        if (isTracking)
        {
            Auto_Turn();


        }

    }
    void Auto_Turn()
    {
        if (!isTurning) return;

        // Calculate the direction to the target
        Vector3 directionToTarget = targetposition - thisTransform.position;
        directionToTarget.y = 0; // Ignore vertical component for horizontal rotation
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

        // Smoothly rotate the turret toward the target
        thisTransform.rotation = Quaternion.RotateTowards(
            thisTransform.rotation,
            targetRotation,
            rotationSpeed * Time.fixedDeltaTime
        );

        // Check if the turret has reached the target rotation
        if (Quaternion.Angle(thisTransform.rotation, targetRotation) < 0.1f)
        {
            isTurning = false; // Stop turning when close enough
        }
    }



    public float Auto_Elevation_Angle(Vector3 targetPosition)
    {
        Vector3 directionToTarget = targetPosition - thisTransform.position;

        // Horizontal distance and vertical difference
        float horizontalDistance = new Vector2(directionToTarget.x, directionToTarget.z).magnitude;
        float verticalDifference = directionToTarget.y;

        // Calculate the angle using projectile motion formula
        float gravity = Mathf.Abs(Physics.gravity.y);
        float velocitySquared = Mathf.Pow(bulletVelocity, 2f);
        float discriminant = Mathf.Pow(bulletVelocity, 4f) - gravity * (gravity * Mathf.Pow(horizontalDistance, 2f) + 2 * verticalDifference * velocitySquared);

        if (discriminant < 0)
        {
            Debug.Log("No valid solution for the elevation angle. Target is out of range.");
            return 0f; // Default angle
        }

        float sqrtDiscriminant = Mathf.Sqrt(discriminant);
        float angleRadians = Mathf.Atan((velocitySquared - sqrtDiscriminant) / (gravity * horizontalDistance));

        return Mathf.Rad2Deg * angleRadians; // Return angle in degrees
    }



}