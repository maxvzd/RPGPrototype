using UnityEngine;
using UnityEngine.AI;

public class MoveNavAgent : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    
    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.destination = Random.insideUnitSphere * 100f;
    }
}
