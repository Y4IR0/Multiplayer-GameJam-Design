using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float controllerDeadzone = 0.1f;

    public GameObject gun;

    private Vector2 movement;
    private Vector2 aim;

    private PlayerInput playerInput;
    private CharacterController characterController;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleInput();
        HandleMovement();
        HandleRotation();
    }

    private void HandleInput()
    {
        movement = playerInput.actions["Move"].ReadValue<Vector2>();
        aim = playerInput.actions["Look"].ReadValue<Vector2>();
    }

    private void HandleMovement()
    {
        Vector3 move = new Vector3(movement.x, movement.y);
        characterController.Move(move * Time.deltaTime * moveSpeed);
    }

    private void HandleRotation()
    {
        if (aim.sqrMagnitude > controllerDeadzone * controllerDeadzone)
        {
            gun.gameObject.SetActive(true);
            float angle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
        else
        {
            gun.gameObject.SetActive(false);
        }
    }
}
