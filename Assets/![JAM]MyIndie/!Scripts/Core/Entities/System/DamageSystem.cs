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
        var impact = entity.Get<IsDamageComponent>();

        Debug.Log($"Damage: {health.timeImmunity}"); // Debug lin

        if (health.timeImmunity < Time.time)
        {
            var newHealth = health.GetCurrentHealth() - impact.damage;
            health.SetHealth(newHealth);
            health.SetTimeImmunity(Time.time + impact.damage * 3.3f);
            healthProvider.GetComponent<Rigidbody2D>().linearVelocity = impact.direction * impact.damage;
        }
        else if (health.lastDamage < impact.damage)
        {
            var damageDiff = impact.damage - health.lastDamage;
            var newHealth = health.GetCurrentHealth() - damageDiff;
            health.SetHealth(newHealth);
        }

        health.lastDamage = impact.damage;

        entity.Remove<IsDamageComponent>();
    }
}
