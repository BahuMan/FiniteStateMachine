using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

namespace cryingpants
{
    [CustomEditor(typeof(FSMBehaviour))]
    class FSMEditor: Editor
    {
        public void OnEnable()
        {
            Debug.Log("Custom FSM editor enabled");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.LabelField("Bart's custom FSM editor");
            serializedObject.ApplyModifiedProperties();
        }
    }
}
