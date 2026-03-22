using System;
using System.Collections.Generic;
using UnityEngine;

namespace NPC.Senses
{
    public class Detectable : MonoBehaviour
    {
        [SerializeField] private List<Transform> detectablePoints;
        
        public Guid Id { get; private set; }
        public IReadOnlyList<Transform> DetectablePoints => detectablePoints;
        
        public void Awake()
        {
            Id = GetComponent<Entity>().Id;
        }
    }
}