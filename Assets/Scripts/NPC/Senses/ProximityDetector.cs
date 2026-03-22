using UnityEngine;

namespace NPC.Senses
{
    public class ProximityDetector : MonoBehaviour
    {
        [SerializeField] private SphereCollider sightCollider;
        private SightDetection _sight;

        public void Awake()
        {
            _sight = GetComponentInParent<SightDetection>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<Detectable>(out var detectable))
            {
                _sight.AddNearbyEntity(detectable);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent<Detectable>(out var detectable))
            {
                _sight.RemoveNearbyEntity(detectable);
            }
        }

        public void OnValidate()
        {
            var sight = GetComponentInParent<SightDetection>();
            if(sightCollider && sight) 
                sightCollider.radius = sight.Radius;
        }
    }
}