using BitterECS.Integration;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class EntitiesProvider : ProviderEcs<EntitiesPresenter>
{
    public CharacterController characterController;
    protected override void Awake()
    {
        base.Awake();

        characterController = GetComponent<CharacterController>();
    }
}
