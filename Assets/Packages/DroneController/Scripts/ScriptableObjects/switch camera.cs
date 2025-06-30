using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera fpv;     // Assign your FPV camera in the Inspector
    public new Camera camera;  // Assign your third-person camera in the Inspector

    void Start()
    {
        // Ensure only one camera is active at the start
        if (fpv != null && camera != null)
        {
            fpv.enabled = true;
            camera.enabled = false;
            Debug.Log("Cameras initialized successfully");
        }
        else
        {
            Debug.LogError("Cameras not assigned in Inspector.");
        }
    }

    void Update()
    {
        // Check if "V" key is pressed on keyboard or "O" button (joystick button 1) on controller
        if (Input.GetKeyDown(KeyCode.V) || Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            // Toggle between cameras
            if (fpv != null && camera != null)
            {
                fpv.enabled = !fpv.enabled;
                camera.enabled = !camera.enabled;
                Debug.Log("Camera switched. FPV is " + (fpv.enabled ? "enabled" : "disabled") + ".");
            }
            else
            {
                Debug.LogError("Cameras not assigned in Inspector.");
            }
        }
    }
}
