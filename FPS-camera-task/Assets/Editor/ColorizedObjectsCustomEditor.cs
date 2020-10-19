using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[UnityEditor.CustomEditor(typeof(ColorizedObject), true)]
public class ColorizedObjectsCustomEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        ColorizedObject obj = target as ColorizedObject;
        EditorGUILayout.LabelField("Default Color: ", obj.DefaultColor.ToString());
        
        if (GUILayout.Button("Reset color to default value")) {
            obj.SetColor(obj.DefaultColor);
        }
    }
}
