using System;
using UnityEngine;

namespace Combat.LockOn
{
    public class LockOnTarget : MonoBehaviour
    {
        public Guid Id { get; private set; }
        public Transform LookAtTransform => lookAtTransform;
        [SerializeField] private Transform lookAtTransform;

        public void Start()
        {
            var entity = GetComponent<Entity>();
            if (entity)
            {
                Id = entity.Id;
            }
        }
    }
}