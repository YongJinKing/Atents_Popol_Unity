using UnityEditor.SceneManagement;
using UnityEditor;

public class SceneStartOption
{
    [MenuItem("SceneStartOption/시작 씬부터 시작 #F5")]
    public static void SetupFromStartScene()
    {
        var pathOfFirstScene = EditorBuildSettings.scenes[0].path;
        var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(pathOfFirstScene);
        EditorSceneManager.playModeStartScene = sceneAsset;
        UnityEditor.EditorApplication.isPlaying = true;
    }

    [MenuItem("SceneStartOption/현재 씬부터 시작 _F5")]
    public static void StartFromThisScene()
    {
        EditorSceneManager.playModeStartScene = null;
        UnityEditor.EditorApplication.isPlaying = true;
    }
}
