using BitterECS.Core;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputBaseAttackingSystem : IEcsInitSystem
{
    public Priority Priority => Priority.High;
    private EcsFilter _playerEntities =
        Build.For<EntitiesPresenter>()
             .Filter()
             .Include<InputComponent>()
             .Exclude<IsMovingComponent>();

    public void Init()
    {
        var inputs = ControllableSystem.Inputs.Playable;

        ControllableSystem.SubscribePerformed(inputs.BasicAttack, OnBasicAttackPerformed);
        ControllableSystem.SubscribeCanceled(inputs.BasicAttack, OnBasicAttackCanceled);
    }

    private void OnBasicAttackPerformed(InputAction.CallbackContext _)
    {
        foreach (var entity in _playerEntities)
        {
            entity.AddOrReplace<IsBaseAttackingComponent>(new());
        }
    }

    private void OnBasicAttackCanceled(InputAction.CallbackContext _)
    {
        foreach (var entity in _playerEntities)
        {
            entity.Remove<IsBaseAttackingComponent>();
        }
    }
}
