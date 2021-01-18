
abstract class DecoratorNode : Node
{
    protected Node childNode;

    public DecoratorNode(Node childNode)
    {
        this.childNode = childNode;
    }
}
