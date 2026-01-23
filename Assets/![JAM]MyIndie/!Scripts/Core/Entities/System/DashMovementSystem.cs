using BitterECS.Core;
using UnityEngine;

public class DashMovementSystem : IEcsAutoImplement, IEcsFixedRunSystem
{
    public Priority Priority => Priority.High;

    private EcsFilter _movingFilter = Build.For<EntitiesPresenter>()
        .Filter()
        .WhereProvider<EntitiesProvider>()
        .Include<ActiveDashComponent>()
        .Include<DashComponent>();

    public void FixedRun()
    {
        var dt = Time.fixedDeltaTime;

        foreach (var entity in _movingFilter)
        {
            ref var activeDash = ref entity.Get<ActiveDashComponent>();
            var settings = entity.Get<DashComponent>();
            var provider = entity.GetProvider<EntitiesProvider>();

            var moveStep = (Vector3)activeDash.direction * settings.dashSpeedMultiplier * dt;
            provider.characterController.Move(moveStep);

            activeDash.remainingTime -= dt;

            if (activeDash.remainingTime <= 0)
            {
                entity.Remove<ActiveDashComponent>();
            }
        }
    }
}
