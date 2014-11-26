using Game.Scriptable;
using UnityEngine;

public static class Instance
{
    public static EnemyFactory EnemyFactory;
    public static BulletFactory BulletFactory;
    public static PlayerFactory PlayerFactory;
    public static PointManager PointManager;
    public static GameLogicParameters GameLogicParameters;
   
    static Instance()
    {
        EnemyFactory = SafeLoadAsset<EnemyFactory>("EnemyFactory");
        BulletFactory = SafeLoadAsset<BulletFactory>("BulletFactory");
        PlayerFactory = SafeLoadAsset<PlayerFactory>("PlayerFactory");
        PointManager = SafeLoadAsset<PointManager>("PointManager");
        GameLogicParameters = SafeLoadAsset<GameLogicParameters>("GameLogicParameters");
    }

    static T SafeLoadAsset<T>(string fileName) where T : ScriptableObject
    {
        var resource = Resources.Load(fileName);
        if (resource == null)
        {
            Debug.LogError("Can't find file: "+fileName);
            return default(T);
        }

        var casted = resource as T;
        if (casted == null)
        {
            Debug.LogError("Asset "+fileName+" is not "+typeof(T).Name);
            return default(T);
        }
        return casted;
    }
}