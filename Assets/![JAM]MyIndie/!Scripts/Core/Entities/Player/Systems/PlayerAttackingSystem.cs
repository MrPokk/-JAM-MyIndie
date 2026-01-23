using BitterECS.Core;
using UnityEngine;

public class PlayerAttackingSystem : IEcsAutoImplement
{
    public Priority Priority => Priority.High;

    private EcsEvent _attackEvent =
    Build.For<EntitiesPresenter>()
        .Event()
        .SubscribeWhereEntity<IsBaseAttackingComponent>(e =>
        EcsConditions.Has<InputComponent>(e),
    added: OnAttack);

    private static void OnAttack(EcsEntity entity)
    {
        Debug.Log(entity.GetID());
    }
}
