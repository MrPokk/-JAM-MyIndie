using BitterECS.Core;
using UnityEngine;
using static UnityEngine.LowLevelPhysics2D.PhysicsShape;
public class TouchDamageSystem : IEcsAutoImplement
{
    public Priority Priority => Priority.High;

    public EcsEvent _attackEvent =
    Build.For<EntitiesPresenter>()
        .Event()
        .Subscribe<TouchDamageComponent>(added: Init);

    private static void Init(EcsEntity entity)
    {
        Debug.Log(entity);
        entity.GetProvider<EntitiesProvider>().onCollisionEnter += CheckCollision;
    }
    private static void CheckCollision(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
            Debug.Log(contact.thisCollider.gameObject.name);
        }
    }
}
