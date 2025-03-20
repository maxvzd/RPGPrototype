using System;
using UnityEngine;

namespace Items
{
    [Serializable]
    public class ItemInstanceProperties
    {
        [SerializeField] private string myMessage;
        
        public ItemProperties Item { get; private set; }
        public bool IsInitialisedSpecialProp { get; private set; }

        public string MyMessage => myMessage;

        public void SetMessage()
        {
            myMessage = "has been set";
        }

        public void Initialise(ItemProperties item)
        {
            IsInitialisedSpecialProp = true;
            Item = item;
        }
    }
}