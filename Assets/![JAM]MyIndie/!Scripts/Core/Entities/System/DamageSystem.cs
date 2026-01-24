using BitterECS.Core;
public class DamageSystem : IEcsAutoImplement
{
    public Priority Priority => Priority.High;

    public EcsEvent _attackEvent =
    Build.For<EntitiesPresenter>()
        .Event()
        .Subscribe<IsDamageComponent>(added: OnDamage);

    private static void OnDamage(EcsEntity entity)
    {
        entity.GetProvider<EntitiesProvider>().GetComponent<HealthComponentProvider>().Value.currentHealth -= entity.Get<IsDamageComponent>().damage;
        entity.Remove<IsDamageComponent>();
    }
}
