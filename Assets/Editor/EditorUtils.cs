using System.IO;
using UnityEditor;
using UnityEngine;
using System.Collections;

public class EditorUtils
{

    public static void CreateAssetFile<T>() where T : ScriptableObject, new()
    {

        var path = string.Format("Assets/Resources/{0}.asset", typeof(T).Name);
        if (Selection.activeObject)
        {
            string selectionPath = AssetDatabase.GetAssetPath(Selection.activeObject); // relative path
            if (Directory.Exists(selectionPath))
            {
                path = Path.Combine(selectionPath, string.Format("{0}.asset", typeof(T).Name));
            }
        }

        var asset = ScriptableObject.CreateInstance<T>();
        AssetDatabase.CreateAsset(asset, path);
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }

    [MenuItem("Custom/Create asteroid manager")]
    public static void CreateAsteroidFactory()
    {
        CreateAssetFile<EnemyFactory>();
    }

    [MenuItem("Custom/Create player manager")]
    public static void CreatePlayerFactory()
    {
        CreateAssetFile<PlayerFactory>();
    }

    [MenuItem("Custom/Create bullet factory")]
    public static void CreateBulletFactory()
    {
        CreateAssetFile<BulletFactory>();
    }

    [MenuItem("Custom/Game Logic Parameters")]
    public static void CreateGameLogicParameters()
    {
        CreateAssetFile<GameLogicParameters>();
    }

    [MenuItem("Custom/Point Manager")]
    public static void CreatePointManager()
    {
        CreateAssetFile<PointManager>();
    }
}
