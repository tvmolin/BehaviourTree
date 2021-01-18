public abstract class CompositeNodeWindow : NodeWindow
{
    public void AddChildToLogicNode(Node child)
    {
        ((CompositeNode)logicNode).childNodes.Add(child);
    }

    public void RemoveChildFromLogicNode(Node child)
    {
        ((CompositeNode)logicNode).childNodes.Remove(child);
    }
}