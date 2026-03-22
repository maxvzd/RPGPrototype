using UnityEngine;

namespace DataPersistence.SmartObjects
{
    public class SmartObjectBehaviour : MonoBehaviour
    {
        [SerializeField] private string key;
        public string Key => key;
        
        private SmartObject _instance;

        public void Awake()
        {
            _instance = new SmartObject(key, gameObject);
        }
    }
}