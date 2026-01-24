using BitterECS.Core;
using UnityEngine;

public class DamageSystem : IEcsAutoImplement
{
    public Priority Priority => Priority.High;

    public EcsEvent _attackEvent =
        Build.For<EntitiesPresenter>()
        .Event()
        .Subscribe<IsDamageComponent>(added: OnDamage);

    private static void OnDamage(EcsEntity entity)
    {
        var healthProvider = entity.GetProvider<EntitiesProvider>().GetComponent<HealthComponentProvider>();
        ref var health = ref healthProvider.Value;
        var damage = entity.Get<IsDamageComponent>().damage;

        if (health.timeImmunity < Time.time)
        {
            var newHealth = health.GetCurrentHealth() - damage;
            health.SetHealth(newHealth);
            health.timeImmunity = Time.time + damage / 0.3f;
        }
        else if (health.lastDamage < damage)
        {
            var damageDiff = damage - health.lastDamage;
            var newHealth = health.GetCurrentHealth() - damageDiff;
            health.SetHealth(newHealth);
        }

        health.lastDamage = damage;

        entity.Remove<IsDamageComponent>();
    }
}
