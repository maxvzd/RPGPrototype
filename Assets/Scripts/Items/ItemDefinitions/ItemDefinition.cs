using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Items.ItemDefinitions
{
    [CreateAssetMenu(menuName = "Items/Definitions/Item")]
    [Serializable]
    public class ItemDefinition : ScriptableObject
    {
        [SerializeField] private float weight;
        [SerializeField] private string itemName;
        [SerializeField] private string description;
        [SerializeField] private GameObject prefab;
        [FormerlySerializedAs("type")] [SerializeField] private ItemType[] types;
        
        public float Weight => weight;
        public string ItemName => itemName;
        public string Description => description;
        public GameObject Prefab => prefab;
        public ItemType[] Types => types;
    }
}