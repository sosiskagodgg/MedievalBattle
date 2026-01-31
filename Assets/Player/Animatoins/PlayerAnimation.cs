using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private Vector3 lastPosition;
    private Vector3 currentVelocity;
    private Vector3 localVelocity;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        lastPosition = transform.position;
    }

    void Update()
    {
        // 1. Считаем скорость
        Vector3 displacement = transform.position - lastPosition;
        currentVelocity = displacement / Time.deltaTime;
        lastPosition = transform.position;

        // 2. Преобразуем в локальное пространство (куда смотрит игрок)
        localVelocity = transform.InverseTransformDirection(currentVelocity);

        // 3. Игнорируем вертикальную составляющую (гравитация)
        localVelocity.y = 0;

        // 4. Обновляем аниматор
        if (animator != null)
        {
            // Общая скорость (0-1)
            float speed = Mathf.Clamp01(localVelocity.magnitude / 5f);
            animator.SetFloat("Speed", speed);

            // Направление относительно игрока
            Vector2 direction = new Vector2(localVelocity.x, localVelocity.z).normalized;
            animator.SetFloat("MoveX", direction.x);
            animator.SetFloat("MoveY", direction.y);
        }
    }
}