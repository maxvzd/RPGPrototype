using UnityEngine;
using UnityEngine.AI;

public class MoveNavAgent : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    
    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        var pos = Random.insideUnitSphere * 100f;
        pos.y = 0f;
        _navMeshAgent.destination = pos;
    }
}