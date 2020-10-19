using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ColorizeAllObjects : MonoBehaviour {

    [MenuItem("Tools/Colorize all objects")]
    public static void LoadLevelAdditive() {
        List<ColorizedObject> objects = new List<ColorizedObject>(FindObjectsOfType<ColorizedObject>());
        objects.ForEach(obj => obj.SetColor(Color.red));
    }

    [MenuItem("Tools/Load Scene Addition", true)]
    public static bool LoadLevelValidation() {
        return Selection.activeObject != null && Selection.activeObject.GetType() == typeof(SceneAsset);
    }
}
