using System;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "Items/item")]
    [Serializable]
    public class ItemProperties : ScriptableObject
    {
        [SerializeField] private float weight;
        [SerializeField] private string itemName;
        [SerializeField] private string description;
        [SerializeField] private GameObject prefab;
        
        public float Weight => weight;
        public string ItemName => itemName;
        public string Description => description;
        public GameObject Prefab => prefab;
    }
}