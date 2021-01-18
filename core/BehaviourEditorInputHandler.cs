using UnityEngine;

public class BehaviourEditorInputHandler
{
    private BehaviourEditor editor;

    public BehaviourEditorInputHandler(BehaviourEditor editor)
    {
        this.editor = editor;
    }

    public void HandleUserInput(Event e)
    {
        if (e.type == EventType.MouseDown)
        {
            if (e.button == 1)
            {
                RightClick(e);
            }
            else if (e.button == 0)
            {
                LeftClick(e);
            }
        }
    }

    private void RightClick(Event e)
    {
        bool clickedOnNode = false;
        editor.selectedNode = null;
        for (int i = 0; i < editor.nodes.Count; i++)
        {
            NodeWindow node = editor.nodes[i];
            if (node.windowBounds.Contains(e.mousePosition))
            {
                clickedOnNode = true;
                editor.selectedNode = node;
                break;
            }
        }

        if (clickedOnNode)
        {
            editor.renderer.DrawModifyNodeMenu(e);
        }
        else
        {
            editor.renderer.DrawAddNodeMenu(e);
        }
    }

    private void LeftClick(Event e)
    {
        // Debug.Log("Not implemented");
    }

}