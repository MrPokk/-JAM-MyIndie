using System;
using BitterECS.Core;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackSystem : IEcsInitSystem, IEcsAutoImplement
{
    public Priority Priority => Priority.High;

    private EcsFilter _playerFilter = Build.For<EntitiesPresenter>()
        .Filter()
        .WhereProvider<EntitiesProvider>()
        .Include<InputComponent>()
        .Include<DamageComponent>();

    public void Init()
    {
        var inputs = ControllableSystem.Inputs.Playable;
        ControllableSystem.SubscribePerformed(inputs.BasicAttack, OnBasicAttackPerformed);
        ControllableSystem.SubscribeCanceled(inputs.BasicAttack, OnBasicAttackCanceled);
    }

    private void OnBasicAttackCanceled(InputAction.CallbackContext context)
    {
        foreach (var entity in _playerFilter)
        {
            entity.Remove<IsAttackingComponent>();
        }
    }

    private void OnBasicAttackPerformed(InputAction.CallbackContext context)
    {
        foreach (var entity in _playerFilter)
        {
            entity.Add<IsAttackingComponent>(new());
            PerformAttack(entity);
        }
    }

    private void PerformAttack(EcsEntity entity)
    {
        Debug.Log($"OnAttack {entity.GetID()}");
        var provider = entity.GetProvider<EntitiesProvider>();

        Vector3 size = new Vector3(0.3f, 0.5f, 1);
        Vector2 posMouse = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 playerPos = provider.transform.position;

        Vector2 direction = posMouse - playerPos;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float radAngle = angle * Mathf.Deg2Rad;

        Vector2 attackCenter = playerPos + new Vector2(size.x / 2 * Mathf.Cos(radAngle), size.x / 2 * Mathf.Sin(radAngle));

        Collider2D[] colliders = Physics2D.OverlapBoxAll(attackCenter, size, angle, 1 << 7);

        Tool.DrawBox(attackCenter, size, angle, Color.red, 1);
        Debug.Log($"Targets found: {colliders.Length}");

        int damage = entity.Get<DamageComponent>().damage;

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent<EntitiesProvider>(out var targetProvider))
            {
                targetProvider.Entity.Add(new IsDamageComponent(damage, direction.normalized));
            }
        }
    }
}
