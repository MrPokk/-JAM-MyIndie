using BitterECS.Core;

public class HealthBonusSystem : IEcsFixedRunSystem
{
    public Priority Priority => Priority.Medium;

    private EcsFilter _ecsEntities = Build.For<EntitiesPresenter>()
        .Filter()
        .Include<ContainerItemsComponent>(c => c.Constants<HealthBonus>())
        .Include<HealthComponent>();

    public void FixedRun()
    {
        foreach (var entity in _ecsEntities)
        {
            var modifier = entity.Get<ContainerItemsComponent>().Get<HealthBonus>();
            ref var health = ref entity.Get<HealthComponent>();

            var newMaxHealth = health.GetMaxHealth() + modifier.healthAmount;
            health.SetMaxHealth(newMaxHealth);
        }
    }
}
