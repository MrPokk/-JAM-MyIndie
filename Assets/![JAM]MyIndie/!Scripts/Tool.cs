using UnityEngine;

public class Tool : MonoBehaviour
{
    // Для вызова в любом месте кода
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
}
