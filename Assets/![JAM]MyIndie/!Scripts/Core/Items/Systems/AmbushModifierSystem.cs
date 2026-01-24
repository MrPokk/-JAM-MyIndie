//using BitterECS.Core;

//public class AmbushModifierSystem : IEcsFixedRunSystem
//{
//    public Priority Priority => Priority.Medium;

//    private EcsFilter _ecsEntities = Build.For<EntitiesPresenter>()
//        .Filter()
//        .Include<ContainerItemsComponent>(c => c.Constants<AmbushModifier>())
//        .Include<AttackComponent>();

//    public void FixedRun()
//    {
//        foreach (var entity in _ecsEntities)
//        {
//            var modifier = entity.Get<ContainerItemsComponent>().Get<AmbushModifier>();
//            ref var attack = ref entity.Get<AttackComponent>();

//            // Если у цели полное здоровье, увеличиваем урон
//            if (attack.HasTarget && attack.TargetHealth >= attack.TargetMaxHealth)
//            {
//                attack.Damage *= modifier.damage;
//            }
//        }
//    }
//}
