using System;
using BitterECS.Core;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public partial class PlayerMovingSystem : IEcsFixedRunSystem, IEcsInitSystem
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

            var controller = provider.characterController;
            var rawInput = inputComponent.currentInput.normalized;
            provider.spriteRenderer.flipX = rawInput.x < 0;

            var motion = movingComponent.GetSpeed() * Time.fixedDeltaTime * rawInput;
            controller.Move(motion);

            entity.AddOrRemove<IsMovingComponent, Vector3>(new(), rawInput, dir => dir != Vector3.zero);
        }
    }

}
