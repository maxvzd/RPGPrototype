using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "Items/item")]
    public class ItemProperties : ScriptableObject
    {
        [SerializeField] private float weight;
        [SerializeField] private string itemName;
        [SerializeField] private string description;
        
        public float Weight => weight;
        public string ItemName => itemName;
        public string Description => description;
        //public GameObject Prefab => prefab;
    }
}