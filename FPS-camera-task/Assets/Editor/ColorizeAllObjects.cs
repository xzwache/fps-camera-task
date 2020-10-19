using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ColorizeAllObjects : MonoBehaviour {

    [MenuItem("Tools/Colorize all objects")]
    public static void LoadLevelAdditive() {
        List<ColorizedObject> objects = new List<ColorizedObject>(FindObjectsOfType<ColorizedObject>());
        objects.ForEach(obj => {
            obj.Reset();
            obj.SetColor(Color.red);
        });
    }
}
