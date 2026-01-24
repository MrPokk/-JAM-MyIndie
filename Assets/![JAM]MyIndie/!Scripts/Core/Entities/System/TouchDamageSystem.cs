using BitterECS.Core;
using UnityEngine;

public class TouchDamageSystem : IEcsAutoImplement
{
    public Priority Priority => Priority.High;

    public EcsEvent _attackEvent =
        Build.For<EntitiesPresenter>()
        .Event()
        .Subscribe<IsCollisionEnter>(added: OnCollision);

    private static void OnCollision(EcsEntity entity)
    {
        if (!entity.TryGet<TouchDamageComponent>(out var touchDamageComponent))
        {
            return;
        }

        var collisionData = entity.Get<IsCollisionEnter>();
        var damage = touchDamageComponent.damage;
        var collision = collisionData.collision;
        Vector2 direction = collision.transform.position - entity.GetProvider<EntitiesProvider>().transform.position;
        collisionData.collision.gameObject.GetComponent<EntitiesProvider>().Entity.Add(new IsDamageComponent(damage, direction));
    }
}
