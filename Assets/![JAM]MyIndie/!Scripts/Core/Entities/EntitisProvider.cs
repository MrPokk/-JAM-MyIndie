using System;
using BitterECS.Integration;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class EntitiesProvider : ProviderEcs<EntitiesPresenter>
{
    public CharacterController characterController;
    public Action<Collision> onCollisionEnter;
    protected override void Awake()
    {
        base.Awake();

        characterController = GetComponent<CharacterController>();
    }

    protected void OnCollisionEnter(Collision collision)
    {
        onCollisionEnter.Invoke(collision);
    }
}
