using System.Collections.Generic;
using System.IO;
using DataPersistence.SerializableClasses.OnObject;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Serialization;

namespace DataPersistence
{
    public class Serializer : MonoBehaviour
    {
        [FormerlySerializedAs("gameObjectToSerialize")] [SerializeField] private List<GameObject> gameObjectsToSerialize;
        
        public void SerializeGameObject()
        {
            var serializableItems = new ISerializable[gameObjectsToSerialize.Count];
            for (var i = 0; i < gameObjectsToSerialize.Count; i++)
            {
                var gameObjectToSerialize = gameObjectsToSerialize[i];
                var serializer = (ISerializableGameObject<ISerializable>)GetSerializableComponent(gameObjectToSerialize);
                var serializable = serializer.GetSerializable();
                serializableItems[i] = serializable;
            }

            var serializedObject = JsonConvert.SerializeObject(serializableItems, Formatting.Indented);
            File.AppendAllText("people.json", serializedObject);
        }
        
        private object GetSerializableComponent(GameObject obj)
        {
            foreach (var component in obj.GetComponents<MonoBehaviour>())
            {
                var interfaces = component.GetType().GetInterfaces();
                foreach (var type in interfaces)
                {
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ISerializableGameObject<>))
                    {
                        return component;
                    }
                }
            }

            return null;
        }
    }
}