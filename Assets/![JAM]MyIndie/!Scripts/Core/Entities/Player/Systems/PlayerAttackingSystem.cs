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

        Vector3 size = new Vector3(2, 1.5f, 1);
        Vector2 posMouse = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 player = provider.transform.position;

        Vector2 direction = posMouse - player;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion quaternion = Quaternion.AngleAxis(angle, Vector3.forward);

        Collider[] colliders = Physics.OverlapBox(player, size, quaternion, 1 << 7);
        Tool.DrawBox(player, size, quaternion, Color.red, 1);

        foreach (Collider collider in colliders)
        {
            collider.GetComponent<HealthComponentProvider>().Value.currentHealth--;
        }
        Debug.Log(colliders.Length);
    }
}
