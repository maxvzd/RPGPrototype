using System;
using UnityEngine;

namespace Combat.LockOn
{
    public class LockOnTriggerHandler : MonoBehaviour
    {
        public EventHandler<Collider> OnEnter;
        public EventHandler<Collider> OnExit;
        
        private void OnTriggerEnter(Collider other)
        {
            OnEnter?.Invoke(this, other);
        }

        private void OnTriggerExit(Collider other)
        {
            OnExit?.Invoke(this, other);
        }
    }
}