using UnityEngine;

namespace PlayerMovement
{
    public class ShouldAlignBodyWithCameraWhenMoving : MonoBehaviour
    {
        private ActorMovement _movement;
        private bool _actorWasMovingLastFrame;

        private void Start()
        {
            _movement = GetComponent<ActorMovement>();
        }

        public void Update()
        {
            if (_actorWasMovingLastFrame != _movement.ActorIsMoving)
            {
                if (_movement.ActorIsMoving)
                {
                    PlayerTurner.BodyShouldFollowCameraRegister();
                }
                else
                {
                    PlayerTurner.BodyShouldFollowCameraUnRegister();
                }
            }
            _actorWasMovingLastFrame = _movement.ActorIsMoving;
        }
    }
}
