using UnityEngine;
using UnityEngine.AI;

class WanderAction : TimeDependentActionNode
{
    private Vector3 destination = Vector3.zero;
    Wanderer ai;
    Transform transform;
    NavMeshAgent agent;

    public WanderAction(Wanderer ai, NavMeshAgent agent)
    {
        this.ai = ai;
        transform = ai.GetTransform();
        this.agent = agent;
        this.cycleTime = Random.Range(1, 5);
    }

    protected override NodeStatus DoAction()
    {
        Debug.Log("CAT WANDERED");
        ai.SpendEnergyWandering();
        MoveTowardsDestination();

        return NodeStatus.RUNNING;
    }

    private void MoveTowardsDestination()
    {
        if (destination == Vector3.zero)
        {
            float x = transform.position.x;
            float y = transform.position.y;
            float z = transform.position.z;
            destination = new Vector3(Random.Range(x - 5, x + 5), y, Random.Range(z - 5, z + 5));
            NavMeshHit hit;
            NavMesh.SamplePosition(destination, out hit, 10, 1);
            hit.position = new Vector3(hit.position.x, y, hit.position.z);
            destination = hit.position;
        }

        if (Vector3.Distance(transform.position, destination) > 0.1f)
        {
            agent.isStopped = false;
            agent.SetDestination(destination);
        }
        else
        {
            destination = Vector3.zero;
            agent.isStopped = true;
        }
    }
}
