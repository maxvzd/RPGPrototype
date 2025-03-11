using Constants;
using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    public class ApplyRootMotionToNavMeshAgent : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;

        private float _t;
        private float _speed;
        
        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.updatePosition = false;
            _navMeshAgent.updateRotation = true;

            _animator = GetComponent<Animator>();
            _animator.applyRootMotion = true;
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
                _t += Time.deltaTime;
                _t = Mathf.Clamp(_t, 0, 1);
            }
            else if (_t > 0f)
            {
                _t -= Time.deltaTime;
                _t = Mathf.Clamp(_t, 0, 1);
            }
            
            _speed = Mathf.Lerp(0f, _navMeshAgent.speed, _t);

            _animator.SetFloat(AnimatorConstants.Vertical, _speed);
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