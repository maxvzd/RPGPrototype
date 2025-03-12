using UnityEngine;

namespace NPC
{
    public class SocialStats : MonoBehaviour
    {
        [SerializeField] private float _disposition;
        public float Disposition => _disposition;
    }
}