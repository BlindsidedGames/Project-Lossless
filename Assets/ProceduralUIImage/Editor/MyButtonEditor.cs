using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

namespace Editor1
{
    [CustomEditor(typeof(MyButton))]
    public class MyButtonEditor : ButtonEditor
    {
        public override void OnInspectorGUI()
        {
            var targetMyButton = (MyButton)target;

            targetMyButton.invertInteractableColor =
                EditorGUILayout.Toggle("Invert", targetMyButton.invertInteractableColor);

            base.OnInspectorGUI();
        }
    }
}