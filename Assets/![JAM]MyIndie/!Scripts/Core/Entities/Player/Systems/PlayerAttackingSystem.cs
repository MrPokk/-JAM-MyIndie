using BitterECS.Core;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerAttackingSystem : IEcsAutoImplement
{
    public Priority Priority => Priority.High;

    public EcsEvent _attackEvent =
    Build.For<EntitiesPresenter>()
        .Event()
        .SubscribeWhereEntity<IsBaseAttackingComponent>(e => EcsConditions.Has<InputComponent>(e), added: OnAttack);

    private static void OnAttack(EcsEntity entity)
    {
        Debug.Log($"OnAttack {entity.GetID()}");
        EntitiesProvider provider = entity.GetProvider<EntitiesProvider>();

        Vector3 size = new Vector3(0.3f, 0.5f, 1);
        Vector2 posMouse = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 player = provider.transform.position;

        Vector2 direction = posMouse - player;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion quaternion = Quaternion.AngleAxis(angle, Vector3.forward);

        angle *= Mathf.Deg2Rad;
        player += new Vector2(size.x / 2 * Mathf.Cos(angle), size.x / 2 * Mathf.Sin(angle));
        Collider[] colliders = Physics.OverlapBox(player, size, quaternion, 1 << 7);
        Tool.DrawBox(player, size, quaternion, Color.red, 1);

        int damage = provider.Entity.Get<DamageComponent>().damage;
        foreach (Collider collider in colliders)
        {
            collider.GetComponent<EntitiesProvider>().Entity.Add(new IsDamageComponent(damage, direction.normalized));
        }
        Debug.Log(colliders.Length);
    }
}
