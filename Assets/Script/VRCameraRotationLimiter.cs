using UnityEngine;
using UnityEngine.XR;

public class VRCameraRotationLimiter : MonoBehaviour
{
    public Transform vrCamera; // Assign the Main Camera
    public float maxYRotation = 45f; // Limit Y-axis rotation
    public float minYRotation = -45f;

    private float currentYRotation;

    void LateUpdate()
    {
        Vector3 euler = vrCamera.rotation.eulerAngles;

        // Convert to -180 to 180 range for proper clamping
        currentYRotation = (euler.y > 180) ? euler.y - 360 : euler.y;

        // Clamp Y rotation within min-max range
        currentYRotation = Mathf.Clamp(currentYRotation, minYRotation, maxYRotation);

        // Apply rotation with clamped values
        vrCamera.rotation = Quaternion.Euler(0, currentYRotation, 0);
    }
}
