using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 5f;
    [SerializeField] private float _mouseSensitivity = 2f;
    [SerializeField] private Camera playerCam;

    private CharacterController _characterController;
    private Vector3 _velocity;
    private Vector3 _movementDirection;
    private float _pitch;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        if (playerCam == null) playerCam = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        _pitch = Mathf.Clamp(_pitch - mouseY, -85f, 85f);
        playerCam.transform.localRotation = Quaternion.Euler(_pitch, 0f, 0f);

        // movement relative to where the player faces
        _movementDirection = (transform.forward * Input.GetAxisRaw("Vertical")
                            + transform.right * Input.GetAxisRaw("Horizontal")).normalized;

        // escape to free cursor
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        //click back in
        if(Input.GetMouseButtonDown(0) && Cursor.lockState == CursorLockMode.None )
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }


        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // ray hits any collider 
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject.TryGetComponent<Iinteract>(out var interactable))
                {
                    interactable.interact();
                }

            }
        }
    }

    private void LateUpdate()
    {
        // Gravity
        if (_characterController.isGrounded && _velocity.y < 0f)
            _velocity.y = -2f;

        _velocity.y += -9.81f * Time.deltaTime;

        Vector3 move = (_movementDirection * _playerSpeed) + _velocity;
        _characterController.Move(move * Time.deltaTime);
    }
}


public interface Iinteract
{
    void interact();
}