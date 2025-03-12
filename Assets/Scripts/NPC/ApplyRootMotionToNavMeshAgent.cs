using PlayerMovement;
using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    public class ApplyRootMotionToNavMeshAgent : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        
        private ActorMovement _actorMovement;

        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.updatePosition = false;
            _navMeshAgent.updateRotation = true;

            _animator = GetComponent<Animator>();
            _animator.applyRootMotion = true;

            _actorMovement = GetComponent<ActorMovement>();
        }
        
        private void Update()
        {
            SynchroniseAnimatorAndAgent();
        }

        private void SynchroniseAnimatorAndAgent()
        {
            if (!_navMeshAgent.enabled) return;
            
            if (_navMeshAgent.remainingDistance >= _navMeshAgent.stoppingDistance && !_navMeshAgent.isStopped)
            {
                _actorMovement.Move(new Vector2(0, _navMeshAgent.speed));
            }
            else
            {
                _actorMovement.Move(new Vector2(0, 0));
            }
        }

        private void OnAnimatorMove()
        {
            Vector3 rootPos = _animator.rootPosition;
            rootPos.y = _navMeshAgent.nextPosition.y;
            transform.position = rootPos;
            _navMeshAgent.nextPosition = rootPos;
        }
    }
}