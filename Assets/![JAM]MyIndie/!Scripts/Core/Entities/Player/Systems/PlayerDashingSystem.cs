using BitterECS.Core;
using UnityEngine;

public class PlayerDashingSystem : IEcsAutoImplement
{
    public Priority Priority => Priority.Medium;

    private EcsEvent _dashTrigger = Build.For<EntitiesPresenter>()
        .Event()
        .SubscribeWhereEntity<IsDashingComponent>(e =>
            EcsConditions.HasProvider<EntitiesProvider>(e) &&
            EcsConditions.Has<DashComponent, InputComponent>(e),
            added: OnDashStarted);

    private static void OnDashStarted(EcsEntity entity)
    {
        ref var dash = ref entity.Get<DashComponent>();

        if (dash.currentCharges > 0)
        {
            dash.currentCharges--;
            dash.delayTimer = dash.rechargeDelay;

            var input = entity.Get<InputComponent>();
            var provider = entity.GetProvider<EntitiesProvider>();

            var direction = input.currentInput.sqrMagnitude > 0.01f
                ? input.currentInput.normalized
                : (Vector2)provider.transform.right;

            entity.Add(new ActiveDashComponent(dash.dashDuration, direction));
        }

        entity.Remove<IsDashingComponent>();
    }
}
