using BitterECS.Core;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDashSystem : IEcsInitSystem
{
    public Priority Priority => Priority.High;

    private EcsFilter _playerEntities =
    new EcsFilter<EntitiesPresenter>()
        .Include<InputComponent>()
        .Include<DashComponent>();

    public void Init()
    {
        var inputs = ControllableSystem.Inputs.Playable;
        ControllableSystem.SubscribePerformed(inputs.Dash, OnDashPerformed);
    }

    private void OnDashPerformed(InputAction.CallbackContext context)
    {
        foreach (var entity in _playerEntities)
        {
            OnDashStarted(entity);
        }
    }

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

            entity.AddOrReplace(new IsDashingComponent(dash.dashDuration, direction));
        }
    }
}
