using BitterECS.Core;

public class ExecutionerSystem : IEcsFixedRunSystem
{
    public Priority Priority => Priority.Medium;

    private EcsFilter _ecsEntities = Build.For<EntitiesPresenter>()
        .Filter()
        .Include<ContainerItemsComponent>(c => c.Constants<ExecutionerThreshold>())
        .Include<IsDamageComponent>();

    public void FixedRun()
    {
        foreach (var entity in _ecsEntities)
        {
            //var exec = entity.Get<ContainerItemsComponent>().Get<ExecutionerThreshold>();
            //var attack = entity.Get<IsDamageComponent>();

            //ref var targetHealth = ref attack.targetEntity.Get<HealthComponent>();

            //if (targetHealth.GetCurrentHealth() > 0 && targetHealth.GetCurrentHealth() < (int)exec.hpThreshold)
            //{
            //    targetHealth.SetHealth(0); // Смерть
            //}
        }
    }
}
