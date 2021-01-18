using System.Collections.Generic;
public class SelectorNode : CompositeNode
{
    public SelectorNode() : base() { }
    public SelectorNode(List<Node> childNodes) : base(childNodes) { }

    public override NodeStatus evaluate()
    {
        foreach (Node node in childNodes)
        {
            switch (node.evaluate())
            {
                case NodeStatus.SUCCESS:
                    nodeStatus = NodeStatus.SUCCESS;
                    return nodeStatus;
                case NodeStatus.FAILURE:
                    break;
                case NodeStatus.RUNNING:
                    nodeStatus = NodeStatus.RUNNING;
                    return nodeStatus;
                default:
                    break;
            }
        }
        nodeStatus = NodeStatus.FAILURE;
        return nodeStatus;
    }
}
