using System.Linq;
using BitterECS.Core;

public class MovementSpeedModifierSystem : IEcsFixedRunSystem
{
    public Priority Priority => Priority.Medium;

    private EcsFilter _ecsEntities =
    Build.For<EntitiesPresenter>()
        .Filter()
        .Include<ContainerItemsComponent>(c => c.Constants<MovementSpeedModifier>());

    public void FixedRun()
    {
        foreach (var entity in _ecsEntities)
        {
            var container = entity.Get<ContainerItemsComponent>();
            var movementSpeedModifier = container.Get<MovementSpeedModifier>();

            entity.Get<MovingComponent>().speed *= movementSpeedModifier.speedBonus;
        }
    }
}
