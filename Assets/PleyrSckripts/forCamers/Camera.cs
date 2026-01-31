using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset = new Vector3(0, 15, 0); // Высота камеры
    [SerializeField] private float smoothSpeed = 5f;

    private void LateUpdate()
    {
        // Позиция камеры = позиция игрока + смещение
        Vector3 desiredPosition = player.position + offset;

        // Плавное перемещение
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

    }
}