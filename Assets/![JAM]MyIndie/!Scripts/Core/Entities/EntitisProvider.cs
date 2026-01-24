using System;
using BitterECS.Integration;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class EntitiesProvider : ProviderEcs<EntitiesPresenter>
{
    public CharacterController characterController;
    public SpriteRenderer spriteRenderer;

    protected override void Awake()
    {
        base.Awake();
        characterController = GetComponent<CharacterController>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    protected void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Entity.Add<IsCollisionEnter>(new(hit));
    }
}

internal struct IsCollisionEnter
{
    public ControllerColliderHit hit;
    public IsCollisionEnter(ControllerColliderHit hit)
    {
        this.hit = hit;
    }
}
