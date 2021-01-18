using UnityEngine;

abstract class TimeDependentActionNode : ActionNode
{
    protected float cycleTime = 1;
    protected float timePassed = 0;

    public override NodeStatus evaluate()
    {
        timePassed += Time.deltaTime;

        if (timePassed >= cycleTime)
        {
            timePassed = 0;
            return DoAction();
        }

        return NodeStatus.RUNNING;
    }

    protected abstract NodeStatus DoAction();

}
