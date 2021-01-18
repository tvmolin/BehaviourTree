using UnityEngine;
using System.Collections.Generic;

public class SelectorNodeWindow : CompositeNodeWindow
{
    public SelectorNodeWindow(Vector3 position)
    {
        windowTitle = "Selector Node";
        windowBounds = new Rect(position.x, position.y, width, height);
        logicNode = new SelectorNode(new List<Node>());
    }

    protected override void DrawNodeWindow()
    {
        // label = GUILayout.TextField(label);
    }

}
