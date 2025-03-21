using System;
using Items.Properties;
using UnityEngine;

namespace Items.InstancePropertiesClasses
{
    [Serializable]
    public class ItemInstanceProperties : InstanceProperties<ItemProperties>
    {
        [SerializeField] private string myMessage;
        public string MyMessage => myMessage;

        public void SetMessage()
        {
            myMessage = "has been set";
        }
    }
}