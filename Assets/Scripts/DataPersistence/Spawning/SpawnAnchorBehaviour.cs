using UnityEngine;

namespace DataPersistence.Spawning
{
    public class SpawnAnchorBehaviour : MonoBehaviour
    {
        private SpawnAnchor _instance;
        [SerializeField] private string key;
        
        public void Awake()
        {
            _instance = new SpawnAnchor(key, transform.position);
        }
    }
}
