using System.Collections.Generic;

public abstract class CompositeNode : Node
{

    public List<Node> childNodes = new List<Node>();

    public CompositeNode()
    {

    }

    public CompositeNode(List<Node> childNodes)
    {
        this.childNodes = childNodes;
    }
}
