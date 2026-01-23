using BitterECS.Integration;
using UnityEngine;

[RequireComponent(typeof(MovingComponentProvider), typeof(InputComponentProvider))]
[RequireComponent(typeof(CharacterController))]
public class PlayerProvider : ProviderEcs<EntitiesPresenter>
{
    public CharacterController characterController;
    protected override void Awake()
    {
        base.Awake();

        characterController = GetComponent<CharacterController>();
    }
}
