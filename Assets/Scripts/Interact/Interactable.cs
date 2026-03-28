using System;
using Registries;
using UnityEngine;

namespace Interact
{
    public abstract class Interactable : MonoBehaviour, IEntity, IUnityEntity
    {
        public Guid Id { get; protected set; }
        public abstract void Interact();
        public abstract string IconName { get; }
        public int GetGameObjectInstanceID()
        {
            return gameObject.GetInstanceID();
        }
    }
}