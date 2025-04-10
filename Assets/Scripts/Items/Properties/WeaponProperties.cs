﻿using Items.Equipment.Sheathing;
using UnityEngine;

namespace Items.Properties
{
    [CreateAssetMenu(menuName = "Items/Weapon")]
    public class WeaponProperties : ItemProperties, ISheathable
    {
        [SerializeField] private SheathedItemPositions[] sheathPositions;
        public SheathedItemPositions[] SheathPositions => sheathPositions;
    }
}