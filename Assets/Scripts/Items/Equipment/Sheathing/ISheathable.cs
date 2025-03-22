using UnityEngine;

namespace Items.Equipment.Sheathing
{
    public interface ISheathable : IEquippable
    {
        SheathedItemPositions[] SheathPositions { get; }
    }
}