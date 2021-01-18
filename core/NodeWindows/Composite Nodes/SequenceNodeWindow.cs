using UnityEngine;
using System.Collections.Generic;

public class SequenceNodeWindow : CompositeNodeWindow
{
    public SequenceNodeWindow(Vector3 position)
    {
        windowTitle = "Sequence Node";
        windowBounds = new Rect(position.x, position.y, width, height);
        logicNode = new SequenceNode(new List<Node>());
        isRunning = true;
    }

    protected override void DrawNodeWindow()
    {
    }

}
