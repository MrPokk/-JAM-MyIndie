using BitterECS.Integration;
using UnityEngine;

public class Startup : EcsUnityRoot
{
    protected override void Bootstrap()
    {
    }

    protected override void PostBootstrap()
    {
        PlayerProvider player = new Loader<PlayerProvider>(EntitiesPaths.PLAYER);
        EnemyProvider enemy = new Loader<EnemyProvider>(EntitiesPaths.ENEMY);
    }
}
