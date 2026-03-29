using BitterECS.Core;
using UnityEngine;

public class DashMovementSystem : IEcsAutoImplement, IEcsFixedRunSystem
{
    public Priority Priority => Priority.High;

    private EcsFilter _movingFilter = Build.For<EntitiesPresenter>()
        .Filter()
        .WhereProvider<PlayerProvider>()
        .Include<IsDashingComponent>()
        .Include<DashComponent>();

    public void FixedRun()
    {
        var dt = Time.fixedDeltaTime;

        foreach (var entity in _movingFilter)
        {
            ref var activeDash = ref entity.Get<IsDashingComponent>();
            ref var settings = ref entity.Get<DashComponent>();
            var provider = entity.GetProvider<EntitiesProvider>();

            var velocity = activeDash.direction * settings.dashSpeedMultiplier;

            provider.rigidbody.linearVelocity = velocity;

            activeDash.remainingTime -= dt;

            if (activeDash.remainingTime <= 0)
            {
                entity.Remove<IsDashingComponent>();
            }
        }
    }
}
