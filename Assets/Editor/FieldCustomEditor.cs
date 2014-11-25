using UnityEditor;
using UnityEngine;

[CustomEditor(typeof (Field))]
public class FieldCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var field = target as Field;

        field.SetToCameraSize = EditorGUILayout.Toggle("Set to camera size", field.SetToCameraSize);
        if (field.SetToCameraSize)
        {
            field.TargetCamera =
                (Camera) EditorGUILayout.ObjectField("Target camera", field.TargetCamera, typeof (Camera), true);
        }
        else
        {
            field.Width = EditorGUILayout.Slider("Width", field.Width, 1, 100);
            field.Height = EditorGUILayout.Slider("Height", field.Height, 1, 100);
        }

        EditorUtility.SetDirty(target);
    }
}