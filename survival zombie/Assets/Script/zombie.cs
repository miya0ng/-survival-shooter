using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class zombie : MonoBehaviour
{
    NavMeshAgent agent;
    public LayerMask targetLayer;
    private float traceDist = 100f;
    private Transform target;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        target = FindTarget(traceDist);
        agent.SetDestination(target.position);
    }

    protected Transform FindTarget(float radius)
    {
        var colliders = Physics.OverlapSphere(transform.position, radius, targetLayer.value);
        if (colliders.Length == 0)
        {
            return null;
        }

        var target = colliders.OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).First();
        return target.transform;
    }
}
