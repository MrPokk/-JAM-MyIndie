using BitterECS.Core;

public class LifestealPassiveSystem : IEcsFixedRunSystem
{
    public Priority Priority => Priority.Medium;

    private EcsFilter _ecsEntities = Build.For<EntitiesPresenter>()
        .Filter()
        .Include<ContainerItemsComponent>(c => c.Constants<LifestealPassive>())
        .Include<HealthComponent>()
        .Include<IsDamageComponent>();

    public void FixedRun()
    {
        foreach (var entity in _ecsEntities)
        {
            //var lifesteal = entity.Get<ContainerItemsComponent>().Get<LifestealPassive>();
            //var attack = entity.Get<AttackComponent>();
            //ref var health = ref entity.Get<HealthComponent>();

            //var healAmount = (int)(attack.lastDealtDamage * lifesteal.healPercentage);
            //health.SetHealth(health.GetCurrentHealth() + healAmount);
        }
    }
}
