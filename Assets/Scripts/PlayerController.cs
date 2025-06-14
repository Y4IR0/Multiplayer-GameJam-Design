using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float controllerDeadzone = 0.1f;

    public GameObject gun;
    public bool isAiming = false;
    
    private Vector2 movement;
    private Vector2 aim;
    private Vector3 defaultScale;

    private PlayerInput playerInput;
    private Rigidbody2D rb;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        defaultScale = transform.localScale;
    }

    void Update()
    {
        HandleInput();
        HandleRotation();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleInput()
    {
        movement = playerInput.actions["Move"].ReadValue<Vector2>();
        aim = playerInput.actions["Look"].ReadValue<Vector2>();
    }

    private void HandleMovement()
    {
        rb.linearVelocity = movement * moveSpeed;
    }

    private void HandleRotation()
    {
        if (aim.sqrMagnitude > controllerDeadzone * controllerDeadzone)
        {
            isAiming = true;
            
            //gun.SetActive(true);
            gun.GetComponent<SpriteHider>().hideChildrenSprites = false;
            float angle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);

            // Flip sprite
            bool isFlipped = angle > 90 || angle < -90;
            transform.localScale = new Vector3(defaultScale.x, isFlipped ? -defaultScale.y : defaultScale.y, defaultScale.z);
        }
        else
        {
            isAiming = false;
            
            //gun.SetActive(false);
            gun.GetComponent<SpriteHider>().hideChildrenSprites = true;
        }
    }
    
    
}
