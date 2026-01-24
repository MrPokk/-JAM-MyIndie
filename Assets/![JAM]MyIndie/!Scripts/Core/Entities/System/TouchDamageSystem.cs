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
        var collisionData = entity.Get<IsCollisionEnter>();
        var hit = collisionData.hit;

        Debug.DrawRay(hit.point, hit.normal, Color.white);
        Debug.Log(hit.collider.gameObject.name);
        entity.Remove<IsCollisionEnter>();
    }
}
