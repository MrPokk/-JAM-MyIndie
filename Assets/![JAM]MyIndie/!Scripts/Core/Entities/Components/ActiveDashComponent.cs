using UnityEngine;

public struct ActiveDashComponent
{
    public Vector2 direction;
    public float remainingTime;

    public ActiveDashComponent(float remainingTime, Vector2 direction)
    {
        this.direction = direction;
        this.remainingTime = remainingTime;
    }
}
