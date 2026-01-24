using BitterECS.Core;
using UnityEngine;

public class DashRechargeSystem : IEcsFixedRunSystem, IEcsAutoImplement
{
    public Priority Priority => Priority.Low;

    private EcsFilter _query =
    Build.For<EntitiesPresenter>()
    .Filter()
    .Include<DashComponent>();

    public void FixedRun()
    {
        var deltaTime = Time.fixedDeltaTime;

        foreach (var entity in _query)
        {
            ref var dash = ref entity.Get<DashComponent>();

            if (dash.currentCharges >= dash.maxCharges)
            {
                dash.rechargeTimer = 0;
                dash.delayTimer = 0;
                continue;
            }

            if (dash.delayTimer > 0)
            {
                dash.delayTimer -= deltaTime;
                continue;
            }

            dash.rechargeTimer += deltaTime;

            if (dash.rechargeTimer >= dash.rechargeDuration)
            {
                dash.currentCharges++;
                dash.rechargeTimer = 0;
            }
        }
    }

}
