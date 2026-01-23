using System;
using BitterECS.Integration;
using UnityEngine;

[Serializable]
public struct MovingComponent
{
    private const int MaxSpeed = 10;
    [SerializeField, Range(1, MaxSpeed)] private int _speed;
    private int _currentSpeed;

    public readonly int GetSpeed() => _currentSpeed > 0 ? _currentSpeed : _speed;
    public void SetSpeed(int speed) => _currentSpeed = Mathf.Clamp(speed, 1, MaxSpeed);
    public void ResetSpeed() => _currentSpeed = _speed;
}

public class MovingComponentProvider : ProviderEcs<MovingComponent> { }
