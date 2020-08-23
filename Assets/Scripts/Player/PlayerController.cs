using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 12f;
    public float mouseSensitivity;

    private float cameraAngleY = 0f;

    private Camera playerCamera;
    private CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = transform.Find("Main Camera").GetComponent<Camera>();
    }

    private void Start()
    {
        LockCamera();
    }

    private void Update()
    {
        if (CanMoveCamera())
        {
            HandleMovement();
            HandleMouseLook();

            if (Input.GetButton("Cancel"))
            {
                UnlockCamera();
            }
        }

        if (!CanMoveCamera())
        {
            if (Input.GetButton("Fire1"))
            {
                LockCamera();
            }
        }
    }

    private void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
    }

    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        transform.Rotate(Vector3.up * mouseX * mouseSensitivity);

        cameraAngleY -= mouseY * mouseSensitivity;
        // Clamp xRotation to prevent the camera pointing behind the player's head
        cameraAngleY = Mathf.Clamp(cameraAngleY, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(cameraAngleY, 0f, 0f);
    }

    private bool CanMoveCamera()
    {
        return Cursor.lockState == CursorLockMode.Locked;
    }

    private void LockCamera()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void UnlockCamera()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
