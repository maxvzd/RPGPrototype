using UnityEngine;

namespace Items.Equipment.Sheathing
{ 
    [CreateAssetMenu(menuName = "Items/Sheathing/SheathePosition")]
    public class SheathedItemPositions : ScriptableObject
    {
        [SerializeField] private ItemType type;
        [SerializeField] private Vector3 sheathedPosition;
        [SerializeField] private Vector3 sheathedRotation;
        [SerializeField] private Vector3 wieldedPosition;
        [SerializeField] private Vector3 wieldedRotation;

        public ItemType Type => type;
        public Vector3 SheathedPosition => sheathedPosition;
        public Vector3 SheathedRotation => sheathedRotation;
        public Vector3 WieldedPosition => wieldedPosition;
        public Vector3 WieldedRotation => wieldedRotation;
    }
}