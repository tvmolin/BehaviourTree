class BooleanCheck : LeafNode
{
    public delegate bool BooleanExpression();

    public BooleanExpression expression;

    public BooleanCheck(BooleanExpression expression)
    {
        this.expression = expression;
    }

    public override NodeStatus evaluate()
    {
        nodeStatus = expression() ? NodeStatus.SUCCESS : NodeStatus.FAILURE;
        return nodeStatus;
    }
}
