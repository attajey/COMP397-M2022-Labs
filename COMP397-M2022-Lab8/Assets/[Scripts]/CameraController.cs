using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 10.0f;
    public Transform playerBody;
    public Joystick rightJoystick;

    private float xRotation = 0.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float x = /* Input.GetAxis("Mouse X") * mouseSensitivity + */ rightJoystick.Horizontal;
        float y = /* Input.GetAxis("Mouse Y") * mouseSensitivity + */ rightJoystick.Vertical;

        xRotation -= y;
        xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);
        transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
        playerBody.Rotate(Vector3.up * x);

    }
}
