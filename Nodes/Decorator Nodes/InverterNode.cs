class InverterNode : DecoratorNode
{

    public InverterNode(Node childNode) : base(childNode) { }

    public override NodeStatus evaluate()
    {
        switch (childNode.evaluate())
        {
            case NodeStatus.SUCCESS:
                nodeStatus = NodeStatus.FAILURE;
                return nodeStatus;
            case NodeStatus.FAILURE:
                nodeStatus = NodeStatus.SUCCESS;
                return nodeStatus;
            case NodeStatus.RUNNING:
                nodeStatus = NodeStatus.RUNNING;
                return nodeStatus;
            default:
                nodeStatus = NodeStatus.FAILURE;
                return nodeStatus;
        }
    }
}
