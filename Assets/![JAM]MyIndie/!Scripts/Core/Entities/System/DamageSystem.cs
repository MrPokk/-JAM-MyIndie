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
        HealthComponentProvider health = entity.GetProvider<EntitiesProvider>().GetComponent<HealthComponentProvider>();
        int damage = entity.Get<IsDamageComponent>().damage;
        if (health.Value.timeImmunity < Time.time)
        {
            health.Value.currentHealth -= damage;
            health.Value.timeImmunity = Time.time + damage / 0.3f;
        }
        else if (health.Value.lastDamage < damage)
        {
            health.Value.currentHealth -= damage - health.Value.lastDamage;
        }
        health.Value.lastDamage = damage;
        
        entity.Remove<IsDamageComponent>();
    }
}
