using System.Collections.Generic;

class SequenceNode : CompositeNode
{
    public SequenceNode() : base() { }
    public SequenceNode(List<Node> childNodes) : base(childNodes) { }

    public override NodeStatus evaluate()
    {
        bool isAnyNodeRunning = false;
        foreach (Node node in childNodes)
        {
            switch (node.evaluate())
            {
                case NodeStatus.SUCCESS:
                    continue;
                case NodeStatus.FAILURE:
                    nodeStatus = NodeStatus.FAILURE;
                    return nodeStatus;
                case NodeStatus.RUNNING:
                    nodeStatus = NodeStatus.RUNNING;
                    isAnyNodeRunning = true;
                    break;
                default:
                    continue;
            }
        }
        nodeStatus = isAnyNodeRunning ? NodeStatus.RUNNING : NodeStatus.SUCCESS;
        return nodeStatus;
    }
}
