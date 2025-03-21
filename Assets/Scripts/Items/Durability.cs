using System;
using UnityEngine;

namespace Items
{
    public interface IDurability
    {
        void InitialiseDurability(Durability durability);
        Durability Durability { get; }
    }
    
    [Serializable]
    public class Durability
    {
        [SerializeField] private float maxDurability;
        [SerializeField] private float currentDurability;
    }
}