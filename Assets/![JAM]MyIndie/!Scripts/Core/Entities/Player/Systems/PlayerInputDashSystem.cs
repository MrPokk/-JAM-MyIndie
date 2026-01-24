using BitterECS.Core;
using UnityEngine.InputSystem;

public class PlayerInputDashSystem : IEcsInitSystem
{
    public Priority Priority => Priority.High;

    private EcsFilter _playerEntities = Build.For<EntitiesPresenter>()
        .Filter()
        .Include<InputComponent>()
        .Include<DashComponent>()
        .Exclude<ActiveDashComponent>();

    public void Init()
    {
        var inputs = ControllableSystem.Inputs.Playable;
        ControllableSystem.SubscribePerformed(inputs.Dash, OnDashPerformed);
    }

    private void OnDashPerformed(InputAction.CallbackContext context)
    {
        foreach (var entity in _playerEntities)
        {
            entity.AddOrReplace<IsDashingComponent>(new());
        }
    }
}
