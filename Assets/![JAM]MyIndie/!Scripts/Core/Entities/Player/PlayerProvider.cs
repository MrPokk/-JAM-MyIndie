using BitterECS.Integration;
using UnityEngine;

[RequireComponent(typeof(MovingComponentProvider), typeof(InputComponentProvider))]
[RequireComponent(typeof(CharacterController))]
public class PlayerProvider : EntitiesProvider
{
    public Animator animator;
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponentInChildren<Animator>();
    }
}
