using Constants;
using UnityEngine;

namespace PlayerMovement
{
    public class PlayerTurner : MonoBehaviour
    {
        [SerializeField] private Transform mainCamera;
        [SerializeField] private float angleToTurnAt;
        
        private Animator _animator;
        private ActorMovement _movement;

        public void Start()
        {
            _animator = GetComponent<Animator>();
            _movement = GetComponent<ActorMovement>();
        }

        public void Update()
        {
            var cameraAngle = mainCamera.eulerAngles.y;
            var bodyAngle = transform.eulerAngles.y;

            if (_movement.PlayerIsMoving)
            {
                var currentTransform = transform;
                var currentForward = currentTransform.forward;
                var cameraForward = mainCamera.forward;
                
                currentTransform.forward = new Vector3(cameraForward.x, currentForward.y, cameraForward.z);
                return;
            }

            // if (bodyAngle - cameraAngle < -angleToTurnAt)
            // {
            //     _animator.SetTrigger(AnimatorConstants.TurnRightTrigger);
            // }
            // else if (bodyAngle - cameraAngle > angleToTurnAt)
            // {
            //     _animator.SetTrigger(AnimatorConstants.TurnLeftTrigger);
            // }
        }
    }
}