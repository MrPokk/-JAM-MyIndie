using System.Linq;
using BitterECS.Core;

public class MovementSpeedModifierSystem : IEcsFixedRunSystem
{
    public Priority Priority => Priority.Medium;

    private EcsFilter _ecsEntities =
    Build.For<EntitiesPresenter>()
        .Filter()
        .Include<MovingComponent>()
        .Include<ContainerItemsComponent>(c => c.Constants<MovementSpeedModifier>());

    public void FixedRun()
    {
        foreach (var entity in _ecsEntities)
        {
            ref var moveComponent = ref entity.Get<MovingComponent>();
            ref var container = ref entity.Get<ContainerItemsComponent>();
            var movementSpeedModifier = container.Get<MovementSpeedModifier>();

            moveComponent.speed = moveComponent.baseSpeed * movementSpeedModifier.speedBonus;
        }
    }
}
