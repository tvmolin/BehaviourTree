using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BehaviourEditor : EditorWindow
{
    private static int DEFAULT_EDITOR_HEIGHT = 800;
    private static int DEFAULT_EDITOR_WIDTH = 600;

    public List<NodeWindow> nodes { get; }
    public NodeWindow selectedNode { get; set; }
    public Vector3 mousePosition;

    public BehaviourEditorRenderer renderer { get; }
    private BehaviourEditorInputHandler inputHandler;
    public BehaviourEditorService service { get; }

    public AnimalAI animalAI;

    #region Menu Items
    [MenuItem("Behaviour Editor/Editor")]
    static void ShowEditor()
    {
        BehaviourEditor editor = GetWindow<BehaviourEditor>();
        editor.minSize = new Vector2(DEFAULT_EDITOR_HEIGHT, DEFAULT_EDITOR_WIDTH);
    }
    #endregion 

    public BehaviourEditor()
    {
        nodes = new List<NodeWindow>();
        renderer = new BehaviourEditorRenderer(this);
        inputHandler = new BehaviourEditorInputHandler(this);
        service = new BehaviourEditorService(this);
    }

    private void OnGUI()
    {
        //Current GUI event being processed in the behaviour editor
        Event e = Event.current;
        mousePosition = e.mousePosition;

        inputHandler.HandleUserInput(e);

        renderer.DrawNodes();

        renderer.DrawEditorFields();

        if (animalAI != null)
        {
            animalAI.topNode = null;
            animalAI.topNode = service.FindRootNode();
        }
    }

}
