using UnityEngine;

public class MouseParallax : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Сила смещения. Чем больше число, тем сильнее двигается объект.")]
    public float parallaxStrength = 20f;

    [Tooltip("Скорость сглаживания движения (Lerp).")]
    public float smoothing = 5f;

    [Tooltip("Если true, инвертирует движение (объект двигается против мыши).")]
    public bool invertDirection = false;

    private Vector3 _startPosition;
    private Vector3 _targetPosition;

    void Start()
    {
        // Запоминаем начальную позицию объекта
        _startPosition = transform.localPosition;
    }

    void Update()
    {
        // 1. Получаем позицию мыши
        // Мы используем координаты от центра экрана (-0.5 до 0.5), чтобы быть независимыми от разрешения
        float xMouse = (Input.mousePosition.x - Screen.width / 2) / Screen.width;
        float yMouse = (Input.mousePosition.y - Screen.height / 2) / Screen.height;

        // 2. Рассчитываем смещение
        float direction = invertDirection ? -1f : 1f;

        // Умножаем на силу эффекта
        float offsetX = xMouse * parallaxStrength * direction;
        float offsetY = yMouse * parallaxStrength * direction;

        // 3. Вычисляем целевую позицию
        _targetPosition = new Vector3(
            _startPosition.x + offsetX,
            _startPosition.y + offsetY,
            _startPosition.z
        );

        // 4. Плавно двигаем объект к цели (интерполяция)
        transform.localPosition = Vector3.Lerp(transform.localPosition, _targetPosition, Time.deltaTime * smoothing);
    }
}
