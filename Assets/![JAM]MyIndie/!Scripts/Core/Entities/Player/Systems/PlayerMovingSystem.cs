using System;
using BitterECS.Core;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMovingSystem : IEcsFixedRunSystem, IEcsInitSystem
{
    public Priority Priority => Priority.High;

    private Camera _mainCamera;

    private EcsFilter _ecsFilter =
    Build.For<EntitiesPresenter>()
         .Filter()
         .WhereProvider<EntitiesProvider>()
         .Include<InputComponent>()
         .Include<MovingComponent>();

    public void Init()
    {
        _mainCamera = Camera.main;

        var move = ControllableSystem.Inputs.Playable.Move;
        ControllableSystem.SubscribePerformed(move, MovePressingSystem);
        ControllableSystem.SubscribeCanceled(move, MoveUnPressingSystem);
    }

    private void MovePressingSystem(CallbackContext context)
    {
        var direction = context.ReadValue<Vector2>();
        foreach (var entity in _ecsFilter)
        {
            entity.Get<InputComponent>().currentInput = direction;
        }
    }

    private void MoveUnPressingSystem(CallbackContext context)
    {
        foreach (var entity in _ecsFilter)
        {
            entity.Get<InputComponent>().currentInput = Vector2.zero;
        }
    }

    public void FixedRun()
    {
        foreach (var entity in _ecsFilter)
        {
            var provider = entity.GetProvider<EntitiesProvider>();

            ref var movingComponent = ref entity.Get<MovingComponent>();
            ref var inputComponent = ref entity.Get<InputComponent>();

            var controller = provider.rigidbody;
            var rawInput = inputComponent.currentInput.normalized;
            FlipSprite(provider, rawInput);
            ref var ada = ref entity.Get<HealthComponent>().timeImmunity;
            if (entity.TryGet<HealthComponent>(out var healthComponent) && healthComponent.timeImmunity < Time.time)
            {
                var motion = movingComponent.speed * Time.fixedDeltaTime * rawInput;
                controller.linearVelocity = motion;
            }

            entity.AddOrRemove<IsMovingComponent, Vector3>(new(), rawInput, dir => dir != Vector3.zero);
        }
    }

    private static void FlipSprite(EntitiesProvider provider, Vector2 rawInput)
    {
        if (Mathf.Abs(rawInput.x) < 0.01f) return;

        var scale = provider.transform.localScale;
        scale.x = Mathf.Abs(scale.x) * Mathf.Sign(rawInput.x);
        provider.transform.localScale = scale;
    }
}
