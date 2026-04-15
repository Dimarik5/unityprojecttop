using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    public float mouseSensitivity = 1000f;
    private float xRotation = 1f;
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    public int maxJumps = 2;
    private int jumpsLeft;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Проверка, стоит ли персонаж на земле
        isGrounded = controller.isGrounded;
        if (isGrounded)
        {
            jumpsLeft = maxJumps;
            if (velocity.y < 0)
            {
                velocity.y = -2f;
            }
        }

        // Прыжок
        if (Input.GetButtonDown("Jump") && jumpsLeft > 0)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpsLeft--; // Уменьшаем количество оставшихся прыжков
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseX;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.Rotate(Vector3.up * mouseX);

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * speed * Time.deltaTime);

        // Применение гравитации
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}