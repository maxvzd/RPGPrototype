using System;
using System.Collections.Generic;
using System.Linq;
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
        private PlayerAnimationEventListener _eventListener;
        private bool _isTurning;
        private static int _bodyShouldFollowCameraRegister;

        //public List<bool> BodyShouldFollowCamera { get; set; } = false;

        public void Start()
        {
            _animator = GetComponent<Animator>();
            _eventListener = GetComponent<PlayerAnimationEventListener>();
            _eventListener.FinishedTurning += FinishedTurning;
        }

        private void FinishedTurning(object sender, EventArgs e)
        {
            _isTurning = false;
        }

        public static void BodyShouldFollowCameraRegister()
        {
            _bodyShouldFollowCameraRegister++;
        }

        public static void BodyShouldFollowCameraUnRegister()
        {
            _bodyShouldFollowCameraRegister = Mathf.Clamp(_bodyShouldFollowCameraRegister - 1, 0, int.MaxValue);
        }
        
        public void Update()
        {
            if (_bodyShouldFollowCameraRegister > 0) 
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
                var angleBetweenCameraAndBody = mainCamera.eulerAngles.y - transform.eulerAngles.y;
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