using UnityEngine;

namespace Items.Properties
{
    [CreateAssetMenu(menuName = "Items/Weapon")]
    public class WeaponProperties : ItemProperties
    {
        [SerializeField] private float damage;
        public float Damage => damage;
    }
}