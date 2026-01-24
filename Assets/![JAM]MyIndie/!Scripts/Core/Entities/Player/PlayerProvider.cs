using BitterECS.Integration;
using UnityEngine;

[RequireComponent(typeof(MovingComponentProvider), typeof(InputComponentProvider))]
public class PlayerProvider : EntitiesProvider
{
    public Animator animator;
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponentInChildren<Animator>();
    }
}
