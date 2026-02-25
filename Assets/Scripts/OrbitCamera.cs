using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    public Transform target; 

    public float mouseSensitivity = 3.0f; 
    public float distanceFromTarget = 4.0f; 
    public Vector2 pitchLimits = new Vector2(-10, 60); 
    public Vector3 offset = new Vector3(0, 1.5f, 0); 

    private float yaw;   
    private float pitch; 

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        if (!target) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, pitchLimits.x, pitchLimits.y); 

        Vector3 targetPos = target.position + offset;
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        transform.position = targetPos - (rotation * Vector3.forward * distanceFromTarget);

        transform.LookAt(targetPos);
    }
}