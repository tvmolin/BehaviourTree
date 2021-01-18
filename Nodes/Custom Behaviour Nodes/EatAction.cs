using UnityEngine;

[CreateAssetMenu]
class EatAction : TimeDependentActionNode
{
    Eater ai;

    public EatAction(Eater ai)
    {
        this.ai = ai;
    }

    public EatAction(Eater ai, float cycleTime)
    {
        this.ai = ai;
        this.cycleTime = cycleTime;
    }

    protected override NodeStatus DoAction()
    {
        Debug.Log("CAT ATE");
        ai.ConsumeFood(new Food());

        if (ai.IsFull())
        {
            ai.StopEating();
            nodeStatus = NodeStatus.SUCCESS;
        }
        else
        {
            nodeStatus = NodeStatus.RUNNING;
        }

        return nodeStatus;
    }
}
