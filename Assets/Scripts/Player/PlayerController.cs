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

    private void Update()
    {
        HandleMovement();
        HandleMouseLook();
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
}
