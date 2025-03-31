using System.Linq;
using Constants;
using UnityEngine;
using UnityEngine.AI;

namespace PlayerMovement
{
    public class Grounded : MonoBehaviour
    {
        [SerializeField] private float maxCastDepth;
        [SerializeField] private bool isPlayer;
        
        private Vector3 _halfExtents;
        private Animator _animator;
        
        public bool IsGrounded { get; private set; }

        private void Start()
        {
            _animator = GetComponent<Animator>();

            var radius = isPlayer ? GetComponent<CharacterController>().radius : GetComponent<NavMeshAgent>().radius;
            
            _halfExtents = new Vector3(radius, maxCastDepth, radius);
        }

        private void Update()
        {
            DetectIsGrounded();
            Debug.DrawRay(transform.position, -transform.up * maxCastDepth, Color.cyan);
        }

        private void DetectIsGrounded()
        {
            var currentTransform = transform;
            IsGrounded = Physics.BoxCastAll(currentTransform.position, _halfExtents, -currentTransform.up, currentTransform.rotation, maxCastDepth).Any();
            _animator.SetBool(AnimatorConstants.IsGrounded, IsGrounded);
        }
    }
}
