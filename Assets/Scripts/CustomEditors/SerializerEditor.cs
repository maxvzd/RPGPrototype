using DataPersistence;
using UnityEditor;
using UnityEngine;

namespace CustomEditors
{
    [CustomEditor(typeof(Serializer))]
    public class SerializerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            Serializer serializer = (Serializer)target;
            if(GUILayout.Button("Serialize"))
            {
                serializer.SerializeGameObject();
            }
        }
    }
}