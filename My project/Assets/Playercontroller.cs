using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Playercontroller : MonoBehaviour
{
    [Header("Disparo")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Movimiento")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float gravity = -9.81f;

    [Header("Dash / Esquiva")]
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 3f;

    private CharacterController controller;
    private Camera mainCamera;
    private Vector3 velocity;

    private bool isDashing = false;
    private float dashTimer = 0f;
    private float cooldownTimer = 0f;
    private Vector3 dashDirection;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        // 1. Temporizador del Dash
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        // 2. Activar Dash con Espacio
        if (Input.GetKeyDown(KeyCode.Space) && cooldownTimer <= 0)
        {
            StartDash();
        }

        // 3. Comportamiento durante el Dash o Movimiento normal
        if (isDashing)
        {
            controller.Move(dashDirection * dashSpeed * Time.deltaTime);
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0)
            {
                isDashing = false;
            }
        }
        else
        {
            HandleMovement();
            HandleAiming();
        }

        // 4. Disparar con Clic Izquierdo del Ratón
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(moveX, 0f, moveZ).normalized;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Gravedad básica
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }
        controller.Move(velocity * Time.deltaTime);
    }

    private void HandleAiming()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, transform.position);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 pointToLook = ray.GetPoint(rayDistance);
            Vector3 lookDirection = pointToLook - transform.position;
            lookDirection.y = 0f;

            if (lookDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(lookDirection);
            }
        }
    }

    private void StartDash()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = new Vector3(moveX, 0f, moveZ).normalized;
        dashDirection = inputDir != Vector3.zero ? inputDir : transform.forward;

        isDashing = true;
        dashTimer = dashDuration;
        cooldownTimer = dashCooldown;
    }

    void Shoot()
    {
        if (bulletPrefab != null)
        {
            Transform spawnPoint = firePoint != null ? firePoint : transform;
            Instantiate(bulletPrefab, spawnPoint.position + transform.forward * 1f, spawnPoint.rotation);
        }
    }
}