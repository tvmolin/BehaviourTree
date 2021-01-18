using UnityEngine;
using UnityEditor;

public class BehaviourEditorRenderer
{
    private BehaviourEditor editor;

    public BehaviourEditorRenderer(BehaviourEditor editor)
    {
        this.editor = editor;
    }

    public void DrawNodes()
    {
        editor.BeginWindows();

        foreach (NodeWindow node in editor.nodes)
        {
            node.DrawCurve();
        }

        for (int i = 0; i < editor.nodes.Count; i++)
        {
            NodeWindow node = editor.nodes[i];
            if (node.isRunning)
            {
                GUI.color = Color.green;
            }
            else if (node.isFailed)
            {
                GUI.color = Color.red;
            }
            else
            {
                GUI.color = Color.cyan;
            }
            node.windowBounds = GUI.Window(i, node.windowBounds, DrawNodeWindow, node.windowTitle);
        }

        editor.EndWindows();
    }

    private void DrawNodeWindow(int id)
    {
        editor.nodes[id].OnNodeWindowDraw();
        GUI.DragWindow();
    }

    public void DrawAddNodeMenu(Event e)
    {
        GenericMenu menu = new GenericMenu();

        //Only allow to add root nodes if there are no nodes in the tree
        if (editor.animalAI == null)
        {
            menu.AddDisabledItem(new GUIContent("Add an AnimalAI to start!"));
        }
        else if (editor.nodes.Count == 0)
        {
            menu.AddItem(new GUIContent("Add Sequence"), false, editor.service.AddNodeCallback, typeof(SequenceNodeWindow));
            menu.AddItem(new GUIContent("Add Selector"), false, editor.service.AddNodeCallback, typeof(SelectorNodeWindow));
        }
        else
        {
            menu.AddItem(new GUIContent("Export tree"), false, editor.service.ExportTreeCallback);
        }
        menu.ShowAsContext();
        e.Use();

    }

    public void DrawModifyNodeMenu(Event e)
    {
        GenericMenu menu = new GenericMenu();
        menu.AddItem(new GUIContent("Delete"), false, editor.service.ModifyNodeCallback, NodeModificationAction.DELETE);
        //Add child node submenu
        if (typeof(CompositeNodeWindow).IsAssignableFrom(editor.selectedNode.GetType()))
        {
            menu.AddSeparator("");
            menu.AddItem(new GUIContent("Add child node/Sequence"), false, editor.service.AddChildNodeCallback, typeof(SequenceNodeWindow));
            menu.AddItem(new GUIContent("Add child node/Selector"), false, editor.service.AddChildNodeCallback, typeof(SelectorNodeWindow));
            menu.AddItem(new GUIContent("Add child node/Action"), false, editor.service.AddChildNodeCallback, typeof(ActionNodeWindow));
            menu.AddItem(new GUIContent("Add child node/Boolean check"), false, editor.service.AddChildNodeCallback, typeof(BooleanCheckNodeWindow));
        }

        //Change type submenu
        menu.AddSeparator("");
        menu.AddItem(new GUIContent("Change type/Sequence"), false, editor.service.ModifyNodeCallback, NodeModificationAction.CHANGE_FOR_SEQUENCE_NODE);
        menu.AddItem(new GUIContent("Change type/Selector"), false, editor.service.ModifyNodeCallback, NodeModificationAction.CHANGE_FOR_SELECTOR_NODE);

        menu.ShowAsContext();
        e.Use();
    }

    public void DrawEditorFields()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.Space(30);
        editor.animalAI = (AnimalAI)EditorGUILayout.ObjectField("Animal AI: ", editor.animalAI, typeof(AnimalAI), true);
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
    }

    public static void DrawNodeCurve(Rect start, Rect end, bool left, Color color)
    {
        Vector3 startPosition = new Vector3(left ? start.x + start.width : start.x,
                                            start.y + (start.height * 0.5f),
                                            0);

        Vector3 endPosition = new Vector3(end.x + (end.width * 0.5f),
                                          end.y + (end.height * 0.5f),
                                          0);
        Vector3 startTangent = startPosition + Vector3.right * 50;
        Vector3 endTangent = endPosition + Vector3.left * 50;

        Color shadow = new Color(0, 0, 0, 0.06f);
        for (int i = 0; i < 3; i++)
        {
            Handles.DrawBezier(startPosition, endPosition, startTangent, endTangent, shadow, null, (i + 1) * 0.5f);
        }

        Handles.DrawBezier(startPosition, endPosition, startTangent, endTangent, color, null, 1);
    }

}