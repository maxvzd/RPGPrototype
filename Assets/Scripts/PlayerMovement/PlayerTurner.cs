using System;
using Constants;
using UnityEngine;

namespace PlayerMovement
{
    public class PlayerTurner : MonoBehaviour
    {
        [SerializeField] private Transform mainCamera;
        [SerializeField] private float angleToTurnAt;
        [SerializeField] private float turnSpeed;
        
        private Animator _animator;
        private ActorMovement _movement;
        private PlayerAnimationEventListener _eventListener;
        private bool _isTurning;

        public void Start()
        {
            _animator = GetComponent<Animator>();
            _movement = GetComponent<ActorMovement>();
            _eventListener = GetComponent<PlayerAnimationEventListener>();
            _eventListener.FinishedTurning += FinishedTurning;
        }

        private void FinishedTurning(object sender, EventArgs e)
        {
            _isTurning = false;
        }

        public void Update()
        {
            if (_movement.ActorIsMoving)
            {
                var currentTransform = transform;
                var currentForward = currentTransform.eulerAngles;
                var cameraRot = mainCamera.eulerAngles;
                
                var playerRot = new Vector3(currentForward.x, cameraRot.y, currentForward.z);
                
                transform.rotation = Quaternion.Lerp(currentTransform.rotation, Quaternion.Euler(playerRot), turnSpeed * Time.deltaTime);
                _isTurning = false;
            }
            else if(!_isTurning)
            {
                float angleBetweenCameraAndBody = mainCamera.eulerAngles.y - transform.eulerAngles.y;
                if (angleBetweenCameraAndBody < 0)
                {
                    angleBetweenCameraAndBody += 360;
                }

                if (angleBetweenCameraAndBody > 0 + angleToTurnAt &&  angleBetweenCameraAndBody < 180)
                {
                    _isTurning = true;
                    _animator.SetTrigger(AnimatorConstants.TurnRightTrigger);
                }

                if (angleBetweenCameraAndBody > 180 && angleBetweenCameraAndBody < 360 - angleToTurnAt)
                {
                    _isTurning = true;
                    _animator.SetTrigger(AnimatorConstants.TurnLeftTrigger);
                }
            }
        }
    }
}