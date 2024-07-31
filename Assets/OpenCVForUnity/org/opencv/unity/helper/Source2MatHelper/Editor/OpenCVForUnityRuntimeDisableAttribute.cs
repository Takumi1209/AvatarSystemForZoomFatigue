#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

namespace OpenCVForUnity
{
    public class OpenCVForUnityRuntimeDisableAttribute : PropertyAttribute
    {
    }

    [CustomPropertyDrawer(typeof(OpenCVForUnityRuntimeDisableAttribute))]
    public class OpenCVForUnityRuntimeDisableDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect aPosition, SerializedProperty aProperty, GUIContent aLabel)
        {
            EditorGUI.BeginDisabledGroup(EditorApplication.isPlayingOrWillChangePlaymode);
            EditorGUI.PropertyField(aPosition, aProperty, aLabel, true);
            EditorGUI.EndDisabledGroup();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, true);
        }
    }
}
#endif