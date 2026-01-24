using UnityEngine;

public class Tool : MonoBehaviour
{
    public static void DrawBox(Vector3 center, Vector3 size, Quaternion rotation, Color drawColor, float duration = 0.1f)
    {
        Vector3 halfExtents = size * 0.5f;
        Vector3[] corners = new Vector3[8]
        {
            new Vector3(-halfExtents.x, -halfExtents.y, -halfExtents.z),
            new Vector3(halfExtents.x, -halfExtents.y, -halfExtents.z),
            new Vector3(halfExtents.x, halfExtents.y, -halfExtents.z),
            new Vector3(-halfExtents.x, halfExtents.y, -halfExtents.z),
            new Vector3(-halfExtents.x, -halfExtents.y, halfExtents.z),
            new Vector3(halfExtents.x, -halfExtents.y, halfExtents.z),
            new Vector3(halfExtents.x, halfExtents.y, halfExtents.z),
            new Vector3(-halfExtents.x, halfExtents.y, halfExtents.z)
        };

        // Преобразуем углы в мировое пространство с учетом поворота и позиции
        for (int i = 0; i < 8; i++)
        {
            corners[i] = rotation * corners[i] + center;
        }

        // Рисуем 12 ребер куба
        Debug.DrawLine(corners[0], corners[1], drawColor, duration);
        Debug.DrawLine(corners[1], corners[2], drawColor, duration);
        Debug.DrawLine(corners[2], corners[3], drawColor, duration);
        Debug.DrawLine(corners[3], corners[0], drawColor, duration);

        Debug.DrawLine(corners[4], corners[5], drawColor, duration);
        Debug.DrawLine(corners[5], corners[6], drawColor, duration);
        Debug.DrawLine(corners[6], corners[7], drawColor, duration);
        Debug.DrawLine(corners[7], corners[4], drawColor, duration);

        Debug.DrawLine(corners[0], corners[4], drawColor, duration);
        Debug.DrawLine(corners[1], corners[5], drawColor, duration);
        Debug.DrawLine(corners[2], corners[6], drawColor, duration);
        Debug.DrawLine(corners[3], corners[7], drawColor, duration);
    }
    public static void DrawBox(Vector3 center, Vector2 size, float angle, Color drawColor, float duration = 0.1f)
    {
        // Преобразуем угол в радианы и создаем матрицу поворота
        float radAngle = angle * Mathf.Deg2Rad;
        float cos = Mathf.Cos(radAngle);
        float sin = Mathf.Sin(radAngle);

        // Половины размеров для 2D (в плоскости XY, Z = 0)
        Vector2 halfExtents = size * 0.5f;

        // Углы прямоугольника в локальном пространстве (без Z-координаты)
        Vector2[] localCorners = new Vector2[4]
        {
        new Vector2(-halfExtents.x, -halfExtents.y), // левый нижний
        new Vector2(halfExtents.x, -halfExtents.y),  // правый нижний
        new Vector2(halfExtents.x, halfExtents.y),   // правый верхний
        new Vector2(-halfExtents.x, halfExtents.y)   // левый верхний
        };

        // Преобразуем углы в мировое пространство с учетом поворота и позиции
        Vector3[] corners = new Vector3[4];
        for (int i = 0; i < 4; i++)
        {
            // Применяем поворот (матрица 2D поворота)
            Vector2 rotatedCorner = new Vector2(
                localCorners[i].x * cos - localCorners[i].y * sin,
                localCorners[i].x * sin + localCorners[i].y * cos
            );

            // Добавляем позицию центра
            corners[i] = center + new Vector3(rotatedCorner.x, rotatedCorner.y, 0);
        }

        // Рисуем 4 стороны прямоугольника
        Debug.DrawLine(corners[0], corners[1], drawColor, duration); // нижняя сторона
        Debug.DrawLine(corners[1], corners[2], drawColor, duration); // правая сторона
        Debug.DrawLine(corners[2], corners[3], drawColor, duration); // верхняя сторона
        Debug.DrawLine(corners[3], corners[0], drawColor, duration); // левая сторона
    }
}
