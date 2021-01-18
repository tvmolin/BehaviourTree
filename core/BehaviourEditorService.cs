using System.Collections.Generic;
using System;
using UnityEngine;

public class BehaviourEditorService
{
    private BehaviourEditor editor;

    public BehaviourEditorService(BehaviourEditor editor)
    {
        this.editor = editor;
    }

    public void AddNodeCallback(object o)
    {
        Type type = (Type)o;
        NodeWindow newNode = CreateNode(type);
        editor.nodes.Add(newNode);
    }

    public void AddChildNodeCallback(object o)
    {
        if (typeof(CompositeNodeWindow).IsAssignableFrom(editor.selectedNode.GetType()))
        {
            Type type = (Type)o;
            NodeWindow node = CreateNode(type);
            node.parent = editor.selectedNode;

            if (!typeof(AbstractActionNodeWindow).IsAssignableFrom(type))
            {
                //Only add logic node if it is not an action node
                ((CompositeNodeWindow)editor.selectedNode).AddChildToLogicNode(node.logicNode);
            }

            editor.nodes.Add(node);
        }
    }

    public void ExportTreeCallback()
    {
        Node rootNode = FindRootNode();
        Debug.Log(((CompositeNode)rootNode).childNodes.Count);
    }

    public void ModifyNodeCallback(object o)
    {
        NodeModificationAction action = (NodeModificationAction)o;

        switch (action)
        {
            case NodeModificationAction.DELETE:
                RemoveNodeAndChildren(editor.selectedNode);
                editor.selectedNode = null;
                break;
            case NodeModificationAction.CHANGE_FOR_SELECTOR_NODE:
                ReplaceSelectedNode(new SelectorNodeWindow(editor.mousePosition));
                break;
            case NodeModificationAction.CHANGE_FOR_SEQUENCE_NODE:
                ReplaceSelectedNode(new SequenceNodeWindow(editor.mousePosition));
                break;
            default:
                break;
        }
    }

    public NodeWindow CreateNode(Type nodeType)
    {
        if (typeof(CompositeNodeWindow).IsAssignableFrom(nodeType))
        {
            return (NodeWindow)Activator.CreateInstance(nodeType, new Vector3(editor.mousePosition.x, editor.mousePosition.y));
        }
        else
        {
            return (NodeWindow)Activator.CreateInstance(nodeType, new Vector3(editor.mousePosition.x, editor.mousePosition.y), editor.animalAI);
        }
    }

    public void ReplaceSelectedNode(NodeWindow nodeToBeReplaced)
    {
        List<NodeWindow> childNodes = editor.nodes.FindAll(node => node.parent == editor.selectedNode);
        foreach (NodeWindow node in childNodes)
        {
            node.parent = nodeToBeReplaced;
        }

        nodeToBeReplaced.parent = editor.selectedNode.parent;
        nodeToBeReplaced.windowBounds = editor.selectedNode.windowBounds;

        editor.nodes.Remove(editor.selectedNode);
        editor.selectedNode = null;

        editor.nodes.Add(nodeToBeReplaced);
    }

    private void RemoveNodeAndChildren(NodeWindow nodeToBeDeleted)
    {
        RemoveLogicNodeFromParent(nodeToBeDeleted);
        List<NodeWindow> markedForDeletion = new List<NodeWindow>();
        markedForDeletion.Add(nodeToBeDeleted);

        NodeWindow nodeToTest = nodeToBeDeleted;

        while (true)
        {
            NodeWindow childNode = editor.nodes.Find(node => node.parent == nodeToTest);
            if (editor.nodes.Find(node => node.parent == nodeToTest) != null)
            {
                markedForDeletion.Add(childNode);
                nodeToTest = childNode;
                continue;
            }
            break;
        }

        foreach (NodeWindow node in markedForDeletion)
        {
            editor.nodes.Remove(node);
        }
    }

    public Node FindRootNode()
    {
        foreach (NodeWindow node in editor.nodes)
        {
            if (node.parent == null)
            {
                return node.logicNode;
            }
        }

        return null;
    }

    private void RemoveLogicNodeFromParent(NodeWindow nodeToBeDeleted)
    {
        try
        {
            CompositeNodeWindow composite = (CompositeNodeWindow)nodeToBeDeleted;
            CompositeNodeWindow parent = (CompositeNodeWindow)composite.parent;

            if (parent != null)
            {
                parent.RemoveChildFromLogicNode(composite.logicNode);
            }
        }
        catch (System.InvalidCastException)
        {
            //Do nothing, this just wasn't a CompositeNode
        }
    }
}