using UnityEngine;

public abstract class Node : ScriptableObject
{
    public NodeStatus nodeStatus;

    public abstract NodeStatus evaluate();

}
