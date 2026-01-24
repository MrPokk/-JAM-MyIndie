using System;
using BitterECS.Integration;
using UnityEngine;

[Serializable]
public struct MovingComponent
{
    private const int MaxSpeed = 9999;
    public int speed;
}

public class MovingComponentProvider : ProviderEcs<MovingComponent> { }
