using BitterECS.Integration;

public class Startup : EcsUnityRoot
{
    protected override void Bootstrap()
    {

    }

    protected override void PostBootstrap()
    {
        PlayerProvider player = new Loader<PlayerProvider>(EntitiesPaths.PLAYER);
    }
}
