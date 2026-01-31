using UnityEngine;
using UnityEngine.InputSystem;

public class TopDownMouseLook : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10f;
    PlayerInput inputActions;
    // Для мыши
    private Vector2 mousePosition;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }
    private void Awake()
    {
        inputActions = new();
        inputActions.Move.Look.Enable();

        inputActions.Move.Look.performed += OnLook;

    }
    // Метод для Input System
    public void OnLook(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        // Если используется мышь
        if (Mouse.current != null)
        {
            // Преобразуем позицию мыши в луч
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);
            Plane groundPlane = new Plane(Vector3.up, transform.position);

            if (groundPlane.Raycast(ray, out float distance))
            {
                Vector3 targetPoint = ray.GetPoint(distance);
                Vector3 direction = targetPoint - transform.position;
                direction.y = 0;

                if (direction != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(
                        transform.rotation,
                        targetRotation,
                        rotationSpeed * Time.deltaTime
                    );
                }
            }
        }
    }
}