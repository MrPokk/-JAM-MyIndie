using BitterECS.Integration;
using UnityEngine.EventSystems;
using UnityEngine;

public class Startup : EcsUnityRoot
{
    public static CameraObject Camera;
    public static EventSystem EventSystem;

    protected override void Bootstrap()
    {
        Camera = new Loader<CameraObject>(PrefabObjectsPaths.CAMERA_OBJECT);
        EventSystem = new Loader<EventSystem>(SettingsPaths.EVENT_SYSTEM);
    }

    protected override void PostBootstrap()
    {
        PlayerProvider player = new Loader<PlayerProvider>(EntitiesPaths.PLAYER);
        EnemyProvider enemy = new Loader<EnemyProvider>(EntitiesPaths.ENEMY);
    }
}
