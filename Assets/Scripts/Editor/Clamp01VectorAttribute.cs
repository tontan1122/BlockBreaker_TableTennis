using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Clamp01VectorAttribute : PropertyAttribute { }

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(Clamp01VectorAttribute))]
public class Clamp01VectorDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        switch (property.type)
        {
            case "Vector2":
                property.vector2Value = new Vector2(
                    Mathf.Clamp01(property.vector2Value.x),
                    Mathf.Clamp01(property.vector2Value.y)
                );
                break;
            case "Vector3":
                property.vector3Value = new Vector3(
                    Mathf.Clamp01(property.vector3Value.x),
                    Mathf.Clamp01(property.vector3Value.y),
                    Mathf.Clamp01(property.vector3Value.z)
                );
                break;
        }
        EditorGUI.PropertyField(position, property, label, true);
    }
}
#endif
