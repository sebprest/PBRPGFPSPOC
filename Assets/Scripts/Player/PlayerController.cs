using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 12f;
    public float mouseSensitivity = 1f;

    private float cameraAngleY = 0f;

    private Camera playerCamera;
    private CharacterController controller;
    private PartyMemberController[] partyMembers;
    private PartyMemberController currentPartyMember;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = transform.Find("Main Camera").GetComponent<Camera>();
        partyMembers = GetComponentsInChildren<PartyMemberController>();

        if (partyMembers.Length > 0)
        {
            currentPartyMember = partyMembers[0];
            currentPartyMember.ShowWeapon();
        }
        else
            Debug.LogError("No party members were found!");
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
            HandleSwitchingPartymembers();
            HandleAttacking();

            if (Input.GetButton("Cancel"))
                UnlockCamera();
        }

        if (!CanMoveCamera())
            if (Input.GetButton("Fire1"))
                LockCamera();
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

    private void HandleSwitchingPartymembers()
    {
        /* TODO: Implement a more robust solution that will enable the player
         * to have a party size other than four */
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SwitchToPartyMember(partyMembers[0]);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SwitchToPartyMember(partyMembers[1]);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            SwitchToPartyMember(partyMembers[2]);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            SwitchToPartyMember(partyMembers[3]);
    }

    private void SwitchToPartyMember(PartyMemberController partyMember)
    {
        currentPartyMember.HideWeapon();
        currentPartyMember = partyMember;
        currentPartyMember.ShowWeapon();
    }

    private void HandleAttacking()
    {
        if (Input.GetButton("Fire1"))
            currentPartyMember.Attack();
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
