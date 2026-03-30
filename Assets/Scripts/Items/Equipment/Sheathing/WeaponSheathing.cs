using System;
using UnityEngine;

namespace Items.Equipment.Sheathing
{
    public class WeaponSheathing : MonoBehaviour
    {
        public EventHandler WeaponSheathed;
        public EventHandler WeaponUnSheathed;
        public EventHandler OffhandSheathed;
        public EventHandler OffhandUnSheathed;
        private EquipmentSlotManager _equipmentSlotManager;
        public bool IsWeaponSheathed { get; private set; } = true;
        public bool IsOffhandSheathed { get; private set; } = true;

        public void Start()
        {
            _equipmentSlotManager =  GetComponent<EquipmentSlotManager>();
            _equipmentSlotManager.OnItemEquipped += OnItemEquipped;
            _equipmentSlotManager.OnItemUnEquipped += OnItemUnEquipped;
        }

        private void OnItemUnEquipped(object sender, ItemType e)
        {
            switch (e)
            {
                case ItemType.Weapon:
                    IsWeaponSheathed = true;
                    break;
                case ItemType.Offhand:
                    IsOffhandSheathed = true;
                    break;
            }
        }

        private void OnItemEquipped(object sender, ItemType e)
        {
            switch (e)
            {
                case ItemType.Weapon when !IsOffhandSheathed:
                    UnsheatheWeapon();
                    break;
                case ItemType.Offhand when !IsWeaponSheathed:
                    UnsheatheOffhand();
                    break;
            }
        }

        public void ToggleSheathed()
        {
            if (!IsWeaponSheathed || !IsOffhandSheathed)
            {
                if (!IsWeaponSheathed) SheatheWeapon();
                if (!IsOffhandSheathed) SheatheOffhand();
            }
            else if (IsWeaponSheathed && IsOffhandSheathed)
            {
                UnsheatheWeapon();
                UnsheatheOffhand();
            }
        }

        private void SheatheWeapon()
        {
            if (IsWeaponSheathed || (!_equipmentSlotManager.IsWeaponEquipped && !_equipmentSlotManager.IsOffHandEquipped)) return;
            
            IsWeaponSheathed = true;
            WeaponSheathed?.Invoke(this, EventArgs.Empty);
        }
        
        private void SheatheOffhand()
        {
            if (IsOffhandSheathed ||  !_equipmentSlotManager.IsOffHandEquipped) return;
            
            IsOffhandSheathed = true;
            OffhandSheathed?.Invoke(this, EventArgs.Empty);
        }
        
        private void UnsheatheWeapon()
        {
            if (!IsWeaponSheathed || !_equipmentSlotManager.IsWeaponEquipped) return;
            
            IsWeaponSheathed = false;
            WeaponUnSheathed?.Invoke(this, EventArgs.Empty);
        }
        
        private void UnsheatheOffhand()
        {
            if (!IsOffhandSheathed ||  !_equipmentSlotManager.IsOffHandEquipped) return;
            
            IsOffhandSheathed = false;
            OffhandUnSheathed?.Invoke(this, EventArgs.Empty);
        }
    }
}