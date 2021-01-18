using UnityEngine;

public abstract class NodeWindow
{
    public Rect windowBounds;
    public string windowTitle;
    public float height = 100;
    public float width = 200;
    public string label = "";
    public NodeWindow parent;

    public Node logicNode;
    public bool isRunning = false;
    public bool isFailed = false;
    public bool isSuccessful = false;

    public void OnNodeWindowDraw()
    {
        if (logicNode != null)
        {
            isRunning = logicNode.nodeStatus == NodeStatus.RUNNING;
            isFailed = logicNode.nodeStatus == NodeStatus.FAILURE;
            isSuccessful = logicNode.nodeStatus == NodeStatus.SUCCESS;
        }

        DrawNodeWindow();
    }

    protected abstract void DrawNodeWindow();

    public void DrawCurve()
    {
        if (parent != null)
        {
            Rect rect = windowBounds;
            rect.y += windowBounds.height * 0.5f;
            rect.width = 1;
            rect.height = 1;

            BehaviourEditorRenderer.DrawNodeCurve(parent.windowBounds, rect, true, Color.black);
        }
    }
}
