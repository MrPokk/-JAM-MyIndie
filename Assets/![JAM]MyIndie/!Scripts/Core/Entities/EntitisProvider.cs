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
}
